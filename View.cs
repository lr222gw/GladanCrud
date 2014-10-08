using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            Console.Out.WriteLine("Registrera medlem");
            Console.Out.WriteLine("==========================================="); //vyn presenterar hur man lägger till
            
            Console.Out.Write("Ange Förnamn: ");
            userDataArr[0] = Console.ReadLine();
            
            Console.Out.Write("Ange Efternamn: ");
            userDataArr[1] = Console.ReadLine();
            
            Console.Out.Write("Ange Personnummer(ÅÅMMDD-XXXX): ");
            userDataArr[2] = Console.ReadLine();

            return userDataArr;
        }

        public void confirm(string messageToPrint)
        {
            Console.Out.WriteLine();
            Console.Out.WriteLine(messageToPrint);
            Console.ReadLine();
        }

        public void showAllUsers() //REMOVE OR REBUILD
        {
            Console.Clear();
            showMembersOfList(this.memberList.memberList);
        }

        public void showMembersList()
        {
            Console.Clear();
            Console.Out.WriteLine("Medlemslista");
            Console.Out.WriteLine("==============================================");
            Console.Out.WriteLine("MedlemsID   Namn                  Personnummer");
            Console.Out.WriteLine("----------------------------------------------");

            int listSize = this.memberList.memberList.Count();

            for (int i = 0; i < listSize; i++)
                Console.Out.WriteLine("{0,5}       {1,-20} {2,13}", memberList.memberList[i].getThisMemberId(), memberList.memberList[i].getUserFirstName()
                                      + " " + memberList.memberList[i].getUserLastName(), memberList.memberList[i].getSocialSecurityNumber());

            Console.Out.WriteLine("==============================================");
        }

        public void showCompactList()
        {
            Console.Clear();
            Console.Out.WriteLine("Lista medlemmar - Kompakt");
            Console.Out.WriteLine("========================================");
            Console.Out.WriteLine("MedlemsID   Namn             Antal båtar");
            Console.Out.WriteLine("----------------------------------------");
            
            int listSize = this.memberList.memberList.Count();

            for (int i = 0; i < listSize; i++)
                Console.Out.WriteLine("{0,5}       {1,-20} {2,2}", memberList.memberList[i].getThisMemberId(), memberList.memberList[i].getUserFirstName()
                                      + " " + memberList.memberList[i].getUserLastName(), memberList.memberList[i].getBoatListOfUser().Count());

            Console.Out.WriteLine("========================================");
            Console.Out.Write("Tryck 'ENTER' för att återgå till menyn");
            Console.In.ReadLine();
        }

        public void showDetailedList()
        {
            Console.Clear();
            Console.Out.WriteLine("Lista medlemmar - Fullständig");
            Console.Out.WriteLine("==========================================================================");
            Console.Out.WriteLine("MedlemsID   Namn                   Personnummer   Båttyp          Längd(m)");
            Console.Out.WriteLine("--------------------------------------------------------------------------");

            int listSize = this.memberList.memberList.Count();

            for (int i = 0; i < listSize; i++)
            {
                Console.Out.Write("{0,5}       {1,-20}   {2,-15}", memberList.memberList[i].getThisMemberId(), memberList.memberList[i].getUserFirstName()
                                      + " " + memberList.memberList[i].getUserLastName(), memberList.memberList[i].getSocialSecurityNumber());

                List<BoatModel> boatList = memberList.memberList[i].getBoatListOfUser();
                int numberOfBoats = boatList.Count();

                if(numberOfBoats > 0)
                { 
                    for (int j = 0; j < numberOfBoats; j++)
                    {
                        if (j > 0)
                            Console.Out.Write("                                                  ");

                        Console.Out.WriteLine("{0,-17} {1,3}", boatList[j].getBoatTypeString(), boatList[j].getBoatLength());
                    }
                }
                else
                {
                    Console.Out.WriteLine();
                }
            }

            Console.Out.WriteLine("==========================================================================");
            Console.Out.Write("Tryck 'ENTER' för att återgå till menyn");
            Console.In.ReadLine();
        }

        public int getUserID(string message)
        {
            Console.Out.Write(message);
            string input = Console.ReadLine();
            
            if (Regex.IsMatch(input, "^[0-9]+"))
            {
                return int.Parse(input);
            }
            else
            {
                Console.Out.WriteLine("Måste ange ett korrekt värde");
                getUserID(message);
            }

            return 0;
        }

        public void getUpdatedMemberInfo(string memberName, string socialSecurityNumber)
        {
        }
        
        public void showNavigation()
        {
            Console.Clear();
            Console.WriteLine("Gladan CRUD");
            Console.WriteLine("================================");
            Console.WriteLine("1. Registrera medlem");
            Console.WriteLine("2. Lista medlemmar - Kompakt");
            Console.WriteLine("3. Lista medlemmar - Fullständig");
            Console.WriteLine("4. Ta bort medlem");
            Console.WriteLine("5. Ändra medlem");
            Console.WriteLine("6. Visa medlem");
            Console.WriteLine("7. Registrera båt");
            Console.WriteLine("8. Ta bort båt");
            Console.WriteLine("9. Ändra båt");
            Console.WriteLine("0. Avsluta applikation");
            Console.WriteLine("================================");
            Console.Write("Ange menyalternativ: ");
        }

        public int getUserInput()
        {
            //string input = Console.ReadLine();
            //if (input) //TODO: gör säkerhetskollar...
            //{

            //}
            string input = Console.ReadLine();
            if (Regex.IsMatch(input, "^[0-9]+"))
            {
                return int.Parse(input);
            }
            else
            {
                Console.Out.WriteLine("Måste ange ett korrekt värde");
                getUserInput();
            }            

            return 0;
        }

        public string[] updateAndReturnMemberData(string[] userData)
        {
            string[] updatedUserData = new string[3];

            Console.Clear();
            Console.Out.WriteLine("Ändra medlemsuppgifter");
            Console.Out.WriteLine("====================================");
            
            //Hämtar ny data
            Console.Out.WriteLine("Lämnna tomt för att ignorera ändring");
            
            Console.Out.WriteLine("\nFörnamn: " + userData[0]);
            Console.Out.Write("Nytt Förnamn: ");
            updatedUserData[0] = Console.ReadLine().Trim();
            
            Console.Out.WriteLine("\nEfternamn: " + userData[1]);
            Console.Out.Write("Nytt Efteramn: ");
            updatedUserData[1] = Console.ReadLine().Trim();
            
            Console.Out.WriteLine("\nPersonnummer: " + userData[2]);
            Console.Out.Write("Nytt Personnummer: ");
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
            int enumSize = Enum.GetNames(typeof(BoatType)).Length;

            for (int i = 1; i <= enumSize; i++)
                Console.Out.WriteLine(i + ". " + Enum.GetName(typeof(BoatType), i));

            Console.Out.WriteLine("-------------------------");
            Console.Out.Write("Välj typ av båt: ");
        }

        public BoatType getNewBoatType()
        {
            Console.Clear();
            Console.Out.WriteLine("Registrera ny båt");
            Console.Out.WriteLine("=========================");

            presentListOfBoatTypes();
            
            return (BoatType)Enum.Parse(typeof(BoatType), Enum.GetName(typeof(BoatType), getUserInput()));
        }

        public int getNewBoatLength()
        {
            Console.Out.Write("Ange båtens längd(m): ");

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
