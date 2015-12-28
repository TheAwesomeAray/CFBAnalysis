using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Player
    {
        public Player()
        {
            Team = new Team();
        }

        [Key]
        public int Id { get; set; }

        public int TeamId { get; set; }

        public int? PositionId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        [NotMapped]
        [ForeignKey("TeamId")]
        public Team Team { get; set; }
    }
}
