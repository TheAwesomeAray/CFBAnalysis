using Data;
using Domain;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.SportsReference
{
    public class TransformationFacade : BaseFacade
    {
        public TransformationFacade(string connectionString)
        {

            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        public void CreateAndSaveGame(HtmlDTO dto)
        {
            Game game = dto.Game;
            InsertTeams(game.HomeTeam, game.AwayTeam);
            GetTeamIDs(game);
            InsertGame(game);
            GetGameId(game);
            GameStatsBuilder(dto, game);
            PopulateGamePlayers(game);
            InsertFinalGameData(game);
        }

        //Team Transformations
        public void InsertTeams(Team homeTeam, Team awayTeam)
        {
            var teamDA = new TeamDA(ConnectionString);
            teamDA.Insert(homeTeam);
            teamDA.Insert(awayTeam);
        }

        public void GetTeamIDs(Game game)
        {
            var teamDA = new TeamDA(ConnectionString);
            game.AwayTeam.Id = teamDA.GetTeamID(game.AwayTeam.Name);
            game.AwayTeamId = game.AwayTeam.Id;
            game.HomeTeam.Id = teamDA.GetTeamID(game.HomeTeam.Name);
            game.HomeTeamId = game.HomeTeam.Id;
            game.WinnerId = game.HomeTeamScore > game.AwayTeamScore ? game.HomeTeamId.Value : game.AwayTeamId.Value;
        }

        public void InsertGame(Game game)
        {
            var gameDA = new GameDA(ConnectionString);
            gameDA.Insert(game);
        }

        public void GetGameId(Game game)
        {
            var gameDA = new GameDA(ConnectionString);
            game.Id = gameDA.GetGameId(game.Name, game.GameDate);
        }

        public void GameStatsBuilder(HtmlDTO htmlDTO, Game game)
        {
            htmlDTO.HomeTeam = game.HomeTeam;
            htmlDTO.AwayTeam = game.AwayTeam;

            game.PassingGameStats = ConvertToPassingGameStats(htmlDTO);
            game.RushingGameStats = ConvertToRushingGameStats(htmlDTO);
            game.ReceivingGameStats = ConvertToReceivingGameStats(htmlDTO);
            game.DefensiveGameStats = ConvertToDefensiveGameStats(htmlDTO);
            game.KickReturnGameStats = ConvertToKickReturnGameStats(htmlDTO);
            game.KickingGameStats = ConvertToKickingGameStats(htmlDTO);
            game.TeamGameStats.Add(ConvertToTeamStats(htmlDTO.AwayTeamStats, game.AwayTeamId.Value, game));
            game.TeamGameStats.Add(ConvertToTeamStats(htmlDTO.HomeTeamStats, game.HomeTeamId.Value, game));

            SetGameForStats(game);
        }

        private void SetGameForStats(Game game)
        {
            game.PassingGameStats
                .Select(x => { x.Game = game; x.GameId = game.Id; return x; }).ToList();

            game.RushingGameStats
                .Select(x => { x.Game = game; x.GameId = game.Id; return x; }).ToList();

            game.ReceivingGameStats
                .Select(x => { x.Game = game; x.GameId = game.Id; return x; }).ToList();

            game.DefensiveGameStats
                .Select(x => { x.Game = game; x.GameId = game.Id; return x; }).ToList();

            game.KickReturnGameStats
                .Select(x => { x.Game = game; x.GameId = game.Id; return x; }).ToList();

            game.KickingGameStats
                .Select(x => { x.Game = game; x.GameId = game.Id; return x; }).ToList();

            game.TeamGameStats
                .Select(x => { x.Game = game; x.GameId = game.Id; return x; }).ToList();
        }

        public List<PassingPlayerGameStats> ConvertToPassingGameStats(HtmlDTO gameData)
        {
            var passingStats = new List<PassingPlayerGameStats>();

            if (gameData.PassingPlayersHtml.Count() == 0)
            {
                var stat = new PassingPlayerGameStats();
                stat.NotAvailable = true;
                stat.GameId = gameData.Game.Id;
                stat.PlayerId = -1;
                stat.Player.FullName = "No Data Available";

                passingStats.Add(stat);
            }
            else
            {
                foreach (var p in gameData.PassingPlayersHtml)
                {
                    var stat = new PassingPlayerGameStats();
                    stat.PassCompletions = ParseInt(p.PlaysHTML[0]);
                    stat.PassAttempts = ParseInt(p.PlaysHTML[1]);
                    stat.PassingYards = ParseInt(p.PlaysHTML[2]);
                    stat.PassingTouchdowns = ParseInt(p.PlaysHTML[3]);
                    stat.Interceptions = ParseInt(p.PlaysHTML[4]);

                    stat.Player.FullName = p.PlayerName;
                    stat.Player.FirstName = GetSubstring("", " ", p.PlayerName);
                    stat.Player.LastName = GetSubstring(" ", "", p.PlayerName);

                    InsertPlayerTeam(stat.Player, gameData, p.TeamName);

                    passingStats.Add(stat);
                }
            }

            return passingStats;
        }

        public List<RushingPlayerGameStats> ConvertToRushingGameStats(HtmlDTO gameData)
        {
            var rushingStats = new List<RushingPlayerGameStats>();

            if (gameData.RushingPlayersHtml.Count() == 0)
            {
                var stat = new RushingPlayerGameStats();
                stat.NotAvailable = true;
                stat.GameId = gameData.Game.Id;
                stat.PlayerId = -1;
                stat.Player.FullName = "No Data Available";

                rushingStats.Add(stat);
            }
            else
            {
                foreach (var r in gameData.RushingPlayersHtml)
                {
                    var stat = new RushingPlayerGameStats();
                    stat.Rushes = ParseInt(r.PlaysHTML[0]);
                    stat.RushingYards = ParseInt(r.PlaysHTML[1]);
                    stat.RushingTouchdowns = ParseInt(r.PlaysHTML[2]);

                    stat.Player.FullName = r.PlayerName;
                    stat.Player.FirstName = GetSubstring("", " ", r.PlayerName);
                    stat.Player.LastName = GetSubstring(" ", "", r.PlayerName);

                    InsertPlayerTeam(stat.Player, gameData, r.TeamName);

                    rushingStats.Add(stat);
                }
            }

            return rushingStats;
        }

        public List<ReceivingPlayerGameStats> ConvertToReceivingGameStats(HtmlDTO gameData)
        {
            var receivingStats = new List<ReceivingPlayerGameStats>();

            if (gameData.ReceivingPlayersHtml.Count() == 0)
            {
                var stat = new ReceivingPlayerGameStats();
                stat.NotAvailable = true;
                stat.GameId = gameData.Game.Id;
                stat.PlayerId = -1;
                stat.Player.FullName = "No Data Available";

                receivingStats.Add(stat);
            }
            else
            {
                foreach (var r in gameData.ReceivingPlayersHtml)
                {
                    var stat = new ReceivingPlayerGameStats();
                    stat.Receptions = ParseInt(r.PlaysHTML[0]);
                    stat.ReceivingYards = ParseInt(r.PlaysHTML[1]);
                    stat.ReceivingTouchdowns = ParseInt(r.PlaysHTML[2]);

                    stat.Player.FullName = r.PlayerName;
                    stat.Player.FirstName = GetSubstring("", " ", r.PlayerName);
                    stat.Player.LastName = GetSubstring(" ", "", r.PlayerName);

                    InsertPlayerTeam(stat.Player, gameData, r.TeamName);

                    receivingStats.Add(stat);
                }
            }

            return receivingStats;
        }


        public List<DefensivePlayerGameStats> ConvertToDefensiveGameStats(HtmlDTO gameData)
        {
            var defenseStats = new List<DefensivePlayerGameStats>();

            if (gameData.DefensePlayersHtml.Count() == 0)
            {
                var stat = new DefensivePlayerGameStats();
                stat.NotAvailable = true;
                stat.GameId = gameData.Game.Id;
                stat.PlayerId = -1;
                stat.Player.FullName = "No Data Available";

                defenseStats.Add(stat);
            }
            else
            {
                foreach (var k in gameData.DefensePlayersHtml)
                {
                    var stat = new DefensivePlayerGameStats();
                    stat.SoloTackles = ParseInt(k.PlaysHTML[0]);
                    stat.AssistTackles = ParseInt(k.PlaysHTML[1]);
                    stat.TacklesForLoss = ParseInt(k.PlaysHTML[2]);
                    stat.Sacks = ParseInt(k.PlaysHTML[3]);
                    stat.Interceptions = ParseInt(k.PlaysHTML[4]);
                    stat.InterceptionYards = ParseInt(k.PlaysHTML[5]);
                    stat.InterceptionTouchdowns = ParseInt(k.PlaysHTML[6]);
                    stat.PassDefended = ParseInt(k.PlaysHTML[7]);
                    stat.FumbleRecovery = ParseInt(k.PlaysHTML[8]);
                    stat.FumbleYards = ParseInt(k.PlaysHTML[9]);
                    stat.FumbleTouchdown = ParseInt(k.PlaysHTML[10]);
                    stat.FumbleForced = ParseInt(k.PlaysHTML[11]);

                    stat.Player.FullName = k.PlayerName;
                    stat.Player.FirstName = GetSubstring("", " ", k.PlayerName);
                    stat.Player.LastName = GetSubstring(" ", "", k.PlayerName);

                    InsertPlayerTeam(stat.Player, gameData, k.TeamName);

                    defenseStats.Add(stat);
                }
            }

            return defenseStats;
        }

        public List<KickReturnPlayerGameStats> ConvertToKickReturnGameStats(HtmlDTO gameData)
        {
            var kickReturnStats = new List<KickReturnPlayerGameStats>();

            if (gameData.KickReturningPlayersHtml.Count() == 0)
            {
                var stat = new KickReturnPlayerGameStats();
                stat.NotAvailable = true;
                stat.GameId = gameData.Game.Id;
                stat.PlayerId = -1;
                stat.Player.FullName = "No Data Available";

                kickReturnStats.Add(stat);
            }
            else
            {
                foreach (var k in gameData.KickReturningPlayersHtml)
                {
                    var stat = new KickReturnPlayerGameStats();
                    stat.KickReturns = ParseInt(k.PlaysHTML[0]);
                    stat.KickReturnYards = ParseInt(k.PlaysHTML[1]);
                    stat.KickReturnTouchdowns = ParseInt(k.PlaysHTML[2]);
                    stat.PuntReturns = ParseInt(k.PlaysHTML[3]);
                    stat.PuntReturnYards = ParseInt(k.PlaysHTML[4]);
                    stat.PuntReturnTouchdowns = ParseInt(k.PlaysHTML[5]);

                    stat.Player.FullName = k.PlayerName;
                    stat.Player.FirstName = GetSubstring("", " ", k.PlayerName);
                    stat.Player.LastName = GetSubstring(" ", "", k.PlayerName);

                    InsertPlayerTeam(stat.Player, gameData, k.TeamName);

                    kickReturnStats.Add(stat);
                }
            }

            return kickReturnStats;
        }

        public List<KickingPlayerGameStats> ConvertToKickingGameStats(HtmlDTO gameData)
        {
            var kickingStats = new List<KickingPlayerGameStats>();

            if (gameData.KickingPlayersHtml.Count() == 0)
            {
                var stat = new KickingPlayerGameStats();
                stat.NotAvailable = true;
                stat.GameId = gameData.Game.Id;
                stat.PlayerId = -1;
                stat.Player.FullName = "No Data Available";

                kickingStats.Add(stat);
            }
            else
            {
                foreach (var k in gameData.KickingPlayersHtml)
                {
                    var stat = new KickingPlayerGameStats();
                    stat.ExtraPointsMade = ParseInt(k.PlaysHTML[0]);
                    stat.ExtraPointAttempts = ParseInt(k.PlaysHTML[1]);
                    stat.FieldGoalsMade = ParseInt(k.PlaysHTML[2]);
                    stat.FieldGoalAttemtps = ParseInt(k.PlaysHTML[3]);
                    stat.Punts = ParseInt(k.PlaysHTML[4]);
                    stat.PuntYards = ParseInt(k.PlaysHTML[5]);

                    stat.Player.FullName = k.PlayerName;
                    stat.Player.FirstName = GetSubstring("", " ", k.PlayerName);
                    stat.Player.LastName = GetSubstring(" ", "", k.PlayerName);

                    InsertPlayerTeam(stat.Player, gameData, k.TeamName);

                    kickingStats.Add(stat);
                }
            }

            return kickingStats;
        }

        public TeamGameStats ConvertToTeamStats(StatsDTO gameData, int teamId, Game game)
        {
            var stat = new TeamGameStats();


            if (gameData.PlaysHTML.Count() == 0)
                stat.NotAvailable = true;
            else
            {
                stat.TotalYards = ParseInt(gameData.PlaysHTML[0]);
                stat.TotalPlays = ParseInt(gameData.PlaysHTML[1]);
                stat.PassingYards = ParseInt(gameData.PlaysHTML[2]);
                stat.PassCompletions = ParseInt(GetSubstring("", "-", gameData.PlaysHTML[3]));
                stat.PassAttempts = ParseInt(GetSubstring("-", "", gameData.PlaysHTML[3]));
                stat.RushingYards = ParseInt(gameData.PlaysHTML[4]);
                stat.Rushes = ParseInt(gameData.PlaysHTML[5]);
                stat.TotalFirstDowns = ParseInt(gameData.PlaysHTML[6]);
                stat.PassingFirstDowns = ParseInt(gameData.PlaysHTML[7]);
                stat.RushingFirstDowns = ParseInt(gameData.PlaysHTML[8]);
                stat.PenaltyFirstDowns = ParseInt(gameData.PlaysHTML[9]);
                stat.Penalties = ParseInt(gameData.PlaysHTML[10]);
                stat.PenaltyYards = ParseInt(gameData.PlaysHTML[11]);
                stat.TurnOvers = ParseInt(gameData.PlaysHTML[12]);
                stat.FumblesLost = ParseInt(gameData.PlaysHTML[13]);
                stat.Interceptions = ParseInt(gameData.PlaysHTML[14]);
            }

            stat.TeamId = teamId;

            return stat;
        }

        public void InsertPlayerTeam(Player player, HtmlDTO gameData, string teamName)
        {
            player.Team = teamName == gameData.HomeTeam.Name
                                        ? gameData.HomeTeam : gameData.AwayTeam;
            player.TeamId = player.Team.Id;
        }

        public void PopulateGamePlayers(Game game)
        {
            var playerDA = new PlayerDA(ConnectionString);

            foreach (var p in game.PassingGameStats)
            {
                playerDA.Insert(p.Player);
                p.PlayerId = playerDA.GetPlayer(p.Player.FullName);
            }

            foreach (var r in game.RushingGameStats)
            {
                playerDA.Insert(r.Player);
                r.PlayerId = playerDA.GetPlayer(r.Player.FullName);
            }

            foreach (var r in game.ReceivingGameStats)
            {
                playerDA.Insert(r.Player);
                r.PlayerId = playerDA.GetPlayer(r.Player.FullName);
            }

            foreach (var d in game.DefensiveGameStats)
            {
                playerDA.Insert(d.Player);
                d.PlayerId = playerDA.GetPlayer(d.Player.FullName);
            }

            foreach (var kr in game.KickReturnGameStats)
            {
                playerDA.Insert(kr.Player);
                kr.PlayerId = playerDA.GetPlayer(kr.Player.FullName);
            }

            foreach (var k in game.KickingGameStats)
            {
                playerDA.Insert(k.Player);
                k.PlayerId = playerDA.GetPlayer(k.Player.FullName);
            }
        }

        public void InsertFinalGameData(Game game)
        {
            var passingDA = new PassingPlayerStatsDA(ConnectionString);
            var rushingDA = new RushingPlayerStatsDA(ConnectionString);
            var receivingDA = new ReceivingPlayerStatsDA(ConnectionString);
            var defenseDA = new DefensivePlayerStatsDA(ConnectionString);
            var kickreturnDA = new KickReturningPlayerStatsDA(ConnectionString);
            var kickingDA = new KickingPlayerStatsDA(ConnectionString);
            var teamDA = new TeamStatsDA(ConnectionString);

            foreach (var p in game.PassingGameStats)
            {
                passingDA.Insert(p);
            }

            foreach (var r in game.RushingGameStats)
            {
                rushingDA.Insert(r);
            }

            foreach (var r in game.ReceivingGameStats)
            {
                receivingDA.Insert(r);
            }

            foreach (var d in game.DefensiveGameStats)
            {
                defenseDA.Insert(d);
            }

            foreach (var kr in game.KickReturnGameStats)
            {
                kickreturnDA.Insert(kr);
            }

            foreach (var k in game.KickingGameStats)
            {
                kickingDA.Insert(k);
            }

            foreach (var t in game.TeamGameStats)
            {
                teamDA.Insert(t);
            }
        }
    }
}
