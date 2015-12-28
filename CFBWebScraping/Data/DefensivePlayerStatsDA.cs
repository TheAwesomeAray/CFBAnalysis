using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Data
{
    public class DefensivePlayerStatsDA : ContextContainer
    {
        public DefensivePlayerStatsDA(string connectionString) : base(connectionString)
        {
        }

        public void Insert(DefensivePlayerGameStats player)
        {
            if ((from d in Context.DefensiveStats
                 where d.GameId == player.GameId && d.PlayerId == player.PlayerId
                 select d).Count() > 0)
                return;

            Context.DefensiveStats.Add(player);
            Context.SaveChanges();
        }
    }
}
