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

            if (dayCount == 0)
                return startDate;

            period[0] = startDate;

            // array of working days
            for (int i = 1; i < dayCount; i++)
            {
                period[i] = period[i - 1].AddDays(1);
            }

            int index;
            if (weekEnds != null)
            {
                for (int i = 0; i < weekEnds.Length; i++)
                {
                    // if endDate earlier than startDate
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
                        for (int j = index + 1; j < period.Length; j++)
                        {
                            period[j] = period[j - 1].AddDays(1);
                        }
                    }

                    else
                    {
                        index = Array.IndexOf(period, weekEnds[i].EndDate);
                        if (index != -1)
                        {
                            period[0] = weekEnds[i].EndDate.AddDays(1);
                            for (int j = 1; j < dayCount; j++)
                            {
                                period[j] = period[j - 1].AddDays(1);
                            }
                        }
                    }

                    if (weekEnds[i].StartDate < period[0] && weekEnds[i].EndDate > period[dayCount - 1])
                    {
                        period[0] = weekEnds[i].EndDate.AddDays(1);
                        for (int j = 1; j < dayCount; j++)
                        {
                            period[j] = period[j - 1].AddDays(1);
                        }
                    }
                }
            }

            return period[dayCount - 1];
        }
    }
}
