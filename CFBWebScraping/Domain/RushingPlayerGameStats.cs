using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RushingPlayerGameStats
    {
        public RushingPlayerGameStats()
        {
            Player = new Player();
        }

        [Key]
        public int Id { get; set; }

        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public bool NotAvailable { get; set; }

        //Stats
        public int RushingYards { get; set; }

        public int Rushes { get; set; }

        public int FumblesLost { get; set; }

        public int RushingTouchdowns { get; set; }

        [NotMapped]
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        [NotMapped]
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }
    }
}
