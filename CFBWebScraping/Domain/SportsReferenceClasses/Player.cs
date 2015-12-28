using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFBWebScraping.SportsReference
{
    public class Player
    {
        public int PlayerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public int TeamId { get; set; }

        public int PositionId { get; set; }

        public Team Team { get; set; }
    }
}
