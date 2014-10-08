using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladanCRUD
{
    class ApplicationStarter
    {
        static void Main(string[] args)
        {
            MenuController menuController = new MenuController();
            menuController.doMenu();
        }
    }
}
