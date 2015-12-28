using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class WeekCycler
    {
        public WeekCycler()
        {
            //Sports Reference
            Day = 1;
            Month = 9;
            Year = DateTime.Now.Year;

            //ESPN
            Week = 1;
            MaxWeek = 15;
            SeasonType = 2;
        }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        //ESPN
        public int Week { get; set; }

        public int MaxWeek { get; set; }

        public int SeasonType { get; set; }
    }
}
