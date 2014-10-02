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
        private MemberListModel memberList;//Här anropar vi modellen, ska det va så?

        public View(MemberListModel memberList) {

            this.memberList = memberList;            
            
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
            showMembersOfList(this.memberList.memberList);
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
            Console.WriteLine("4. ??????????????????Skapa en ny medlem????????????");
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
            updatedUserData = validateIfInputIsEmptySTRING(updatedUserData, userData);
            return updatedUserData;
        }
        public int[] updateAndReturnBoatData(int[] boatData)
        {

            int[] updatedBoatData = new int[2];

            //Hämtar ny data
            Console.Out.WriteLine("Ändra båtens uppgifter, lämnna tomt för att ignorera ändring");

            //Hämtar ny båttyp
            Console.Out.WriteLine("Båttyp (ange siffra) \nDin nuvarande typ är:" + (BoatType)boatData[0] + "\n");
            presentListOfBoatTypes();
            string typeInput = Console.ReadLine();
            if (typeInput == "")
            {
                updatedBoatData[0] = -1;
            }
            else { updatedBoatData[0] = int.Parse(typeInput); }

            //Hämtar ny båtlängd
            Console.Out.WriteLine("Längd: " + boatData[1]);
            string lengthInput = Console.ReadLine();
            if (lengthInput == "")
            {
                updatedBoatData[1] = -1;
            }
            else { updatedBoatData[1] = int.Parse(lengthInput); }

            //kontrollerar input
            updatedBoatData = validateIfInputIsEmptyINT(updatedBoatData, boatData);
            //returnerar
            return updatedBoatData;
        }
        
        public string[] validateIfInputIsEmptySTRING (string[] updatedData, string[] dataToCheckAgainst)
        {
           
            if (updatedData[0].Length == 0)
            {
                updatedData[0] = dataToCheckAgainst[0];
            }
            if (updatedData[1].Length == 0)
            {
                updatedData[1] = dataToCheckAgainst[1];
            }
            if (updatedData[2].Length == 0)
            {
                updatedData[2] = dataToCheckAgainst[2];
            }
            
            return updatedData;
        }
        public int[] validateIfInputIsEmptyINT(int[] updatedData, int[] dataToCheckAgainst)
        {

            if (updatedData[0] == -1)
            {
                updatedData[0] = dataToCheckAgainst[0];
            }
            if (updatedData[1] == -1)
            {
                updatedData[1] = dataToCheckAgainst[1];
            }


            return updatedData;
        }

        public void presentListOfBoatTypes()
        {
            Console.Out.WriteLine("Välj typ av båt");

            int enumSize = Enum.GetNames(typeof(BoatType)).Length;

            for (int i = 1; i <= enumSize; i++)
                Console.Out.WriteLine(i + ". " + Enum.GetName(typeof(BoatType), i));
        }

        public BoatType getNewBoatType()
        {
            presentListOfBoatTypes();
            
            return (BoatType)Enum.Parse(typeof(BoatType), Enum.GetName(typeof(BoatType), getUserInput()));
        }

        public int getNewBoatLength()
        {
            Console.Out.WriteLine("Ange båtens längd(m): ");
            return getUserInput();
        }

        public void showMembersOfList(List<MemberModel> ListToBeShown)
        {
            Console.Clear();
            for (int i = 0; i < ListToBeShown.Count(); i++)
            {
                Console.Out.WriteLine(ListToBeShown[i].toString());
            }
            
        }

        internal void listMembersBoats(List<BoatModel> memberBoatList)
        {
            Console.Clear();
            for (int i = 0; i < memberBoatList.Count; i++ )
            {
                
                Console.WriteLine(i + ": " + memberBoatList[i].toString());
            }

        }
    }
}
