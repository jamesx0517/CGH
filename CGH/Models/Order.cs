namespace CGH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int ID { get; set; }
        [Display(Name = "單據編號")]
        public int? OrderID { get; set; }
        [Display(Name = "單位")]
        public int? BuyerUnit { get; set; }
        [Display(Name = "姓名")]
        [StringLength(10)]
        public string BuyerName { get; set; }
        [Display(Name = "項目名稱")]
        public int? Category { get; set; }
        [Display(Name = "數量")]
        public int? Quantity { get; set; }
        [Display(Name = "金額")]
        public decimal Money { get; set; }
        [Display(Name = "開始日期")]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "結束日期")]
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "開單日期")]
        [Column(TypeName = "date")]
        public DateTime BillingDate { get; set; }
        [Display(Name = "備註")]
        public string Remark { get; set; }
        [Display(Name = "經手人")]
        [StringLength(50)]
        public string UserID { get; set; }
        [Display(Name = "單據作廢")]
        public bool Cancel { get; set; }

        public virtual BuyCategory BuyCategory1 { get; set; }

        public virtual BuyerUnit BuyerUnit1 { get; set; }

        public virtual UsersTable UsersTable1 { get; set; }
    }
}
