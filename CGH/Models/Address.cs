namespace CGH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]
    public partial class Address
    {
        [Required]
        [StringLength(10)]
        public string City { get; set; }

        [Required]
        [StringLength(20)]
        public string Zone { get; set; }

        public int ID { get; set; }
    }
}
