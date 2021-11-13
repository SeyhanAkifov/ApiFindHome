using ApiFindHome.Data;
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

        [HttpGet]
        public object GetMy(string user)
        {

            var property = db.Properties
                .Include(x => x.Address)
                .Include(x => x.Type).Where(x => x.Creator.Email.ToLower() == user.ToLower()).ToArray();

            return property;
        }
    }
}
