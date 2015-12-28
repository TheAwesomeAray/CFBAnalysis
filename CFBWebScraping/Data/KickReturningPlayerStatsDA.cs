using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Data
{
    public class KickReturningPlayerStatsDA : ContextContainer
    {
        public KickReturningPlayerStatsDA(string connectionString) : base(connectionString)
        {
        }

        public void Insert(KickReturnPlayerGameStats player)
        {
            if ((from kr in Context.KickReturnStats
                 where kr.GameId == player.GameId && kr.PlayerId == player.PlayerId
                 select kr).Count() > 0)
                return;

            Context.KickReturnStats.Add(player);
            Context.SaveChanges();
        }
    }
}
