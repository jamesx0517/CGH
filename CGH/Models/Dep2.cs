namespace CGH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Dep2
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dep2()
        {
            Dep3 = new HashSet<Dep3>();
        }

        [StringLength(10)]
        public string Dep2ID { get; set; }

        [StringLength(10)]
        public string Dep2Name { get; set; }

        [StringLength(10)]
        public string Dep1ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dep3> Dep3 { get; set; }
    }
}
