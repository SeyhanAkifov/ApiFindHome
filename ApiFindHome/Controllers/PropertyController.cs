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
                         && (location != null ? x.Address.City.Name.ToLower() == location.ToLower() : x != null)
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

            var list = db.Properties
                .Include(x => x.Address)
                .Include(x => x.Address.City)
                 .Include(x => x.Address.City.Country)
                 .Include(x => x.Type)
                .Include(x => x.Type).Where(x => x.Creator.ToLower() == user.ToLower()).ToArray();

            var result = mapper.Map<ICollection<Property>, ICollection<HomePagePropertyDto>>(list);

            return result;
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

        [HttpGet]
        public object GetRecipientMessages(string recipient)
        {
            var messages = this.db.Messages.Where(x => x.Recipient == this.User.Identity.Name).ToList();

            return messages;
        }

        [HttpGet]
        public object GetPropertyMessages(int propertyId)
        {
            var messages = this.db.Messages.Where(x => x.PropertyId == propertyId).ToList();

            return messages;
        }

        [HttpPost]
        public object SendMessage([FromBody] MessageDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response { Status = "Failed", Message = "Check your inputs" });
            }

            var message = new Message()
            {
                Sender = model.Sender,
                Recipient = model.Recipient,
                Description = model.Description,
                PropertyId = model.PropertyId,
                Date = DateTime.UtcNow
            };

            this.db.Messages.Add(message);

            this.db.SaveChanges();

            return Ok(new Response { Status = "Success", Message = "Message sended successfully!" });
        }

        
    }
}
