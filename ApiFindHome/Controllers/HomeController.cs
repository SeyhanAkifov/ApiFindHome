using ApiFindHome.Data;
using ApiFindHome.Dto;
using ApiFindHome.Model;
using AutoMapper;
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
        //[Authorize]
        public object Get()
        {
            ICollection<Property> list = db.Properties
                .Include(x => x.Address)
                .Include(x => x.Address.City)
                .Include(x => x.Address.City.Country)
                .Include(x => x.Type).ToList();
            
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

            var result = new
            {
                Cities = cityList.Select(x => new
                {
                    City = x.Name,
                    Properties = list.Where(y => y.Address.City.Name == x.Name).Count()
                }).OrderByDescending(x => x.Properties).Take(4)
            };
            

            return result;

        }

        [HttpGet]
        public object GetWithId(int id)
        {

            var property = db.Properties
                .Include(x => x.Address)
                .Include(x => x.Type).FirstOrDefault(x => x.Id == id);

            return property;
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

        [HttpDelete]
        public object DeleteWithId(int id)
        {

            var property = db.Properties.FirstOrDefault(x => x.Id == id);

            db.Properties.Remove(property);
            db.SaveChanges();

            return property;
        }

        [HttpPost]
        public object Post()
        {
            var typeName = "House";
            var adfor = "Sale";
            var cityName = "Berlin";
            var countryName = "Germany";

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

            var property = new Property
            {
                Type = type,
                Price = 300000,
                AdFor = adFor,
                Condition = "Renovated House",
                Address = new Address { City = city , PostCode = "85664", StreetName = "Kanzleiweg", StreetNumber = "10" },
                Beds = 3,
                Baths = 2,
                Area = 150,
                Floor = 1,
                Garden = true,
                //Creator = this.User,
                AddedOn = DateTime.UtcNow,
                YearOfConstruction = 1951,

            };

            db.Properties.Add(property);
            db.SaveChanges();

            return RedirectToAction("Get");


        }
    }
}
