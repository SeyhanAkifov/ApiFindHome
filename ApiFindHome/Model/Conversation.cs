using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFindHome.Model
{
    public class Conversation : Base
    {

        public Conversation()
        {
            this.Messages = new HashSet<Message>();
        }

      

        [Required]
        public string Sender { get; set; }

        [Required]
        public string Recipient { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
