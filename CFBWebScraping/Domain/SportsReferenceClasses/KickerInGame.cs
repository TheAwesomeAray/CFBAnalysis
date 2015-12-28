using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFBWebScraping.SportsReference
{
    public class KickerInGame
    {
        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public Player Player { get; set; }

        public int ExtraPointsMade { get; set; }

        public int ExtraPointAttempts { get; set; }

        public int FieldGoalsMade { get; set; }

        public int FieldGoalAttemtps { get; set; }

        public int Punts { get; set; }

        public int PuntYards { get; set; }
    }
}
