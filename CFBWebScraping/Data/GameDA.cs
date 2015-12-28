using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Data
{
    public class GameDA : ContextContainer
    {
        public GameDA(string connectionString)
            : base(connectionString)
        {
        }

        public void Insert(Game game)
        {
            if ((from g in Context.Games
                 where g.Name == game.Name && g.GameDate == game.GameDate
                 select g).Count() > 0)
                return;

            Context.Games.Add(game);
            Context.SaveChanges();
        }

        public int GetGameId(string gameName, DateTime gameDate)
        {
            return (from g in Context.Games
                    where g.GameDate == gameDate && g.Name == gameName
                    select g.Id).FirstOrDefault();
        }
    }
}
