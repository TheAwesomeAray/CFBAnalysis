using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HtmlDTO
    {
        public HtmlDTO()
        {
            Game = new Game();
            HomeTeam = new Team();
            AwayTeam = new Team();
            HomeTeamStats = new StatsDTO();
            AwayTeamStats = new StatsDTO();
            PassingPlayersHtml = new List<StatsDTO>();
            RushingPlayersHtml = new List<StatsDTO>();
            ReceivingPlayersHtml = new List<StatsDTO>();
            DefensePlayersHtml = new List<StatsDTO>();
            KickReturningPlayersHtml = new List<StatsDTO>();
            KickingPlayersHtml = new List<StatsDTO>();
        }

        public Game Game { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }

        public StatsDTO HomeTeamStats { get; set; }

        public StatsDTO AwayTeamStats { get; set; }

        public List<StatsDTO> PassingPlayersHtml { get; set; }

        public List<StatsDTO> RushingPlayersHtml { get; set; }

        public List<StatsDTO> ReceivingPlayersHtml { get; set; }

        public List<StatsDTO> DefensePlayersHtml { get; set; }

        public List<StatsDTO> KickReturningPlayersHtml { get; set; }

        public List<StatsDTO> KickingPlayersHtml { get; set; }
    }
}
