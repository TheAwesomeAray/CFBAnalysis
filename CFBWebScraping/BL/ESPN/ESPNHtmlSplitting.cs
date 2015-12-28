using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Domain;

namespace BL.ESPN
{
    public class ESPNHtmlSplitting : BaseFacade
    {
        public int GetMaxWeek(List<string> html)
        {
            var maxWeek = GetSubstring("", "\"", html.Last());
            return ParseInt(maxWeek);
        }

        public List<string> BuildGameLinks(List<string> gameLinks)
        {
            int i = 0;
            gameLinks.RemoveAt(0);

            while (i < gameLinks.Count)
            {
                gameLinks[i] = string.Format("http://espn.go.com/college-football/boxscore?gameId={0}", GetSubstring("", "\"", gameLinks[i]));
                i++;
            }

            return gameLinks;
        }

        public HtmlDTO GetHtmlSegments(string gamehtml)
        {
            var htmlDTO = new HtmlDTO();
            string gameSection = GetSubstring("", "<div id=\"gamepackage-links-wrap\">", gamehtml);
            htmlDTO.Game = GetGameData(gameSection);

            string passingSection = GetSubstring(" Passing</caption>", " Rushing</caption>", gamehtml);
            htmlDTO.PassingPlayersHtml = GetPlayerStats(passingSection, htmlDTO.Game.AwayTeam.Name, htmlDTO.Game.HomeTeam.Name, (int)StatType.Passing);

            string rushingSection = GetSubstring(" Rushing</caption>", " Receiving</caption>", gamehtml);
            htmlDTO.RushingPlayersHtml = GetPlayerStats(rushingSection, htmlDTO.Game.AwayTeam.Name, htmlDTO.Game.HomeTeam.Name, (int)StatType.Rushing);

            string receivingSection = GetSubstring(" Receiving</caption>", " Interceptions</caption>", gamehtml);
            htmlDTO.ReceivingPlayersHtml = GetPlayerStats(receivingSection, htmlDTO.Game.AwayTeam.Name, htmlDTO.Game.HomeTeam.Name, (int)StatType.Receiving);

            return htmlDTO;
        }

        public Game GetGameData(string html)
        {
            var game = new Game();
            //Team Data
            game.AwayTeam.Name = GetSubstring("<title>", " vs.", html);
            game.AwayTeam.Mascot = GetSubstring("", "<", GetSplitString(html, "class=\"short-name\">", 1));
            game.HomeTeam.Name = GetSubstring("vs. ", " - Box Score", html);
            game.HomeTeam.Mascot = GetSubstring("", "<", GetSplitString(html, "class=\"short-name\">", 2));

            //Game Data
            game.GameDate = Convert.ToDateTime(GetSubstring("Box Score - ", " - ESPN</title>", html));
            game.AwayTeamScore = ParseInt(GetSubstring("", "<", GetSubstring("score icon-font-after\">", "", html)));
            game.HomeTeamScore = ParseInt(GetSubstring("", "<", GetSubstring("score icon-font-before\">", "", html)));
            game.Name = string.Format("{0} at {1}", game.AwayTeam.Name, game.HomeTeam.Name);

            return game;
        }

        public List<StatsDTO> GetPlayerStats(string html, string awayTeamName, string homeTeamName, int statType)
        {
            var stats = new List<StatsDTO>();

            if (statType == (int)StatType.Passing)
            {
                var passingStrings = GetSplitString(html, " Passing</caption>");
                stats.AddRange(GetPlayerPassingStats(passingStrings[0], passingStrings[1], awayTeamName, homeTeamName));
                return stats;
            }

            if (statType == (int)StatType.Rushing)
            {
                var rushingStrings = GetSplitString(html, " Rushing</caption>");
                stats.AddRange(GetPlayerRushingStats(rushingStrings[0], rushingStrings[1], awayTeamName, homeTeamName));
                return stats;
            }

            if (statType == (int)StatType.Receiving)
            {
                var receivingStrings = GetSplitString(html, " Receiving</caption>");
                stats.AddRange(GetPlayerReceivingStats(receivingStrings[0], receivingStrings[1], awayTeamName, homeTeamName));
                return stats;
            }


            return stats;
        }

        //TODO: Easy Refactoring here - obvious duplicate code
        public List<StatsDTO> GetPlayerPassingStats(string awayTeamPassingString, string homeTeamPassingString, string awayTeamName, string homeTeamName)
        {
            var passingStats = new List<StatsDTO>();
            var awayPlayerStrings = GetSplitString(awayTeamPassingString, "href=\"http://espn.go.com/college-football/player/_/id/");
            var homePlayerStrings = GetSplitString(homeTeamPassingString, "href=\"http://espn.go.com/college-football/player/_/id/");
            awayPlayerStrings.RemoveAt(0);
            homePlayerStrings.RemoveAt(0);

            foreach (var s in awayPlayerStrings)
            {
                var stat = new StatsDTO();
                stat.PlayerName = GetSubstring(">", "<", s);
                stat.TeamName = awayTeamName;
                stat.PlaysHTML.Add(GetSubstring("\"c-att\">", "</td><td class=\"yds\">", s));
                stat.PlaysHTML.Add(GetSubstring("/", "", stat.PlaysHTML[0]));
                stat.PlaysHTML[0] = GetSubstring("", "/", stat.PlaysHTML[0]);
                stat.PlaysHTML.Add(GetSubstring("<td class=\"yds\">", "</td><td class=\"avg\">", s));
                stat.PlaysHTML.Add(GetSubstring("<td class=\"td\">", "</td><td class=\"int\">", s));
                stat.PlaysHTML.Add(GetSubstring("<td class=\"int\">", "</td><td class=\"qbr\">", s));

                passingStats.Add(stat);
            }

            foreach (var s in homePlayerStrings)
            {
                var stat = new StatsDTO();
                stat.PlayerName = GetSubstring(">", "<", s);
                stat.TeamName = homeTeamName;
                stat.PlaysHTML.Add(GetSubstring("\"c-att\">", "</td><td class=\"yds\">", s));
                stat.PlaysHTML.Add(GetSubstring("/", "", stat.PlaysHTML[0]));
                stat.PlaysHTML[0] = GetSubstring("", "/", stat.PlaysHTML[0]);
                stat.PlaysHTML.Add(GetSubstring("<td class=\"yds\">", "</td><td class=\"avg\">", s));
                stat.PlaysHTML.Add(GetSubstring("<td class=\"td\">", "</td><td class=\"int\">", s));
                stat.PlaysHTML.Add(GetSubstring("<td class=\"int\">", "</td><td class=\"qbr\">", s));

                passingStats.Add(stat);
            }

            return passingStats;
        }

        public List<StatsDTO> GetPlayerRushingStats(string awayTeamRushingString, string homeTeamRushingString, string awayTeamName, string homeTeamName)
        {
            var rushingStats = new List<StatsDTO>();
            var awayPlayerStrings = GetSplitString(awayTeamRushingString, "href=\"http://espn.go.com/college-football/player/_/id/");
            var homePlayerStrings = GetSplitString(homeTeamRushingString, "href=\"http://espn.go.com/college-football/player/_/id/");
            awayPlayerStrings.RemoveAt(0);
            homePlayerStrings.RemoveAt(0);

            foreach (var s in awayPlayerStrings)
            {
                var stat = new StatsDTO();
                stat.PlayerName = GetSubstring(">", "<", s);
                stat.TeamName = awayTeamName;
                stat.PlaysHTML.Add(GetSubstring("<td class=\"car\">", "</td><td class=\"yds\">", s));
                stat.PlaysHTML.Add(GetSubstring("</td><td class=\"yds\">", "</td><td class=\"avg\">", s));
                stat.PlaysHTML.Add(GetSubstring("<td class=\"td\">", "</td><td class=\"long\">", s)); 

                rushingStats.Add(stat);
            }

            foreach (var s in homePlayerStrings)
            {
                var stat = new StatsDTO();
                stat.PlayerName = GetSubstring(">", "<", s);
                stat.TeamName = homeTeamName;
                stat.PlaysHTML.Add(GetSubstring("<td class=\"car\">", "</td><td class=\"yds\">", s));
                stat.PlaysHTML.Add(GetSubstring("</td><td class=\"yds\">", "</td><td class=\"avg\">", s));
                stat.PlaysHTML.Add(GetSubstring("<td class=\"td\">", "</td><td class=\"long\">", s));

                rushingStats.Add(stat);
            }

            return rushingStats;
        }

        public List<StatsDTO> GetPlayerReceivingStats(string awayTeamRushingString, string homeTeamRushingString, string awayTeamName, string homeTeamName)
        {
            var receivingStats = new List<StatsDTO>();
            var awayPlayerStrings = GetSplitString(awayTeamRushingString, "href=\"http://espn.go.com/college-football/player/_/id/");
            var homePlayerStrings = GetSplitString(homeTeamRushingString, "href=\"http://espn.go.com/college-football/player/_/id/");
            awayPlayerStrings.RemoveAt(0);
            homePlayerStrings.RemoveAt(0);

            foreach (var s in awayPlayerStrings)
            {
                var stat = new StatsDTO();
                stat.PlayerName = GetSubstring(">", "<", s);
                stat.TeamName = awayTeamName;
                stat.PlaysHTML.Add(GetSubstring("<td class=\"rec\">", "</td><td class=\"yds\">", s));
                stat.PlaysHTML.Add(GetSubstring("</td><td class=\"yds\">", "</td><td class=\"avg\">", s));
                stat.PlaysHTML.Add(GetSubstring("<td class=\"td\">", "</td><td class=\"long\">", s));

                receivingStats.Add(stat);
            }

            foreach (var s in homePlayerStrings)
            {
                var stat = new StatsDTO();
                stat.PlayerName = GetSubstring(">", "<", s);
                stat.TeamName = homeTeamName;
                stat.PlaysHTML.Add(GetSubstring("<td class=\"rec\">", "</td><td class=\"yds\">", s));
                stat.PlaysHTML.Add(GetSubstring("</td><td class=\"yds\">", "</td><td class=\"avg\">", s));
                stat.PlaysHTML.Add(GetSubstring("<td class=\"td\">", "</td><td class=\"long\">", s));

                receivingStats.Add(stat);
            }

            return receivingStats;
        }

        public enum StatType { Passing = 1, Rushing, Receiving, KickReturns, Kicking }
    }
}
