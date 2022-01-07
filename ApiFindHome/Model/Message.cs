using System;
using System.ComponentModel.DataAnnotations;

namespace ApiFindHome.Model
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public int PropertyId { get; set; }

        public Property Property { get; set; }

        public string Sender { get; set; }

        public string Recipient { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
    }
}
