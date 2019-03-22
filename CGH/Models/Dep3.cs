namespace CGH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Dep3
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dep3()
        {
            Dep4 = new HashSet<Dep4>();
        }

        [StringLength(10)]
        public string Dep3ID { get; set; }

        [StringLength(10)]
        public string Dep3Name { get; set; }

        [StringLength(10)]
        public string Dep2ID { get; set; }

        public virtual Dep2 Dep2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dep4> Dep4 { get; set; }
    }
}
