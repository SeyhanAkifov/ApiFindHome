﻿using ApiFindHome.Data;
using ApiFindHome.Dto;
using ApiFindHome.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFindHome.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        public HomeController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.mapper = mapper;
            this.userManager = userManager;

        }

        ApplicationDbContext db = new ApplicationDbContext();

        [EnableCors]
        [HttpGet]
        
        public object Get(int page)
        {
            ICollection<Property> list = db.Properties
                .Include(x => x.AdFor)
                .Include(x => x.Feature)
                .Include(x => x.Address)
                .Include(x => x.Address.City)
                .Include(x => x.Address.City.Country)
                .Include(x => x.Type).OrderByDescending(x => x.AddedOn).Skip(page).Take(6).ToList();
            
            var result = mapper.Map<ICollection<Property>, ICollection<HomePagePropertyDto>>(list);

            return result;
        }

        
        [HttpGet]
        public object GetCitiesWitProperties()
        {
            ICollection<Property> list = db.Properties
                .Include(x => x.Address)
                .Include(x => x.Address.City)
                .Include(x => x.Address.City.Country)
                .Include(x => x.Type).ToList();

            
            var cityList = db.Cities.ToList();

            var result = cityList.Select(x => new CitySearchDto
            {
                Id = x.Id,
                City = x.Name,
                Properties = list.Where(y => y.Address.City.Name == x.Name).Count(),
                Size =  8 
                
            }).OrderByDescending(x => x.Properties).Take(4).ToList();

            result.First().Size = 4;
            result.Last().Size = 4;

           

            return result;

        }

        
        [HttpGet]
        public object GetWithId(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new Response { Status = "Failed", Message = "Id must be a positive number!" });
            }

            Property property = db.Properties
                .Include(x => x.AdFor)
                .Include(x => x.Feature)
                .Include(x => x.Address)
                .Include(x => x.Address.City)
                .Include(x => x.Address.City.Country)
                .Include(x => x.UserLikes)
                .Include(x => x.Type).FirstOrDefault(x => x.Id == id);

            if (property == null)
            {
                return NotFound(new Response { Status = "Not Found", Message = "Not found property with this id!" });
            }

            var result = mapper.Map<Property, PropertyDetailsDto>(property);

            return result;
        }

        [HttpGet]
        public object GetTypeWithId(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new Response { Status = "Failed", Message = "Id must be a positive number!" });
            }

            var type = db.PropertyTypes.FirstOrDefault(x => x.Id == id);

            if (type == null)
            {
                return NotFound(new Response { Status = "Not Found", Message = "Not found type with this id!" });
            }

            return type;
        }

        [HttpGet]
        public object GetTypes()
        {

            var types = db.PropertyTypes.ToArray();

            return types;
        }

       
        [Authorize]
        [HttpGet]
        public object DeleteWithId(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new Response { Status = "Failed", Message = "Id must be a positive number!" });
            }

            var property = db.Properties.FirstOrDefault(x => x.Id == id);

            if (property == null)
            {
                return NotFound(new Response { Status = "Not Found", Message = "Not found property with this id!" });
            }

            db.Properties.Remove(property);
            db.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Property deleted successfully!" });
        }

        [Authorize]
        [HttpPost]
        public object Post([FromBody] PropertyInputModelDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response { Status = "Failed", Message = "Check your inputs" });
            }

            var typeName = model.TypeName;
            var adfor = model.AdFor;
            var cityName = model.CityName;
            var countryName = model.CountryName;

            var type = db.PropertyTypes.FirstOrDefault(x => x.Name == typeName);
            var adFor = db.AdFors.FirstOrDefault(x => x.Name == adfor);
            var city = db.Cities.FirstOrDefault(x => x.Name == cityName);
            var country = db.Countries.FirstOrDefault(x => x.Name == countryName);

            if (city == null)
            {
                db.Cities.Add(new City { Name = cityName , Country = new Country { Name = countryName} });
                db.SaveChanges();
                city = db.Cities.FirstOrDefault(x => x.Name == cityName);
            }

            if (country == null)
            {
                db.Countries.Add(new Country { Name = countryName });
                db.SaveChanges();
                country = db.Countries.FirstOrDefault(x => x.Name == countryName);
            }

            city.Country = country;

            var feature = new Feature
            {
                AirConditioning = model.Feature.AirConditioning,

                Barbeque = model.Feature.Barbeque,

                Dryer = model.Feature.Dryer,

                Gym = model.Feature.Gym,

                Laundry = model.Feature.Laundry,

                Lawn = model.Feature.Lawn,

                Kitchen = model.Feature.Kitchen,

                OutdoorShower = model.Feature.OutdoorShower,

                Refrigerator = model.Feature.Refrigerator,

                Sauna = model.Feature.Sauna,

                SwimmingPool = model.Feature.SwimmingPool,

                TvCable = model.Feature.TvCable,

                Washer = model.Feature.Washer,

                Wifi = model.Feature.Wifi,

                WindowCoverings = model.Feature.WindowCoverings
            };

            var property = new Property
            {
                Type = type,
                Price = model.Price,
                AdFor = adFor,
                Condition = model.Condition,
                Address = new Address { City = city, PostCode = model.PostCode, StreetName = model.Address.Split(" ")[0], StreetNumber = model.Address.Split(" ")[1] },
                Beds = model.Beds,
                Baths = model.Baths,
                Area = model.Area,
                Floor = model.Floor,
                Garden = model.Garden,
                Creator = model.Creator,
                AddedOn = DateTime.UtcNow,
                YearOfConstruction = model.YearOfConstruction,
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Feature = feature,

            };

            db.Properties.Add(property);
            db.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Property updated successfully!" });


        }

        [Authorize]
        [HttpPost]
        public object Edit([FromBody] PropertyInputModelDto model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new Response { Status = "Failed", Message = "Check your inputs" });
            }


            var id = model.Id;

            if (id <= 0)
            {
                return BadRequest(new Response { Status = "Failed", Message = "Id must be a positive number!" });
            }

            var prop = db.Properties.Find(id);

            if (prop == null)
            {
                return NotFound(new Response { Status = "Not Found", Message = "Not found property with this id!" });
            }

            var typeName = model.TypeName;
            var adfor = model.AdFor;
            var cityName = model.CityName;
            var countryName = model.CountryName;

            var type = db.PropertyTypes.FirstOrDefault(x => x.Name == typeName);
            var adFor = db.AdFors.FirstOrDefault(x => x.Name == adfor);
            var city = db.Cities.FirstOrDefault(x => x.Name == cityName);
            var country = db.Countries.FirstOrDefault(x => x.Name == countryName);

            if (city == null)
            {
                db.Cities.Add(new City { Name = cityName, Country = new Country { Name = countryName } });
                db.SaveChanges();
                city = db.Cities.FirstOrDefault(x => x.Name == cityName);
            }

            if (country == null)
            {
                db.Countries.Add(new Country { Name = countryName });
                db.SaveChanges();
                country = db.Countries.FirstOrDefault(x => x.Name == countryName);
            }

            city.Country = country;

            var feature = new Feature
            {
                AirConditioning = model.Feature.AirConditioning,

                Barbeque = model.Feature.Barbeque,

                Dryer = model.Feature.Dryer,

                Gym = model.Feature.Gym,

                Laundry = model.Feature.Laundry,

                Lawn = model.Feature.Lawn,

                Kitchen = model.Feature.Kitchen,

                OutdoorShower = model.Feature.OutdoorShower,

                Refrigerator = model.Feature.Refrigerator,

                Sauna = model.Feature.Sauna,

                SwimmingPool = model.Feature.SwimmingPool,

                TvCable = model.Feature.TvCable,

                Washer = model.Feature.Washer,

                Wifi = model.Feature.Wifi,

                WindowCoverings = model.Feature.WindowCoverings
            };

            prop.Type = type;
            prop.Price = model.Price;
            prop.AdFor = adFor;
            prop.Condition = model.Condition;
            prop.Address = new Address { City = city, PostCode = model.PostCode, StreetName = model.Address.Split(" ")[0], StreetNumber = model.Address.Split(" ")[1] };
            prop.Beds = model.Beds;
            prop.Baths = model.Baths;
            prop.Area = model.Area;
            prop.Floor = model.Floor;
            prop.Garden = model.Garden;
            prop.Creator = model.Creator;
            prop.AddedOn = DateTime.UtcNow;
            prop.YearOfConstruction = model.YearOfConstruction;
            prop.Title = model.Title;
            prop.Description = model.Description;
            prop.Feature = feature;
            prop.ImageUrl = model.ImageUrl;

            try
            {
                db.SaveChanges();
            }
            catch
            {
                
            }
            

            return Ok(new Response { Status = "Success", Message = "Property created successfully!" });


        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UserLike([FromBody] Like model)
        {   
            var like = new UserLike
            {
                UserId = model.Username,
                PropertyId = model.PropertyId
            };

            db.UserLikes.Add(like);
            

            await db.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Property liked successfully!" });
        }

        [Authorize]
        [HttpPost]
        public object UserUnlike([FromBody] Like model)
        {
            var like = db.UserLikes.FirstOrDefault(x => x.UserId == model.Username && x.PropertyId == model.PropertyId);

            db.UserLikes.Remove(like);

            db.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Property unliked successfully!" });
        }

        [Authorize]
        [HttpGet]
        public object GetMyLikes(string username)
        {
           
            
            var list = db.Properties
                 .Include(x => x.Address)
                 .Include(x => x.Address.City)
                 .Include(x => x.Address.City.Country)
                 .Include(x => x.Type)
                .Where(z => z.UserLikes.Any(x => x.UserId == username)).ToList();

            var result = mapper.Map<ICollection<Property>, ICollection<HomePagePropertyDto>>(list);


            return result;
        }
    }
}
