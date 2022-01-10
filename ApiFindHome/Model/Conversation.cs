using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFindHome.Model
{
    public class Conversation
    {

        public Conversation()
        {
            this.Messages = new HashSet<Message>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        public string Recipient { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
