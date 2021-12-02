using ApiFindHome.Data;
using ApiFindHome.Dto;
using ApiFindHome.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiFindHome.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly IMapper mapper;
        public HomeController(IMapper mapper)
        {
            this.mapper = mapper;

        }

        ApplicationDbContext db = new ApplicationDbContext();

        [EnableCors]
        [HttpGet]
        
        public object Get()
        {
            ICollection<Property> list = db.Properties
                .Include(x => x.AdFor)
                .Include(x => x.Feature)
                .Include(x => x.Address)
                .Include(x => x.Address.City)
                .Include(x => x.Address.City.Country)
                .Include(x => x.Type).Take(6).ToList();
            
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

        [Authorize]
        [HttpGet]
        public object GetWithId(int id)
        {

            Property list = db.Properties
                .Include(x => x.AdFor)
                .Include(x => x.Feature)
                .Include(x => x.Address)
                .Include(x => x.Address.City)
                .Include(x => x.Address.City.Country)
                .Include(x => x.Type).FirstOrDefault(x => x.Id == id);

            var result = mapper.Map<Property, PropertyDetailsDto>(list);

            return result;
        }

        [HttpGet]
        public object GetTypeWithId(int id)
        {

            var type = db.PropertyTypes.FirstOrDefault(x => x.Id == id);

            return type;
        }

        [HttpGet]
        public object GetTypes()
        {

            var types = db.PropertyTypes.ToArray();

            return types;
        }

        [Authorize]
        [HttpDelete]
        public object DeleteWithId(int id)
        {

            var property = db.Properties.FirstOrDefault(x => x.Id == id);

            db.Properties.Remove(property);
            db.SaveChanges();

            return property;
        }

        [Authorize]
        [HttpPost]
        public object Post([FromBody] PropertyInputModelDto model)
        {
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
                Feature = feature,

            };

            db.Properties.Add(property);
            db.SaveChanges();

            return RedirectToAction("Get");


        }
    }
}
