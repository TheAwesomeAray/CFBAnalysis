using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BL
{
    public class BaseMenu : BaseFacade
    {
        public BaseMenu()
        {
            Menu = new Menu();
        }

        public Menu Menu { get; set; }
    }
}
