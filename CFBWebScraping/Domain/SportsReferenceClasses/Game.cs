using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CFBWebScraping.SportsReference
{
    public class Game
    {
        public int GameId { get; set; }

        public string Name { get; set; }

        public DateTime GameDate { get; set; }

        public int Season { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public int WinnerId { get; set; }

        public Team AwayTeam { get; set; }

        public Team HomeTeam { get; set; }

        public List<TeamInGame> TeamsInGame { get; set; }

        public List<OffensivePlayerInGame> OffensivePlayersInGame { get; set; }

        public List<DefensivePlayerInGame> DefensivePlayersInGame { get; set; }

        public List<KickerInGame> KickersInGame { get; set; }
    }
}
