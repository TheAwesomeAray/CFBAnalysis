using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class StatsDTO
    {
        public StatsDTO()
        {
            PlaysHTML = new List<string>();
        }

        public string PlayerName { get; set; }

        public string TeamName { get; set; }

        public List<string> PlaysHTML { get; set; }
    }
}
