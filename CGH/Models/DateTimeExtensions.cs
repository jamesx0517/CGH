using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CGH.Models
{
    public static class DateTimeExtensions
    {
        public static string ToFullTaiwanDate(this  DateTime datetime)
        {
            TaiwanCalendar taiwanCalendar = new TaiwanCalendar();

            return string.Format("{0}{1:00}",
            taiwanCalendar.GetYear(datetime),
            datetime.Month);
        }
    }
}