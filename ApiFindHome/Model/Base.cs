using System;
using System.ComponentModel.DataAnnotations;

namespace ApiFindHome.Model
{
    public abstract class Base
    {
        [Key]
        public int Id { get; set; }

        public DateTime? CreatedDateTime { get; set; }
        public DateTime? LastModifiedDateTime { get; set; }
        public DateTime? DeletedDateTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? LastModifiedBy { get; set; }
    }
}
