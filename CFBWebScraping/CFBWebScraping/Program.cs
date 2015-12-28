using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using BL;
using Domain;
using Data;
using BL.SportsReference;
using DTO;
using BL.ESPN;

namespace CFBWebScraping
{
    class Program
    {
        static void Main(string[] args)
        {
            ScrapeESPN();

            var menu = new MenuFacade();
            var userSelection = menu.ProcessMainMenu();


            if (userSelection.LoadGameData == true)
            {
                if (userSelection.GameDataSources.Contains("Sports-Reference.com"))
                    ScrapeSportsReference();

                if (userSelection.GameDataSources.Contains("ESPN.com"))
                    ScrapeESPN();
            }

            Console.WriteLine("Program Complete.  Press any key to continue.");
            Console.ReadKey();
            return;
        }

        //Scrape game data from Sports-Reference.com and store it in the database
        private static void ScrapeSportsReference()
        {
            var facade = new BaseFacade();
            var week = new WeekCycler();

            Console.WriteLine("Beginning Scpraping of Sports Reference Data!");

            while (week.Month >= 8 || week.Month < 2)
            {
                Console.WriteLine(String.Format("Scraping data for the week of {0}-{1}-{2}", week.Month, week.Day, week.Year));
                string gameListHtml = new WebClient().DownloadString
                    ("http://www.sports-reference.com/cfb/boxscores/index.cgi?month="
                    + week.Month.ToString() + "&day=" + week.Day.ToString() + "&year=" + week.Year.ToString());

                var gameStringSeparator = new string[] { "<td class=\"align_right width_third\"><a href=\"" };
                var gameLinks = gameListHtml.Split(gameStringSeparator, StringSplitOptions.None).ToList();

                gameLinks.RemoveAt(0);
                //If there are any games on the given week
                if (gameLinks.Count() > 0)
                {
                    var gameURLs = new List<string>();

                    //Cycle through all games urls for the week
                    foreach (string s in gameLinks)
                    {
                        string gamehtml = new WebClient().DownloadString("http://www.sports-reference.com/" + s.Substring(0, s.IndexOf('"')));

                        //Declare 
                        var htmlBuilder = new SportsReferenceHtmlSplitting();
                        var transformer = new TransformationFacade("SportsReferenceContext");

                        var htmlDTO = htmlBuilder.GetHtmlSegments(gamehtml);

                        transformer.CreateAndSaveGame(htmlDTO);
                    }
                }

                facade.GetNextWeek(week);
            }

            Console.WriteLine("Sports Reference Scraping Success!");
            Console.WriteLine("");
        }

        private static void ScrapeESPN()
        {
            var facade = new BaseFacade();
            var week = new WeekCycler();
            var htmlBuilder = new ESPNHtmlSplitting();

            Console.WriteLine("Beginning Scraping of ESPN data!");

            string basePageHtml = new WebClient().DownloadString
                                        (String.Format("http://espn.go.com/college-football/scoreboard/_/group/80/year/{0}/seasontype/{1}/week/{2}",
                                            week.Year, week.SeasonType, week.Week));

            week.MaxWeek = htmlBuilder.GetMaxWeek(facade.GetSplitString(facade
                    .GetSubstring("window.espn.scoreboardData", ",\"label\":\"Regular Season\"}", basePageHtml), "\"Week "));

            while (week.Week <= week.MaxWeek)
            {
                Console.WriteLine(string.Format("Scraping Data for Week {0}", week.Week));

                basePageHtml = new WebClient().DownloadString
                            (String.Format("http://espn.go.com/college-football/scoreboard/_/group/80/year/{0}/seasontype/{1}/week/{2}",
                                week.Year, week.SeasonType, week.Week));

                var gameLinks = facade.GetSplitString(facade.GetSubstring("", "</head>", basePageHtml), "http://espn.go.com/college-football/boxscore?gameId=");

                gameLinks = htmlBuilder.BuildGameLinks(gameLinks);

                //If there are any games on the given week
                if (gameLinks.Count() > 0)
                {
                    //Cycle through all games urls for the week
                    foreach (string s in gameLinks)
                    {
                        string gamehtml = new WebClient().DownloadString(s);
                        var transformer = new TransformationFacade("ESPNContext");

                        var htmlDTO = htmlBuilder.GetHtmlSegments(gamehtml);
                        transformer.CreateAndSaveGame(htmlDTO);
                    }
                }

                week.Week++;
            }

            Console.WriteLine("ESPN Scraping Success!");
            Console.WriteLine("");
        }


        //JZ - Make your method here for your gathering of poll data
    }
}