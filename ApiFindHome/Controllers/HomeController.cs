using ApiFindHome.Data;
using ApiFindHome.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        ApplicationDbContext db = new ApplicationDbContext();

        [EnableCors]
        [HttpGet]
        [Authorize]
        public object Get()
        {
            var list = db.Properties
                .Include(x => x.Address)
                .Include(x => x.Type).ToArray();

            return list;
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
        public object GetTypes(int id)
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
            var property = new Property
            {
                Type = new PropertyType { Name = "House" },
                Price = 30000,
                AdFor = new AdFor { Name = "Sale" },
                Condition = "Renovated House",
                Address = new Address { City = new City { Name = "Munich" }, PostCode = "85664", StreetName = "Kanzleiweg", StreetNumber = "10" },
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

            return property;


        }
    }
}
