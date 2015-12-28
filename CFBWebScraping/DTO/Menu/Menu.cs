using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Menu
    {
        public Menu()
        {
            MenuChoices = new MenuChoices();

            MainMenuOptions = new List<string>()
            {
                "Get Game Data",
                "Get Poll Data",
                "Exit"
            };

            GameDataSources = new List<string>()
            {
                "Sports-Reference.com",
                "ESPN.com",
                "All",
                "Exit"
            };

            PollDataSources = new List<string>()
            {
                "Coming Soon!",
                "Exit"

            };

            OptionCounter = 1;
        }

        public MenuChoices MenuChoices { get; set; }

        public List<string> MainMenuOptions { get; set; }

        public List<string> GameDataSources { get; set; }

        public List<string> PollDataSources { get; set; }

        public int OptionCounter { get; set; }
    }
}
