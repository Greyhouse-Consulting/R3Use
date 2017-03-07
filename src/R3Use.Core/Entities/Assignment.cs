using System.Collections;
using System.Collections.Generic;

namespace R3Use.Core.Entities
{
    public class Assignment
    {
        public Assignment()
        {
            Periods = new List<Period>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Period> Periods { get; set; }


        public void AddPeriod(Period period)
        {


            Periods.Add(period);
        }
    }
}