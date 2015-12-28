using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.SqlClient;

namespace Data
{
    public class PlayerDA : ContextContainer
    {
        public PlayerDA(string connectionString)
            : base(connectionString)
        {
        }

        public void Insert(Player player)
        {
            if ((from p in Context.Players
                 where p.FullName == player.FullName
                 && p.TeamId == player.TeamId
                 select p).Count() >= 1)
                return;

            Context.Players.Add(player);
            Context.SaveChanges();
        }

        public int GetPlayer(string fullName)
        {
            return (from p in Context.Players
                    where p.FullName == fullName
                    select p.Id).FirstOrDefault();
        }
    }
}
