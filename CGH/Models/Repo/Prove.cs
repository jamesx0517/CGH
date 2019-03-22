using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CGH.Models.Repo
{
    public class Prove : IProve, IDisposable
    {
        public CGHContext _db = new CGHContext();

        public HCmultiViewModel GetProveByID(int _ID)
        {
            //var resultVM = from c in _db.CertificateContents
            //                       join h in _db.Hrs on c.MemberID equals h.MemberID
            //                       where c.ContentID == _ID
            //                       select new HCmultiViewModel{CermuVM = c , HrmuVM = h};

            HCmultiViewModel resultVM = new HCmultiViewModel
            {
                HrmuVM = _db.Hrs.ToList(),
                CermuVM = _db.CertificateContents.Where(c => c.ContentID == _ID).ToList()

            };


            return (resultVM);
        }


        public void Dispose()
        {
            ((IDisposable)_db).Dispose();
        }
    }
}