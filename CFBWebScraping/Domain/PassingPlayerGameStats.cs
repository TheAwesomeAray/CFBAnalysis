using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PassingPlayerGameStats
    {
        public PassingPlayerGameStats()
        {
            Player = new Player();
        }

        [Key]
        public int Id { get; set; }

        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public bool NotAvailable { get; set; }

        //Stats
        public int PassingYards { get; set; }

        public int PassCompletions { get; set; }

        public int PassAttempts { get; set; }

        public int Interceptions { get; set; }

        public int PassingTouchdowns { get; set; }

        [NotMapped]
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        [NotMapped]
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }
    }
}
