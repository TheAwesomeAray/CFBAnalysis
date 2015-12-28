using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class KickingPlayerGameStats
    {
        public KickingPlayerGameStats()
        {
            Player = new Player();
        }

        [Key]
        public int id { get; set; }

        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public bool NotAvailable { get; set; }

        //Stats
        public int ExtraPointsMade { get; set; }

        public int ExtraPointAttempts { get; set; }

        public int FieldGoalsMade { get; set; }

        public int FieldGoalAttemtps { get; set; }

        public int Punts { get; set; }

        public int PuntYards { get; set; }

        [NotMapped]
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        [NotMapped]
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }
    }
}
