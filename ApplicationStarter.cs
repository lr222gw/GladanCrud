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
            //MemberList list = new MemberList();
            //View myView = new View(list);           
            //myView.addUser();
            //Console.Out.WriteLine("yyy");

            NavigationController nvController = new NavigationController();
            nvController.doNavigate();


        }
    }
}
