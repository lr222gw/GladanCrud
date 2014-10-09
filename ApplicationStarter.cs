using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladanCRUD.controller;

namespace GladanCRUD
{
    class ApplicationStarter
    {
        static void Main(string[] args)
        {
            MenuController mC = new MenuController();
            mC.doMenu();
        }
    }
}
