using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Data
{
    public class RushingPlayerStatsDA : ContextContainer
    {
        public RushingPlayerStatsDA(string connectionString)
            : base(connectionString)
        {
        }

        public void Insert(RushingPlayerGameStats player)
        {
            if ((from p in Context.RushingStats
                 where p.GameId == player.GameId && p.PlayerId == player.PlayerId
                 select p).Count() > 0)
                return;

            Context.RushingStats.Add(player);
            Context.SaveChanges();
        }
    }
}
