namespace CGH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Dep4
    {
        [StringLength(10)]
        public string Dep4ID { get; set; }

        [StringLength(10)]
        public string Dep4Name { get; set; }

        [StringLength(10)]
        public string Dep3ID { get; set; }

        public virtual Dep3 Dep3 { get; set; }
    }
}
