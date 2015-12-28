using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Data
{
    public class KickingPlayerStatsDA : ContextContainer
    {
        public KickingPlayerStatsDA(string connectionString) : base(connectionString)
        {
        }

        public void Insert(KickingPlayerGameStats player)
        {

                if ((from k in Context.KickingStats
                     where k.GameId == player.GameId && k.PlayerId == player.PlayerId
                     select k).Count() > 0)
                    return;

                Context.KickingStats.Add(player);
                Context.SaveChanges();
        }
    }
}
