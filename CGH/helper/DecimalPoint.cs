using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CGH.helper
{
    public static class DecimalPoint
    {
        public static decimal Normalize(this HtmlHelper helper, decimal value)
        {
            return value / 1.000000000000000000000000000000000m;
        }
    }
}