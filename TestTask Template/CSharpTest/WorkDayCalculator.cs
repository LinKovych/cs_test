using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            DateTime[] period = new DateTime[dayCount];
            period[0] = startDate;

            for (int i = 1; i < dayCount; i++)
            {
                period[i] = period[i - 1].AddDays(1);
            }

            int index;
            if (weekEnds != null)
            {
                for (int i = 0; i < weekEnds.Length; i++)
                {
                    if (weekEnds[i].StartDate > weekEnds[i].EndDate)
                    {
                        DateTime temp = weekEnds[i].StartDate;
                        weekEnds[i].StartDate = weekEnds[i].EndDate;
                        weekEnds[i].EndDate = temp;
                    }

                    index = Array.IndexOf(period, weekEnds[i].StartDate);
                    if (index != -1)
                    {
                        period[index] = weekEnds[i].EndDate.AddDays(1);
                        for (int k = index + 1; k < period.Length; k++)
                        {
                            period[k] = period[k - 1].AddDays(1);
                        }
                    }
                }
            }
            return period[dayCount - 1];
        }
    }
}
