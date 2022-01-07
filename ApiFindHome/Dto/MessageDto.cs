using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFindHome.Dto
{
    public class MessageDto
    {
        public int PropertyId { get; set; }
        
        public string Sender { get; set; }

        public string Recipient { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
    }
}
