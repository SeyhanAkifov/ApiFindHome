using ApiFindHome.Data;
using ApiFindHome.Dto;
using ApiFindHome.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class PropertyController : ControllerBase
    {

        private readonly IMapper mapper;
        public PropertyController(IMapper mapper)
        {
            this.mapper = mapper;

        }
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public object GetOrderdebyPriceASC()
        {

            var property = db.Properties
                .Include(x => x.Address)
                .Include(x => x.Type).OrderBy(x => x.Price).ToArray();

            return property;
        }

        [HttpGet]
        public object Search(string type, string location, int min, int max)
        {

            var list = db.Properties
                .Include(x => x.Address)
                .Include(x => x.Address.City)
                .Include(x => x.Address.City.Country)
                .Include(x => x.Type)
                .Include(x => x.AdFor)
                .AsEnumerable()
                .Where(x =>  (type != null ? x.Type.Name == type : x != null)
                         && (location != null ? x.Address.City.Name == location : x != null)
                         && (min != 0 ? x.Price >= min : x != null)
                         && (max != 0 ? x.Price <= max : x != null))
                .ToList();

            var result = mapper.Map<ICollection<Property>, ICollection<HomePagePropertyDto>>(list);

            return result;
        }

        [HttpGet]
        public object GetOrderdebyPriceDESC()
        {

            var property = db.Properties
                .Include(x => x.Address)
                .Include(x => x.Type).OrderByDescending(x => x.Price).ToArray();

            return property;
        }

        [HttpGet]
        public object GetOrderdebyDateASC()
        {

            var property = db.Properties
                .Include(x => x.Address)
                .Include(x => x.Type).OrderBy(x => x.AddedOn).ToArray();

            return property;
        }

        [HttpGet]
        public object GetOrderdebyDateDESC()
        {

            var property = db.Properties
                .Include(x => x.Address)
                .Include(x => x.Type).OrderByDescending(x => x.AddedOn).ToArray();

            return property;
        }

        [HttpGet]
        public object GetByCity(string city)
        {

            var property = db.Properties
                .Include(x => x.Address)
                .Include(x => x.Type).Where(x => x.Address.City.Name.ToLower() == city.ToLower()).ToArray();

            return property;
        }

        [Authorize]
        [HttpGet]
        public object GetMy(string user)
        {

            var property = db.Properties
                .Include(x => x.Address)
                .Include(x => x.Type).Where(x => x.Creator.ToLower() == user.ToLower()).ToArray();

            return property;
        }

        [HttpGet]
        public object GetPropertyWithCityName(string cityName)
        {

            ICollection<Property> list = db.Properties
                 .Include(x => x.Address)
                 .Include(x => x.Address.City)
                 .Include(x => x.Address.City.Country)
                 .Include(x => x.Type)
                 .Where(x => x.Address.City.Name == cityName).ToList();

            var result = mapper.Map<ICollection<Property>, ICollection<HomePagePropertyDto>>(list);

            return result;
        }
    }
}
