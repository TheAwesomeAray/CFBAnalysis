using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TeamGameStats
    {
        public TeamGameStats()
        {
            Team = new Team();
            Game = new Game();
            NotAvailable = false;
        }

        [Key]
        public int Id { get; set; }

        public int GameId { get; set; }

        public int TeamId { get; set; }

        public bool NotAvailable { get; set; }

        //Stats
        public int TotalYards { get; set; }

        public int TotalPlays { get; set; }

        public int TotalFirstDowns { get; set; }

        public int PenaltyFirstDowns { get; set; }

        public int Penalties { get; set; }

        public int PenaltyYards { get; set; }

        public int TurnOvers { get; set; }

        //Passing
        public int PassingYards { get; set; }

        public int PassCompletions { get; set; }

        public int PassAttempts { get; set; }

        public int PassingFirstDowns { get; set; }

        public int Interceptions { get; set; }


        //Rushing
        public int RushingYards { get; set; }

        public int Rushes { get; set; }

        public int RushingFirstDowns { get; set; }

        public int FumblesLost { get; set; }

        //objects
        [NotMapped]
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        [NotMapped]
        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
    }
}
