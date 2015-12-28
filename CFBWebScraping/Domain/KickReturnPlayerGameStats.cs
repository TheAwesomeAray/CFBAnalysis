using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class KickReturnPlayerGameStats
    {
        public KickReturnPlayerGameStats()
        {
            Player = new Player();
        }

        [Key]
        public int Id { get; set; }

        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public bool NotAvailable { get; set; }

        //Stats
        public int KickReturns { get; set; }

        public int KickReturnYards { get; set; }

        public int KickReturnTouchdowns { get; set; }

        public int PuntReturns { get; set; }

        public int PuntReturnYards { get; set; }

        public int PuntReturnTouchdowns { get; set; }

        [NotMapped]
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        [NotMapped]
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }
    }
}
