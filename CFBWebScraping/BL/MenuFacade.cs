using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BL
{
    public class MenuFacade : BaseMenu
    {
        public MenuChoices ProcessMainMenu()
        {
            PrintMainMenu();
            ProcessMenuChoice(Console.ReadLine(), 1);
            ClearRange(3, 15);
            return Menu.MenuChoices;
        }

        public async void PrintMainMenu()
        {
            WriteHeader();
            await Task.Delay(1250);
            Console.WriteLine("\t\t\tWelcome to the CFB Data Scraper!");
            WriteHeader();
            await Task.Delay(1250);
            TypeLine("Please select an option below:", 5);
            await Task.Delay(500);

            foreach (string s in Menu.MainMenuOptions)
            {
                TypeLine(string.Format("{0}. {1}", Menu.OptionCounter.ToString(), s), 1);
                await Task.Delay(300);
                Menu.OptionCounter++;
            }

            Console.Write("> ");
        }

        public async void DataSourceMenu()
        {
            TypeLine("Here is the list of supported datasources.", 1);
            await Task.Delay(700);
            TypeLine("Please select from which data sources you would like to load data from the list below:", 1);
            await Task.Delay(1500);
            Menu.OptionCounter = 1;
            foreach (string s in Menu.GameDataSources)
            {
                TypeLine(string.Format("{0}. {1}", Menu.OptionCounter.ToString(), s), 1);
                await Task.Delay(s.Length * 28);
                Menu.OptionCounter++;
            }

            Console.Write("> ");
        }

        public async void WriteHeader()
        {
            int i = 0;
            while (i < 80)
            {
                Console.Write("-");
                await Task.Delay(5);
                i++;
            }
        }

        public async void TypeLine(string line, int speed)
        {
            foreach (char c in line)
            {
                Console.Write(c);
                await Task.Delay(speed);
            }

            Console.WriteLine();
        }

        public void ClearRange(int startPoint, int length)
        {
            while (startPoint < startPoint + length - 1)
            {
                int counter = 0;
                Console.SetCursorPosition(0, startPoint + length - 1);
                while (counter < 8)
                {
                    Console.Write("          ");
                    counter++;
                }

                length--;
            }

            Console.SetCursorPosition(0, startPoint);
        }

        public void ProcessMenuChoice(string input, int menu)
        {
            if (menu == 1)
            {
                while (ParseInt(input) >= Menu.MainMenuOptions.Count() + 1 || ParseInt(input) == 0)
                {
                    ClearRange(6, 1);
                    TypeLine("Please enter a valid option", 1);
                    Console.Write("> ");
                    input = Console.ReadLine();
                }

                if (input == "1")
                {
                    GetGameDataSourceChoice();
                }
                else if (input == "2")
                {
                    Menu.MenuChoices.LoadPollData = true;
                    ClearRange(4, 4);
                    DataSourceMenu();
                }
                else if (input == "3")
                {
                    Menu.MenuChoices.LoadGameData = true;
                    Menu.MenuChoices.LoadPollData = true;
                    DataSourceMenu();
                    string choice = Console.ReadLine();
                }
            }

            return;
        }

        public void GetGameDataSourceChoice()
        {
            DataSourceMenu();
            int choice = ParseInt(Console.ReadLine());
            while (choice == 0 ||  choice > Menu.GameDataSources.Count + 1)
            {
                ClearRange(4, 4);
                TypeLine("Please enter a valid integer listed below:", 1);
                DataSourceMenu();
                choice = ParseInt(Console.ReadLine());
            }

            if (choice == Menu.GameDataSources.Count)
                return;
            else
                ProcessGameDataSource(choice);
        }

        public void ProcessGameDataSource(int choice)
        {
            Menu.MenuChoices.LoadGameData = true;

            if (choice == Menu.GameDataSources.Count - 1)
                Menu.MenuChoices.GameDataSources = Menu.GameDataSources;
            else
                Menu.MenuChoices.GameDataSources.Add(Menu.GameDataSources[choice - 1]);
        }

        public void ProcessPollDataSource(int choice)
        {
            if (choice == Menu.PollDataSources.Count + 1)
                Menu.MenuChoices.PollDataSources = Menu.GameDataSources;
            else
                Menu.MenuChoices.PollDataSources.Add(Menu.PollDataSources[choice - 1]);
        }
        
    }
}
