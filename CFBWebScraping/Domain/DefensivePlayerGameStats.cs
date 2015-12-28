using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DefensivePlayerGameStats
    {
        public DefensivePlayerGameStats()
        {
            Player = new Player();
        }

        [Key]
        public int Id { get; set; }

        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public bool NotAvailable { get; set; }

        //Stats
        //Tackling
        public int SoloTackles { get; set; }

        public int AssistTackles { get; set; }

        public int TacklesForLoss { get; set; }

        public int Sacks { get; set; }

        //Secondary
        public int Interceptions { get; set; }

        public int InterceptionYards { get; set; }

        public int InterceptionTouchdowns { get; set; }

        public int PassDefended { get; set; }

        //Fumbles
        public int FumbleForced { get; set; }

        public int FumbleRecovery { get; set; }

        public int FumbleYards { get; set; }

        public int FumbleTouchdown { get; set; }

        [NotMapped]
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        [NotMapped]
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }
    }
}
