using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFBWebScraping.SportsReference
{
    public class DefensivePlayerInGame
    {
        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public Player Player { get; set; }


        //Tackling
        public int SoloTackles { get; set; }

        public int AssistTackles { get; set; }

        public int TacklesForLoss { get; set; }

        public int Sack { get; set; }

        //Secondary
        public int Interceptions { get; set; }

        public int InterceptionYards { get; set; }

        public int InterceptionTouchdowns { get; set; }

        public int PassDefended { get; set; }

        //Fumbles
        public int FumbleForced { get; set; }

        public int FumbleRecovery { get; set; }

        public int FumbleYards { get; set; }

        public int FumbleTouchdown { get; set; }
    }
}
