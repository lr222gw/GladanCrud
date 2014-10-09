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
        // Displays main menu
        public void showMenu()
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
        }

        // Shows members in a compact view
        public void showCompactList(List<MemberModel> membersList)
        {
            Console.Clear();
            Console.Out.WriteLine("Medlemslista - Kompakt");
            Console.Out.WriteLine("========================================");
            Console.Out.WriteLine("MedlemsID   Namn             Antal båtar");
            Console.Out.WriteLine("----------------------------------------");

            int listSize = membersList.Count();

            for (int i = 0; i < listSize; i++)
                Console.Out.WriteLine("{0,5}       {1,-20} {2,2}", membersList[i].getThisMemberId(), membersList[i].getUserFirstName() + " " 
                                                                   + membersList[i].getUserLastName(), membersList[i].getBoatListOfUser().Count());

            Console.Out.WriteLine("========================================");
            Console.Out.Write("Tryck 'ENTER' för att återgå till menyn");
            Console.In.ReadLine();
        }

        // Shows member / members in a detailed view
        public void showDetailedList(List<MemberModel> membersList, string listTitle)
        {
            Console.Clear();
            Console.Out.WriteLine(listTitle);
            Console.Out.WriteLine("==========================================================================");
            Console.Out.WriteLine("MedlemsID   Namn                   Personnummer   Båttyp          Längd(m)");
            Console.Out.WriteLine("--------------------------------------------------------------------------");

            int listSize = membersList.Count();

            for (int i = 0; i < listSize; i++)
            {
                Console.Out.Write("{0,5}       {1,-20}   {2,-15}", membersList[i].getThisMemberId(), membersList[i].getUserFirstName() + " " 
                                                                   + membersList[i].getUserLastName(), membersList[i].getSocialSecurityNumber());

                List<BoatModel> boatList = membersList[i].getBoatListOfUser();
                int numberOfBoats = boatList.Count();

                if (numberOfBoats > 0)
                {
                    for (int j = 0; j < numberOfBoats; j++)
                    {
                        if (j > 0)
                            Console.Out.Write("                                                  ");

                        Console.Out.WriteLine("{0,-17} {1,3}", boatList[j].getBoatType().ToString(), boatList[j].getBoatLength());
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
         
        // Shows members / boatowners depending on supplied list
        public void showMembersList(List<MemberModel> members, string listTitle = "Medlemmar")
        {
            Console.Clear();
            Console.Out.WriteLine(listTitle);
            Console.Out.WriteLine("==================================");
            Console.Out.WriteLine("MedlemsID   Namn                  ");
            Console.Out.WriteLine("----------------------------------");

            int listSize = members.Count();

            for (int i = 0; i < listSize; i++)
                Console.Out.WriteLine("{0,5}       {1,-20}",members[i].getThisMemberId(), members[i].getUserFirstName() + " " + members[i].getUserLastName());

            Console.Out.WriteLine("==================================");
        }

        // Shows a members boats
        public void showMemberBoatsList(List<BoatModel> boatList)
        {
            Console.Clear();
            Console.Out.WriteLine("Medlemmens båtar");
            Console.Out.WriteLine("==============================");
            Console.Out.WriteLine("RadID   Båttyp           Längd");
            Console.Out.WriteLine("------------------------------");

            int listSize = boatList.Count();

            for (int i = 0; i < listSize; i++)
                Console.Out.WriteLine("{0,3}     {1,-12}   {2,6}", i + 1, boatList[i].getBoatType().ToString(), boatList[i].getBoatLength());

            Console.Out.WriteLine("==============================");
        }
        
        // Returns a numeric menu value
        public int getUserChoice(string message)
        {
            Console.Out.Write(message);

            int input;

            if (int.TryParse(Console.ReadLine(), out input))
                return input;

            showIllegalInputMessage();

            return getUserChoice(message);
        }

        // Returns a numeric menu value
        public string getUserInput(string regexPattern)                                                            // Not fully implemented yet
        {
            string input = Console.In.ReadLine();

            if (Regex.IsMatch(input, regexPattern))
                return input;

            showIllegalInputMessage();

            return getUserInput(regexPattern);
        }


        // Displays an errormessage when user input is invalid
        public void showIllegalInputMessage()
        {
            Console.Out.WriteLine("Ogiltigt inamatning. Försök igen. ");
        }

        // Collect new user information
        public string[] getNewUserInformation()
        {
            string[] userDataArr = new string[3];

            Console.Clear();
            Console.Out.WriteLine("Registrera medlem");
            Console.Out.WriteLine("===========================================");

            Console.Out.Write("Ange Förnamn: ");
            userDataArr[0] = getUserInput("^([a-ö, A-Ö, -])*$"); // Felaktig regex

            Console.Out.Write("Ange Efternamn: ");
            userDataArr[1] = getUserInput("^([a-ö, A-Ö, -])*$"); // Felaktig regex

            Console.Out.Write("Ange Personnummer(ÅÅMMDD-XXXX): ");
            userDataArr[2] = getUserInput("^\\d{6}-\\d{4}$");
            
            //userDataArr[2] = Console.ReadLine();

            return userDataArr;
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
            showListOfBoatTypes();
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

        public void showListOfBoatTypes()
        {
            int enumSize = Enum.GetNames(typeof(BoatType)).Length;

            for (int i = 1; i <= enumSize; i++)
                Console.Out.WriteLine(i + ". " + Enum.GetName(typeof(BoatType), i));

            Console.Out.WriteLine("=========================");
        }
        
        public void showBoatTypes()
        {
            Console.Clear();
            Console.Out.WriteLine("Registrera ny båt");
            Console.Out.WriteLine("=========================");

            showListOfBoatTypes();
        }

        // Shows a confirmation message after action by user
        public void confirm(string messageToPrint)
        {
            Console.Out.Write("\n" + messageToPrint);
            Console.ReadLine();
        }
    }
}

//public void getUpdatedMemberInfo(string memberName, string socialSecurityNumber)
//{
//}

//public void showMembersOfList(List<MemberModel> ListToBeShown)
//{
//    Console.Clear();
//    for (int i = 0; i < ListToBeShown.Count(); i++)
//    {
//        Console.Out.WriteLine(ListToBeShown[i].toString());
//    }
//}

//internal void listMembersBoats(List<BoatModel> memberBoatList)
//{
//    Console.Clear();
//    for (int i = 0; i < memberBoatList.Count; i++)
//    {
//        Console.WriteLine(i + ": " + memberBoatList[i].toString());
//    }

//}
//public int getUserID(string message) // Sammanfoga med getUserInput eller generalisera???
//{
//    Console.Out.Write(message);
//    string input = Console.ReadLine();

//    if (Regex.IsMatch(input, "^[0-9]+"))
//    {
//        return int.Parse(input);
//    }
//    else
//    {
//        Console.Out.WriteLine("Måste ange ett korrekt värde");
//        getUserID(message);
//    }

//    return 0;
//}

//public BoatType getNewBoatType()
//{
//    Console.Clear();
//    Console.Out.WriteLine("Registrera ny båt");
//    Console.Out.WriteLine("=========================");

//    showListOfBoatTypes();

//    return (BoatType)Enum.Parse(typeof(BoatType), Enum.GetName(typeof(BoatType), getUserInput())); //getUserInput // Ändra så att int returneras och kollen görs i modellen
//}

//public int getNewBoatLength()
//{
//    Console.Out.Write("Ange båtens längd(m): ");

//    return getUserInput();                                                                        //getUserInput
//}

//public int getUserInput()
//{
//    //string input = Console.ReadLine();
//    //if (input) //TODO: gör säkerhetskollar...
//    //{

//    //}
//    string input = Console.ReadLine();
//    if (Regex.IsMatch(input, "^[0-9]+"))
//    {
//        return int.Parse(input);
//    }
//    else
//    {
//        Console.Out.WriteLine("Måste ange ett korrekt värde");
//        getUserInput();
//    }

//    return 0;
//}