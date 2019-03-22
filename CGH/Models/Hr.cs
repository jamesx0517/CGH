namespace CGH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hr")]
    public partial class Hr
    {
        public int ID { get; set; }

        [Display(Name="工號")]
        [StringLength(10)]
        [Required]
        public string MemberID { get; set; }


        [Display(Name = "員工現況")]       
        public int? Status { get; set; }

        [Display(Name = "姓名")]
        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Display(Name = "身分證字號")]
        [Required]
        [StringLength(10)]
        public string IDcardNO { get; set; }

        [Display(Name = "出生日期")]
        [Required]
        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }

        [Display(Name = "國籍")]
        public int? Nationality { get; set; }

        [Display(Name = "到職日")]
        [Required]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "離職日")]       
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "試用期滿")]
        public bool Probation { get; set; }

        [Display(Name = "福利委員")]
        public int? Welfare { get; set; }

        [Display(Name = "勞方代表")]
        public bool Labor { get; set; }

        [Display(Name = "雇用別")]
        public int? Hire { get; set; }

        [StringLength(10)]
        [Display(Name = "部門")]
        public string Dep { get; set; }

        [Display(Name = "職稱")]
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "直屬長官")]
        [StringLength(50)]
        public string Boss { get; set; }

        [Display(Name = "是否為主管")]
        public bool Manager { get; set; }

        [Display(Name = "職務代理人")]
        public int? Agent { get; set; }

        [Display(Name = "工時制度")]
        public int? WorkingHR { get; set; }

        [Display(Name = "備註")]
        public string Remarks { get; set; }

        [Display(Name = "護照號碼")]
        public string PassPortID { get; set; }

        [Display(Name = "婚姻狀況")]
        public int? Marriage { get; set; }

        [Display(Name = "血型")]
        public int? Blood { get; set; }

        [Display(Name = "性別")]
        public int? Sex { get; set; }

        [Display(Name = "身高")]
        [StringLength(10)]
        public string Height { get; set; }

        [Display(Name = "體重")]
        [StringLength(10)]
        public string Weight { get; set; }

        [Display(Name = "兵役")]
        public int? Military { get; set; }

        [Display(Name = "退伍日期")]
        [Column(TypeName = "date")]
        public DateTime? Retired { get; set; }

        [Display(Name = "健康狀態")]
        [StringLength(10)]
        public string Health { get; set; }

        [Display(Name = "最近體檢日期")]
        [Column(TypeName = "date")]
        public DateTime? MEdate { get; set; }

        [Display(Name = "籍貫")]
        [StringLength(10)]
        public string Birthplace { get; set; }

        public string Email { get; set; }
        [Display(Name = "行動電話")]
        [Required]
        [StringLength(11)]
        public string MobilePhone { get; set; }

        [Display(Name = "家用電話")]
        [StringLength(11)]
        public string HomePhone { get; set; }

        [Display(Name = "執行扣計算")]
        public bool Method { get; set; }

        [Display(Name = "憑單填發方式")]
        public int? Send { get; set; }

        [Display(Name = "設籍地址")]
        public string Address1 { get; set; }

        [Display(Name = "郵遞區號")]
        public int? Postal1 { get; set; }

        [Display(Name = "聯絡地址")]
        public string Address2 { get; set; }

        [Display(Name = "聯絡地址郵遞區號")]
        public int? Postal2 { get; set; }

        [Display(Name = "緊急聯絡人")]
        [StringLength(10)]
        public string Emergency { get; set; }

        [Display(Name = "關係")]
        public int? Relationship { get; set; }

        [Display(Name = "電話")]
        [StringLength(11)]
        public string Ephone { get; set; }

        [Display(Name = "最高學歷")]
        public int? Highestedu { get; set; }
        [Display(Name = "是否畢業")]
        public int? Graduation { get; set; }



        public virtual Bool Bool { get; set; }

        public virtual Dep Dep1 { get; set; }

        public virtual Graduation Graduation1 { get; set; }

        public virtual Highestedu Highestedu1 { get; set; }

        public virtual Hire Hire1 { get; set; }

        public virtual Marriage Marriage1 { get; set; }

        public virtual Military Military1 { get; set; }

        public virtual Nationality Nationality1 { get; set; }

        public virtual Relationship Relationship1 { get; set; }

        public virtual Send Send1 { get; set; }

        public virtual Sex Sex1 { get; set; }
      
        public virtual Status Status1 { get; set; }

        public virtual Title Title1 { get; set; }

        public virtual Welfare Welfare1 { get; set; }

        public virtual WorkingHR WorkingHR1 { get; set; }
       
      

    }
}
