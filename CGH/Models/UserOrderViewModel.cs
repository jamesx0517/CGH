using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CGH.Models
{
    public class UserOrderViewModel
    {
        public UsersTable UVM { get; set; }
        public IList <Order> OVM { get; set; }
    }
    
}