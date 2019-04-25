using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CGH.Models
{
    public static class CalculationDate

    {        /// <summary>
            /// 由兩個日期計算出年齡(歲、月、天)
            /// </summary>
            public static string AGE(DateTime beginDateTime, DateTime endDateTime)
            {
                if (beginDateTime > endDateTime)
                    throw new Exception("開始時間應小於或等與結束時間！");

                /*計算出生日期到當前日期總月數*/
                int Months = endDateTime.Month - beginDateTime.Month + 12 * (endDateTime.Year - beginDateTime.Year);
                /*出生日期加總月數後，如果大於當前日期則減一個月*/
                int totalMonth = (beginDateTime.AddMonths(Months) > endDateTime) ? Months - 1 : Months;
                /*計算整年*/
                int fullYear = totalMonth / 12;
                /*計算整月*/
                int fullMonth = totalMonth % 12;
                /*計算天數*/
                DateTime changeDate = beginDateTime.AddMonths(totalMonth);
                int days = (int)(endDateTime - changeDate).TotalDays;
          


            return string.Format("{0}歲{1}月{2}天", fullYear,fullMonth,days);
        }

        public static string Seniority(DateTime beginDateTime, DateTime endDateTime)
        {
            if (beginDateTime > endDateTime)
                throw new Exception("開始時間應小於或等與結束時間！");

            /*計算出生日期到當前日期總月數*/
            int Months = endDateTime.Month - beginDateTime.Month + 12 * (endDateTime.Year - beginDateTime.Year);
            /*出生日期加總月數後，如果大於當前日期則減一個月*/
            int totalMonth = (beginDateTime.AddMonths(Months) > endDateTime) ? Months - 1 : Months;
            /*計算整年*/
            int fullYear = totalMonth / 12;
            /*計算整月*/
            int fullMonth = totalMonth % 12;
            /*計算天數*/
            DateTime changeDate = beginDateTime.AddMonths(totalMonth);
            int days = (int)(endDateTime - changeDate).TotalDays;



            return string.Format("{0}年{1}月{2}天", fullYear, fullMonth, days);
        }
    }
    }

