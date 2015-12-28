using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Data
{
    public class ReceivingPlayerStatsDA : ContextContainer
    {
        public ReceivingPlayerStatsDA(string connectionString)
            : base(connectionString)
        {
        }

        public void Insert(ReceivingPlayerGameStats player)
        {
            if ((from p in Context.ReceivingStats
                 where p.GameId == player.GameId && p.PlayerId == player.PlayerId
                 select p).Count() > 0)
                return;

            Context.ReceivingStats.Add(player);
            Context.SaveChanges();
        }
    }
}
