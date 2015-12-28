using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ReceivingPlayerGameStats
    {
        public ReceivingPlayerGameStats()
        {
            Player = new Player();
        }

        [Key]
        public int Id { get; set; }

        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public bool NotAvailable { get; set; }

        //Receiving
        public int Receptions { get; set; }

        public int ReceivingYards { get; set; }

        public int ReceivingTouchdowns { get; set; }

        [NotMapped]
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        [NotMapped]
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }
    }
}
