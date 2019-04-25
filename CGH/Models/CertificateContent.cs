namespace CGH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("CertificateContent")]
    public partial class CertificateContent
    {
       
        [Key]
        public int ContentID { get; set; }
        [Display(Name = "單據號碼")]
        public int? ContentNO { get; set; }
        [Display(Name = "備註")]
        [StringLength(50)]
        public string Contents { get; set; }
        
        [Display(Name = "育嬰留職開始日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTimeOffset? StartDate { get; set; }
        [Display(Name = "育嬰留職結束日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTimeOffset? EndDate { get; set; }
        [Display(Name = "類別")]
        public int? Category { get; set; }
        [Display(Name = "員工工號")]
        [StringLength(10)]
        public string MemberID { get; set; }

        public virtual Certificate Certificate { get; set; }

        public static implicit operator List<object>(CertificateContent v)
        {
            throw new NotImplementedException();
        }
    }
}
