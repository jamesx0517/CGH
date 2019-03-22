namespace CGH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Dep1
    {
        [StringLength(10)]
        public string Dep1ID { get; set; }

        [StringLength(10)]
        public string Dep1Name { get; set; }
    }
}
