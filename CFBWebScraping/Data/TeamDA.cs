using Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class TeamDA : ContextContainer
    {
        public TeamDA(string connectionString)
            : base(connectionString)
        {
        }

        public void Insert(Team team)
        {

            if ((from t in Context.Teams
                 where t.Name == team.Name
                 select t).Count() >= 1)
                return;

            Context.Teams.Add(team);
            Context.SaveChanges();

        }

        public int GetTeamID(string teamName)
        {
            return (from t in Context.Teams
                    where t.Name == teamName
                    select t.Id).FirstOrDefault();
        }
    }
}
