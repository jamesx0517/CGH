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

        [Display(Name="�u��")]
        [StringLength(10)]
        [Required]
        public string MemberID { get; set; }


        [Display(Name = "���u�{�p")]       
        public int? Status { get; set; }

        [Display(Name = "�m�W")]
        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Display(Name = "�����Ҧr��")]
        [Required]
        [StringLength(10)]
        public string IDcardNO { get; set; }

        [Display(Name = "�X�ͤ��")]
        [Required]
        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }

        [Display(Name = "���y")]
        public int? Nationality { get; set; }

        [Display(Name = "��¾��")]
        [Required]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "��¾��")]       
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "�եδ���")]
        public bool Probation { get; set; }

        [Display(Name = "�֧Q�e��")]
        public int? Welfare { get; set; }

        [Display(Name = "�Ҥ�N��")]
        public bool Labor { get; set; }

        [Display(Name = "���ΧO")]
        public int? Hire { get; set; }

        [StringLength(10)]
        [Display(Name = "����")]
        public string Dep { get; set; }

        [Display(Name = "¾��")]
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "���ݪ��x")]
        [StringLength(50)]
        public string Boss { get; set; }

        [Display(Name = "�O�_���D��")]
        public bool Manager { get; set; }

        [Display(Name = "¾�ȥN�z�H")]
        public int? Agent { get; set; }

        [Display(Name = "�u�ɨ��")]
        public int? WorkingHR { get; set; }

        [Display(Name = "�Ƶ�")]
        public string Remarks { get; set; }

        [Display(Name = "�@�Ӹ��X")]
        public string PassPortID { get; set; }

        [Display(Name = "�B�ê��p")]
        public int? Marriage { get; set; }

        [Display(Name = "�嫬")]
        public int? Blood { get; set; }

        [Display(Name = "�ʧO")]
        public int? Sex { get; set; }

        [Display(Name = "����")]
        [StringLength(10)]
        public string Height { get; set; }

        [Display(Name = "�魫")]
        [StringLength(10)]
        public string Weight { get; set; }

        [Display(Name = "�L��")]
        public int? Military { get; set; }

        [Display(Name = "�h����")]
        [Column(TypeName = "date")]
        public DateTime? Retired { get; set; }

        [Display(Name = "���d���A")]
        [StringLength(10)]
        public string Health { get; set; }

        [Display(Name = "�̪����ˤ��")]
        [Column(TypeName = "date")]
        public DateTime? MEdate { get; set; }

        [Display(Name = "�y�e")]
        [StringLength(10)]
        public string Birthplace { get; set; }

        public string Email { get; set; }
        [Display(Name = "��ʹq��")]
        [Required]
        [StringLength(11)]
        public string MobilePhone { get; set; }

        [Display(Name = "�a�ιq��")]
        [StringLength(11)]
        public string HomePhone { get; set; }

        [Display(Name = "���榩�p��")]
        public bool Method { get; set; }

        [Display(Name = "�̳��o�覡")]
        public int? Send { get; set; }

        [Display(Name = "�]�y�a�}")]
        public string Address1 { get; set; }

        [Display(Name = "�l���ϸ�")]
        public int? Postal1 { get; set; }

        [Display(Name = "�p���a�}")]
        public string Address2 { get; set; }

        [Display(Name = "�p���a�}�l���ϸ�")]
        public int? Postal2 { get; set; }

        [Display(Name = "����p���H")]
        [StringLength(10)]
        public string Emergency { get; set; }

        [Display(Name = "���Y")]
        public int? Relationship { get; set; }

        [Display(Name = "�q��")]
        [StringLength(11)]
        public string Ephone { get; set; }

        [Display(Name = "�̰��Ǿ�")]
        public int? Highestedu { get; set; }
        [Display(Name = "�O�_���~")]
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
