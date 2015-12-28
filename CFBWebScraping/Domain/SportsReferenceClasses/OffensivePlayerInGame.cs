using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFBWebScraping.SportsReference
{
    public class OffensivePlayerInGame
    {
        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public Player Player { get; set; }

        //Passing
        public int PassingYards { get; set; }

        public int PassCompletions { get; set; }

        public int PassAttempts { get; set; }

        public int Interceptions { get; set; }

        public int PassingTouchdowns { get; set; }

        //Rushing
        public int RushingYards { get; set; }

        public int Rushes { get; set; }

        public int FumblesLost { get; set; }

        public int RushingTouchdowns { get; set; }

        //Receiving
        public int Receptions { get; set; }

        public int ReceivingYards { get; set; }

        public int ReceivingTouchdowns { get; set; }

        //Special Teams
        //Returns
        public int KickReturns { get; set; }

        public int KickReturnYards { get; set; }

        public int KickReturnTouchdowns { get; set; }

        public int PuntReturns { get; set; }

        public int PuntReturnYards { get; set; }

        public int PuntReturnTouchdowns { get; set; }
    }
}
