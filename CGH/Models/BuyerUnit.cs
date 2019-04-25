namespace CGH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("BuyerUnit")]
    [Bind(Include = "BuyerUniID,BuyerUnitName,Enable")]
    public partial class BuyerUnit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BuyerUnit()
        {
            Order = new HashSet<Order>();
        }
        
        [Key]
        public int BuyerUniID { get; set; }
        [Display(Name = "單位名稱")]
        public string BuyerUnitName { get; set; }
        [Display(Name = "是否啟動")]
        public bool Enable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
