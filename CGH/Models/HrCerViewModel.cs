using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CGH.Models
{
    public class HrCerViewModel
    {
        public Hr HrVM { get; set; }
        public IList<CertificateContent> CerVM { get; set; }
    }
}