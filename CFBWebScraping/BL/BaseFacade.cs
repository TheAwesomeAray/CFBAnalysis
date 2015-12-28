using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BL
{
    public class BaseFacade
    {
        public string GetSubstring(string a, string b, string s)
        {
            int start = 0;

            if (a != string.Empty)
                start = s.IndexOf(a) + a.Count();

            int length = s.Length - start;

            if (b != string.Empty)
                length = s.IndexOf(b) - start;
            if ((length + start) < start)
                return null;

            return s.Substring(start, length);
        }

        public string GetSplitString(string s, string splitter, int segment)
        {
            var arraySplitter = new string[] { splitter };

            return s.Split(arraySplitter, StringSplitOptions.None).ToList().Count() != 1 ? s.Split(arraySplitter, StringSplitOptions.None).ToList()[segment] : null;
        }

        public List<string> GetSplitString(string s, string splitter)
        {
            var arraySplitter = new string[] { splitter };

            return s.Split(arraySplitter, StringSplitOptions.None).ToList();
        }

        public int ParseInt(string s)
        {
            int x = 0;
            Int32.TryParse(s, out x);

            return x;
        }

        public WeekCycler GetNextWeek(WeekCycler week)
        {
            week.Day += 7;

            if (week.Month == 12 && week.Day > DateTime.DaysInMonth(week.Year, week.Month))
            {
                week.Day = week.Day - DateTime.DaysInMonth(week.Year, week.Month);
                week.Month = 1;
                week.Year++;
            }
            else if (week.Day > DateTime.DaysInMonth(week.Year, week.Month))
            {
                week.Day = week.Day - DateTime.DaysInMonth(week.Year, week.Month);
                week.Month++;
            }

            return week;
        }
    }
}
