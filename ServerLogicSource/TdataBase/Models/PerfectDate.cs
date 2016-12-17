using System;
using System.Collections.Generic;

namespace TdataBase.Models
{
    public class PerfectDate
    {
        /*
         * Create new object with date, perfect for given first partners date
         */
        public PerfectDate(List<Partners> partners, DateTime target)
        {
            Date = target;
            DateTime start = new DateTime(1996, 1, 1);
            partners.ForEach(x =>
            {
                try
                {
                    
                    DateTime first = new DateTime(1996, x.Month, x.Day);
                    DateTime second = new DateTime(1996, x.SecMonth, x.SecDay);

                    //Difference between sample and target
                    int diff = (first - target).Days % 182;

                    //Differences coefficient 
                    double coef = diff > 0 ? 1 - diff / 182.5 : -1 - diff / 185.2;
                    if (coef >= MaxCoef)
                    {
                        Exists = second;
                        MaxCoef = coef;
                    }

                    int targetdays = (Date - start).Days;
                    int seconddays = (second - start).Days;

                    //Counting new date
                    int daysDiff = (int)(targetdays + seconddays * coef) / 2;
                    Date = start.AddDays(daysDiff);

                }
                catch
                {
                    Console.WriteLine($"Incorrect data: {x.Month} {x.Day}, {x.SecMonth} {x.SecDay}");
                }
            });

        }
        //Perfect date
        public DateTime Date { get; set; }

        //Maximum coefficient
        public double MaxCoef { get; set; } = 0;

        //Sample date with maximum coefficient
        public DateTime Exists { get; set; }
    }
}