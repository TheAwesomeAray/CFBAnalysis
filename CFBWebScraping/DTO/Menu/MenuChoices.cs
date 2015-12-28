using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MenuChoices
    {
        public MenuChoices()
        {
            LoadGameData = false;
            LoadPollData = false;
            GameDataSources = new List<string>();
            PollDataSources = new List<string>();
        }

        public bool LoadGameData { get; set; }

        public List<string> GameDataSources { get; set; }

        public bool LoadPollData { get; set; }

        public List<string> PollDataSources { get; set; }
    }
}
