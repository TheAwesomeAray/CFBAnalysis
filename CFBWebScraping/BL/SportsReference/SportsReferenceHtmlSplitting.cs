using Domain;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.SportsReference
{
    public class SportsReferenceHtmlSplitting : BaseFacade
    {
        public HtmlDTO GetHtmlSegments(string html)
        {
            var typeSections = GetSplitString(html, "<tbody>\n<tr  class=\"\">");

            var htmlDTO = GetTeamHtml(typeSections[0]);
            htmlDTO.Game = GetGame(html);

            int i = 1;

            if (html.Contains("Passing"))
                htmlDTO.PassingPlayersHtml = GetPlayersHtml(typeSections[i++], 1);

            if (html.Contains("Rushing &amp; Receiving"))
            {
                htmlDTO.RushingPlayersHtml = GetPlayersHtml(typeSections[i], 2);
                htmlDTO.ReceivingPlayersHtml = GetPlayersHtml(typeSections[i++], 3);
            }
                
            if (html.Contains("Defense &amp; Fumbles"))
                htmlDTO.DefensePlayersHtml = GetPlayersHtml(typeSections[i++], 4);

            if (html.Contains("Kick &amp; Punt Returns"))
                htmlDTO.KickReturningPlayersHtml = GetPlayersHtml(typeSections[i++], 5);

            if (html.Contains("Kicking &amp; Punting"))
                htmlDTO.KickingPlayersHtml = GetPlayersHtml(typeSections[i], 6);

            return htmlDTO;
        }

        //Html for Teams
        public HtmlDTO GetTeamHtml(string html)
        {
            var htmlDTO = new HtmlDTO();
            var teamHtml = GetSplitString(html, "<table id=\"schools\" class=\"stats_table\">");

            if (teamHtml.Count() != 1)
            {
                htmlDTO.AwayTeamStats.PlaysHTML = GetTeamStats(teamHtml[1], 7);
                htmlDTO.HomeTeamStats.PlaysHTML = GetTeamStats(teamHtml[1], 8);
            }

            return htmlDTO;
        }

        public Game GetGame(string html)
        {
            var game = new Game();

            game.Name = GetGameName(html);
            game.GameDate = GetGameDate(html);
            game.AwayTeamScore = GetAwayTeamScore(html);
            game.HomeTeamScore = GetHomeTeamScore(html);
            game.Season = game.GameDate.Month < 3 ? game.GameDate.Year - 1 : game.GameDate.Year;
            game.AwayTeam.Name = GetAwayTeamName(html);
            game.HomeTeam.Name = GetHomeTeamName(html);

            return game;
        }

        public string GetGameName(string html)
        {
            return GetSubstring("<title>", " Box Score", html);
        }

        public DateTime GetGameDate(string html)
        {
            DateTime gameDate = new DateTime();
            DateTime.TryParse(GetSubstring(", ", " |", html), out gameDate);
            return gameDate;
        }

        public string GetAwayTeamName(string html)
        {
            var teamName = GetSplitString(html, "<h1><a href=\"", 1) ?? GetSplitString(html, "<h1>", 1);
            teamName = teamName.StartsWith("(") ? GetSubstring(">", "</a>", teamName) : (GetSubstring(">", "<", teamName) ?? GetFCSTeamName(teamName));

            if (teamName.Contains("&amp;"))
                teamName = teamName.Replace("&amp;", "&");

            return teamName;
        }

        public string GetFCSTeamName(string s)
        {
            s = s.Substring(0, s.IndexOf(","));
            s = s.Substring(0, s.LastIndexOf(" "));
            if (s.Contains("&amp;"))
                s = s.Replace("&amp;", "&");

            return s;
        }

        public string GetHomeTeamName(string html)
        {
            var year = DateTime.Now.Month < 3 ? DateTime.Now.Year - 1 : DateTime.Now.Year;
            var teamName = GetSplitString(html, "<h1><a href=\"", 1) ?? GetSplitString(html, "<h1>", 1);
            teamName = teamName.Substring(0, 200);
            var teamNameSplit = GetSplitString(teamName, "/cfb/schools/");
            teamName = teamNameSplit.Count() == 2 ? GetSubstring(">", "<", teamNameSplit[1]) : GetSubstring(">", "<", teamNameSplit[2]);

            if (teamName.Contains("&amp;"))
                teamName = teamName.Replace("&amp;", "&");

            return teamName;
        }

        public int GetAwayTeamScore(string html)
        {
            var scoreString = GetSplitString(html, "<h1><a href=\"", 1) ?? GetSplitString(html, "<h1>", 1);
            scoreString = GetSubstring("", ",", scoreString);
            scoreString = scoreString.Substring(scoreString.LastIndexOf(" "), scoreString.Length - scoreString.LastIndexOf(" "));

            return ParseInt(scoreString);
        }

        public int GetHomeTeamScore(string html)
        {
            var scoreString = GetSplitString(html, "<h1><a href=\"", 1) ?? GetSplitString(html, "<h1>", 1);
            scoreString = GetSubstring("</a>", "</h1>", scoreString);
            scoreString = GetSubstring("> ", "", scoreString);

            return ParseInt(scoreString);
        }

        public List<string> GetTeamNamesForPlayers(string html)
        {
            string awayTeamName = GetSubstring("", "<", html);
            string homeTeamName = GetSubstring("<th>", "", html);

            return new List<string>() { awayTeamName, homeTeamName };
        }

        public List<string> GetTeamStats(string html, int index)
        {
            if (!html.Contains("Total Yards"))
                return new List<string>();

            return Filter(TrimTeamStats(GetSplitString(html, "<td class=\"")), index);
        }

        public List<string> GetHomeTeamStats(string html)
        {
            var teamStats = GetSplitString(html, "");

            return teamStats;
        }

        public List<string> TrimTeamStats(List<string> stats)
        {
            var temp = new List<string>();
            foreach (string s in stats)
            {
                temp.Add(GetSubstring("<br>", "</p>", s));
            }

            return temp;
        }

        //Html for indidual players
        public List<StatsDTO> GetPlayersHtml(string html, int index)
        {
            var playersHtml = GetSplitString(html, "<tr  class=\"\">");
            var players = new List<StatsDTO>();

            foreach (string s in playersHtml)
            {
                var stats = GetSplitString(s, "<td align=\"right\" >");

                var player = GetBasicPlayerInformation(stats[0]);

                player.PlaysHTML =
                    TrimPlayerStats(Filter(stats, index));
                 if (player.PlaysHTML.Where(x => x != "").Count() > 0)
                    players.Add(player);
            }

            return players;
        }

        public StatsDTO GetBasicPlayerInformation(string html)
        {
            var player = new StatsDTO();
            player.PlayerName = GetSubstring(",", "\">", html) + " " + GetSubstring("csk=\"", ",", html);
            player.TeamName = GetSubstring(">", "<", GetSubstring("<td align=\"left\" ><", "", html));

            return player;
        }

        public List<string> TrimPlayerStats(List<string> stats)
        {
            var temp = new List<string>();
            foreach (string s in stats)
            {
                temp.Add(GetSubstring("", "<", s));
            }

            return temp;
        }

        public List<string> Filter(List<string> stats, int index)
        {
            switch (index)
            {
                //Passing
                case 1:
                    stats.RemoveAt(0);
                    stats.RemoveAt(2);
                    stats.RemoveAt(3);
                    stats.RemoveAt(3);
                    stats.RemoveAt(5);

                    break;

                //Rushing
                case 2:
                    stats.RemoveAt(0);
                    stats.RemoveAt(2);
                    stats.RemoveRange(3, 8);
                    break;

                //Receiving
                case 3:
                    stats.RemoveRange(0, 5);
                    stats.RemoveAt(2);
                    stats.RemoveRange(3, 4);
                    break;

                //Defense
                case 4:
                    stats.RemoveAt(0);
                    stats.RemoveAt(2);
                    stats.RemoveAt(6);
                    break;
                
                //Returning
                case 5:
                    stats.RemoveAt(0);
                    stats.RemoveAt(2);
                    stats.RemoveAt(5);
                    break;

                //Kicking
                case 6:
                    stats.RemoveAt(0);
                    stats.RemoveAt(2);
                    stats.RemoveAt(4);
                    stats.RemoveAt(4);
                    stats.RemoveAt(6);
                    break;

                //Away Team
                case 7:
                    stats.RemoveRange(0, 2);
                    stats.RemoveRange(1, 2);
                    stats.RemoveRange(2, 5);
                    stats.RemoveRange(3, 2);
                    stats.RemoveRange(4, 5);
                    stats.RemoveRange(5, 2);
                    stats.RemoveRange(6, 5);
                    stats.RemoveRange(7, 2);
                    stats.RemoveRange(8, 2);
                    stats.RemoveRange(9, 2);
                    stats.RemoveRange(10, 2);
                    stats.RemoveRange(11, 2);
                    stats.RemoveRange(12, 2);
                    stats.RemoveRange(13, 2);
                    stats.RemoveRange(14, 2);
                    stats.RemoveAt(15);
                    break;

                //Home Team
                case 8:
                    stats.RemoveRange(0, 3);
                    stats.RemoveRange(1, 2);
                    stats.RemoveRange(2, 5);
                    stats.RemoveRange(3, 2);
                    stats.RemoveRange(4, 5);
                    stats.RemoveRange(5, 2);
                    stats.RemoveRange(6, 5);
                    stats.RemoveRange(7, 2);
                    stats.RemoveRange(8, 2);
                    stats.RemoveRange(9, 2);
                    stats.RemoveRange(10, 2);
                    stats.RemoveRange(11, 2);
                    stats.RemoveRange(12, 2);
                    stats.RemoveRange(13, 2);
                    stats.RemoveRange(14, 2);
                    break;
            }

            return stats;
        }
    }
}
