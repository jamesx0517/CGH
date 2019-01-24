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
        [StringLength(10)]
        public string MemberID { get; set; }

        [StringLength(50)]
        public string Pw { get; set; }

        public int? Status { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        [StringLength(10)]
        public string IDcardNO { get; set; }

        public DateTime? Birthday { get; set; }

        public int? Nationality { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? Probation { get; set; }

        public int? Welfare { get; set; }

        public bool? Labor { get; set; }

        public int? Hire { get; set; }

        public int? Dep { get; set; }

        public int? Title { get; set; }

        public int? Boss { get; set; }

        public bool? Manager { get; set; }

        public int? Agent { get; set; }

        public int? WorkingHR { get; set; }

        public string Remarks { get; set; }

        public string PassPortID { get; set; }

        public int? Marriage { get; set; }

        public int? Blood { get; set; }

        public int? Sex { get; set; }

        [StringLength(10)]
        public string Height { get; set; }

        [StringLength(10)]
        public string Weight { get; set; }

        public int? Military { get; set; }

        public DateTime? Retired { get; set; }

        [StringLength(10)]
        public string Health { get; set; }

        public DateTime? MEdate { get; set; }

        [StringLength(10)]
        public string Birthplace { get; set; }

        public string Email { get; set; }

        [StringLength(10)]
        public string MobilePhone { get; set; }

        [StringLength(10)]
        public string HomePhone { get; set; }

        public bool? Method { get; set; }

        public int? Send { get; set; }

        public string Address1 { get; set; }

        public int? Postal1 { get; set; }

        public string Address2 { get; set; }

        public int? Postal2 { get; set; }

        [StringLength(10)]
        public string Emergency { get; set; }

        public int? Relationship { get; set; }

        [StringLength(10)]
        public string Ephone { get; set; }
    }
}
