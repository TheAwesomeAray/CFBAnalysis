using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Game
    {
        public Game()
        {
            AwayTeam = new Team();
            HomeTeam = new Team();
            TeamGameStats = new List<TeamGameStats>();
            PassingGameStats = new List<PassingPlayerGameStats>();
            RushingGameStats = new List<RushingPlayerGameStats>();
            ReceivingGameStats = new List<ReceivingPlayerGameStats>();
            DefensiveGameStats = new List<DefensivePlayerGameStats>();
            KickReturnGameStats = new List<KickReturnPlayerGameStats>();
            KickingGameStats = new List<KickingPlayerGameStats>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime GameDate { get; set; }

        public int Season { get; set; }

        public int? HomeTeamId { get; set; }

        public int? AwayTeamId { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public int WinnerId { get; set; }

        [NotMapped]
        [ForeignKey("AwayTeamId")]
        public Team AwayTeam { get; set; }

        [NotMapped]
        [ForeignKey("HomeTeamId")]
        public Team HomeTeam { get; set; }

        public virtual List<TeamGameStats> TeamGameStats { get; set; }

        public virtual List<PassingPlayerGameStats> PassingGameStats { get; set;  }

        public virtual List<RushingPlayerGameStats> RushingGameStats { get; set; }

        public virtual List<ReceivingPlayerGameStats> ReceivingGameStats { get; set; }

        public virtual List<DefensivePlayerGameStats> DefensiveGameStats { get; set; }

        public virtual List<KickReturnPlayerGameStats> KickReturnGameStats { get; set; }

        public virtual List<KickingPlayerGameStats> KickingGameStats { get; set; }
    }
}
