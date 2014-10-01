using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladanCRUD
{
    class View
    {
        //View
        private MemberListModel memberList;

        public View(MemberListModel list) {

            this.memberList = list;
            
        }
        


        public string[] getNewUserInformation()
        {
            string[] userDataArr = new string[3];

            Console.Clear();
            //vyn presenterar hur man lägger till
            Console.Out.WriteLine("Ange Förnamn :");
            
            userDataArr[0] = Console.ReadLine();
            Console.Out.WriteLine("Ange Efternamn :");
            userDataArr[1] = Console.ReadLine();
            Console.Out.WriteLine("Ange Personnummer :");
            userDataArr[2] = Console.ReadLine();

            return userDataArr;
        }

        public void confirm(string messageToPrint)
        {
            Console.Out.WriteLine(messageToPrint);
            Console.ReadLine();
        }



        public void showAllUsers()
        {
            Console.Clear();

            for (int i = 0; i < this.memberList.memberList.Count(); i++ )
            {
                Console.Out.WriteLine(this.memberList.memberList[i].toString());
            }
        }

        public int deleteUserByID()
        {

            Console.Out.WriteLine("Ange användarens ID, för att ta bort användaren");
            int input = int.Parse(Console.ReadLine());

            return input;

        }


        public void showNavigation()
        {
            Console.Clear();
            Console.WriteLine("Gladan CRUD");
            Console.WriteLine("=================================================");
            Console.WriteLine("1. Registrera medlem");
            Console.WriteLine("2. Lista medlemmar - Kompakt");
            Console.WriteLine("3. Lista medlemmar - Fullständig");
            Console.WriteLine("4. Skapa en ny medlem");
            Console.WriteLine("5. Ta bort medlem");
            Console.WriteLine("6. Ändra medlem");
            Console.WriteLine("8. Visa medlem");
            Console.WriteLine("9. Registrera båt");
            Console.WriteLine("10. Ta bort båt");
            Console.WriteLine("11. Ändra båt");
            Console.WriteLine("0. Avsluta applikation");
        
        }
        public int getUserInput()
        {
            string input = Console.ReadLine();
            //if (input) //TODO: gör säkerhetskollar...
            //{

            //}

            int refinedInput = int.Parse(input);

            return refinedInput;
        }
    }
}
