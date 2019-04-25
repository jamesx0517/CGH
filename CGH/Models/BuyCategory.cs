namespace CGH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("BuyCategory")]
    [Bind(Include = "BuyCategoryID,BuyCategoryName,Enable")]
    public partial class BuyCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BuyCategory()
        {
            Order = new HashSet<Order>();
        }
        [Key]
        public int BuyCategoryID { get; set; }
        [Display(Name = "類別名稱")]
        public string BuyCategoryName { get; set; }
        [Display(Name = "是否啟動")]
        public bool Enable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
