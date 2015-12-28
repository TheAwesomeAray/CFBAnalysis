using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Data
{
    public class TeamStatsDA : ContextContainer
    {
        public TeamStatsDA(string connectionString)
            : base(connectionString)
        {
        }

        public void Insert(TeamGameStats team)
        {
            if ((from t in Context.TeamStats
                 where t.GameId == team.GameId && t.TeamId == team.TeamId
                 select t).Count() > 0)
                return;

            Context.TeamStats.Add(team);
            Context.SaveChanges();
        }
    }
}
