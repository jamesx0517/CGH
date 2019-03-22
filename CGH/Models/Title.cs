namespace CGH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Title")]
    public partial class Title
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Title()
        {
            Hr = new HashSet<Hr>();
        }

        [StringLength(50)]
        public string TitleID { get; set; }
        [Display(Name = "Â¾ºÙ")]
        public string TitleName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Rank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hr> Hr { get; set; }
    }
}
