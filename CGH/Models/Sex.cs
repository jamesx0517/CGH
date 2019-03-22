namespace CGH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sex")]
    public partial class Sex
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sex()
        {
            Hr = new HashSet<Hr>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SexID { get; set; }
        [Display(Name = "©Ê§O")]
        [StringLength(10)]
        public string SexName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hr> Hr { get; set; }
    }
}
