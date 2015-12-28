using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity;
using System.Configuration;
using System.Data.SqlClient;

namespace Data
{
    public class CFBContext : DbContext
    {
        public CFBContext(string connectionString)
            : base(connectionString) 
            {
            }

        public DbSet<PassingPlayerGameStats> PassingStats { get; set; }

        public DbSet<RushingPlayerGameStats> RushingStats { get; set; }
        
        public DbSet<ReceivingPlayerGameStats> ReceivingStats { get; set; } 

        public DbSet<DefensivePlayerGameStats> DefensiveStats { get; set; }

        public DbSet<KickReturnPlayerGameStats> KickReturnStats { get; set; }

        public DbSet<KickingPlayerGameStats> KickingStats { get; set; } 

        public DbSet<TeamGameStats> TeamStats { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Player> Players { get; set; }
    }
}
