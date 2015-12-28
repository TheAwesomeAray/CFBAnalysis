using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.SqlClient;

namespace Data
{
    public class PassingPlayerStatsDA : ContextContainer
    {
        public PassingPlayerStatsDA(string connectionString) 
            : base(connectionString)
        {
        }

        public void Insert(PassingPlayerGameStats player)
        {
                if ((from p in Context.PassingStats
                     where p.GameId == player.GameId && p.PlayerId == player.PlayerId
                     select p).Count() > 0)
                    return;

                Context.PassingStats.Add(player);
                Context.SaveChanges();
        }
    }
}
