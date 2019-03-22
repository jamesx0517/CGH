using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Mvc;

namespace CGH.helper
{
    public static class DateTimeExtensions
    {   
        public static string ToFullTaiwanDate(this HtmlHelper helper, DateTime datetime)
        {   
            TaiwanCalendar taiwanCalendar = new TaiwanCalendar();
             var EndDate = datetime.ToShortDateString().ToString();
            string useless = "9999/9/9";
            if (EndDate.Equals(useless)) {
                return string.Format("  年  月  日");
            }
                return string.Format(" {0} 年 {1} 月 {2} 日",
                taiwanCalendar.GetYear(datetime),
                datetime.Month,
                datetime.Day);
        }


       
    }
}
