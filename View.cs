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
        private BoatListModel boatList;

        public View(MemberListModel memberList, BoatListModel boatList) {

            this.memberList = memberList;
            this.boatList = boatList;
            
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

        public void showAllBoats()
        {
            Console.Clear();

            for (int i = 0; i < this.boatList.boatList.Count(); i++)
            {
                Console.Out.WriteLine(this.boatList.boatList[i].toString());
            }
        }


        public int getUserID(string message)
        {

            Console.Out.WriteLine(message);
            int input = int.Parse(Console.ReadLine());

            return input;

        }

        public void getUpdatedMemberInfo(string memberName, string socialSecurityNumber)
        {
            

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

        public string[] updateAndReturnMemberData(string[] userData)
        {
            
            string[] updatedUserData = new string[3];

            //Hämtar ny data
            Console.Out.WriteLine("Ändra användaruppgifter, lämnna tomt för att ignorera ändring");
            Console.Out.WriteLine("Förnamn: " + userData[0]);
            updatedUserData[0] = Console.ReadLine().Trim();
            Console.Out.WriteLine("Efternamn: " + userData[1]);
            updatedUserData[1] = Console.ReadLine().Trim();
            Console.Out.WriteLine("Personnummer: " + userData[2]);
            updatedUserData[2] = Console.ReadLine().Trim();
            //SendKeys.SendWait("hello");
            //SendKeys
            //Console.In.Read();
            //Console.Out.Write(userData[0]);


            //Om datan som angivits är av längden 0 så ska den gamla datan användas.
            if (updatedUserData[0].Length == 0)
            {
                updatedUserData[0] = userData[0];
            }
            if (updatedUserData[1].Length == 0)
            {
                updatedUserData[1] = userData[1];
            }
            if (updatedUserData[2].Length == 0)
            {
                updatedUserData[2] = userData[2];
            }

            return updatedUserData;
        }

        public BoatType getNewBoatType()
        {
            Console.Out.WriteLine("Välj typ av båt");

            int size = Enum.GetNames(typeof(BoatType)).Length;

            for (int i = 1; i <= size; i++)
                Console.Out.WriteLine(i + ". " + Enum.GetName(typeof(BoatType), i));

            switch (getUserInput())
	        {
                case 1:
                    return BoatType.Segelbåt;
                case 2:
                    return BoatType.Motorseglare;
                case 3:
                    return BoatType.Motorbåt;
                case 4:
                    return BoatType.Kajak_Kanot;
                case 5:
                    return BoatType.Övrigt;
	        }

            // Fulkod - Borde gå att returnera ett Enum baserat på konstantens värde
            return BoatType.Övrigt;
            
        }

        public int getNewBoatLength()
        {
            Console.Out.WriteLine("Ange båtens längd(m): ");
            return getUserInput();
        }
    }
}
