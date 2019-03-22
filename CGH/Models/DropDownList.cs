using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CGH.Models
{
    public class DropDownList
    {
        
        public SelectList GetHire()
        {
            using (CGHContext _db = new CGHContext())
            {
                var hire = _db.Hires.ToList();
                SelectList hirelistItems = new SelectList(hire, "HireID", "HireName");
                return hirelistItems;
            }
        }

        public SelectList GetTitle()
        {
            using (CGHContext _db = new CGHContext())
            {
                var title = _db.Titless.ToList();
                SelectList titlelistItems = new SelectList(title, "TitleID", "TitleName");
                return titlelistItems;
            }
        }

        public SelectList GetMarriage()
        {
            using (CGHContext _db = new CGHContext())
            {
                var marriage = _db.Marriages.ToList();
                SelectList MarriagelistItems = new SelectList(marriage, "MarriageID", "MarriageName");
                return MarriagelistItems;
            }
        }

        public SelectList GetStatus()
        {
            using (CGHContext _db = new CGHContext())
            {
                var status = _db.Statuss.ToList();
                SelectList StatuslistItems = new SelectList(status, "StatusID", "StatusName");
                return StatuslistItems;
            }
        }

        public SelectList GetWelfare()
        {
            using (CGHContext _db = new CGHContext())
            {
                var welfare = _db.Welfares.ToList();
                SelectList WelfarelistItems = new SelectList(welfare, "WelfareID", "WelfareName");
                return WelfarelistItems;
            }
        }

        public SelectList GetSend()
        {
            using (CGHContext _db = new CGHContext())
            {
                var send  = _db.Sends.ToList();
                SelectList SendlistItems = new SelectList(send, "SendID", "SendName");
                return SendlistItems;
            }
        }
        public SelectList GetRelationship()
        {
            using (CGHContext _db = new CGHContext())
            {
                var relationship = _db.Relationships.ToList();
                SelectList RelationshiplistItems = new SelectList(relationship, "RelationshipID", "RelationshipName");
                return RelationshiplistItems;
            }
        }

        public SelectList GetWorkingHR()
        {
            using (CGHContext _db = new CGHContext())
            {
                var workingHR = _db.WorkingHRs.ToList();
                SelectList WorkingHRlistItems = new SelectList(workingHR, "WorkingHRID", "WorkingHRName");
                return WorkingHRlistItems;
            }
        }

        public SelectList GetHighestedu()
        {
            using (CGHContext _db = new CGHContext())
            {
                var highestedu = _db.Highestedus.ToList();
                SelectList HighestedulistItems = new SelectList(highestedu, "HighestID", "HighestName");
                return HighestedulistItems;
            }
        }

        public SelectList GetGraduation()
        {
            using (CGHContext _db = new CGHContext())
            {
                var graduation = _db.Graduations.ToList();
                SelectList GraduationlistItems = new SelectList(graduation, "GraduationID", "GraduationName");
                return GraduationlistItems;
            }
        }

        public SelectList GetDep()
        {
            using (CGHContext _db = new CGHContext())
            {
                var dep = _db.Deps.ToList();
                SelectList DeplistItems = new SelectList(dep, "DepID", "DepName");
                return DeplistItems;
            }
        }

        public SelectList GetMilitary()
        {
            using (CGHContext _db = new CGHContext())
            {
                var military = _db.Militarys.ToList();
                SelectList MilitarylistItems = new SelectList(military, "MilitaryID", "MilitaryName");
                return MilitarylistItems;
            }
        }

        public SelectList GetCertificate()
        {
            using (CGHContext _db = new CGHContext())
            {
                var certificate = _db.Certificates.ToList();
                SelectList CertificatelistItems = new SelectList(certificate, "CertificateID", "CertificateName");
                return CertificatelistItems;
            }
        }

        public SelectList GetAdd()
        {
            using (CGHContext _db = new CGHContext())
            {
                var add = _db.Addresss.Distinct().ToList();
                var add2=add.Select(a => a.City);
                //var add = (from a in _db.Addresss select a.City).Distinct().ToList();
                SelectList AddItems = new SelectList(add2, add2, add2);
                return AddItems;

               
            }
        }
        public SelectList GetBuyerUnit()
        {
            using (CGHContext _db = new CGHContext())
            {
                var BuyerUnit = _db.BuyerUnits.ToList();
                
                
                SelectList BuyerUnitItem = new SelectList(BuyerUnit, "BuyerUniID", "BuyerUnitName");
                return BuyerUnitItem;


            }
        }
        public SelectList GetBuyCategory()
        {
            using (CGHContext _db = new CGHContext())
            {
                var BuyCategory = _db.BuyCategorys.ToList();


                SelectList BuyCategorysItem = new SelectList(BuyCategory, "BuyCategoryID", "BuyCategoryName");
                return BuyCategorysItem;


            }
        }
        
         public SelectList GetManager()
        {
            using (CGHContext _db = new CGHContext())
            {
                var Manager = _db.Hrs.Where(x=>x.Manager==true).ToList();


                SelectList ManagerItem = new SelectList(Manager, "MemberID", "Name");
                return ManagerItem;


            }
        }



    }



}