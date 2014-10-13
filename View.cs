using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GladanCRUD.model;

namespace GladanCRUD.view
{
    class View
    {
        // Visa huvudmenyn
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

        // Visa kompakt medlemslista
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

        // Visa fullständig medlemslista
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
         
        // Visa medlemslista / båtägarlista beroende på indata
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

        // Visa en medlems båtar
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
        
        // Hämta användares val
        public int getUserChoice(string message)
        {
            Console.Out.Write(message);

            int input;

            if (int.TryParse(Console.ReadLine(), out input))
                return input;

            showIllegalInputMessage();

            return getUserChoice(message);
        }

        // Hämta indata från användaren
        public string getUserInput(string regexPattern)
        {
            string input = Console.In.ReadLine().Trim();            

            if (Regex.IsMatch(input, regexPattern))
                return input;

            showIllegalInputMessage();

            return getUserInput(regexPattern);
        }

        // Hämta uppdaterad data
        public int getUpdatedValue(string message)
        {
            Console.Out.Write(message);

            string inputString = Console.In.ReadLine().Trim();
            
            if(String.IsNullOrEmpty(inputString))
                return -1;
            
            int input;

            if (int.TryParse(inputString, out input))
                return input;

            showIllegalInputMessage();

            return getUpdatedValue(message);
        }

        // Visa felmeddelande vid ogiltig inmatning
        public void showIllegalInputMessage()
        {
            Console.Out.WriteLine("Ogiltigt inmatning. Försök igen. ");
        }

        // Samla in ny medlemsinformation
        public string[] getNewUserInformation()
        {
            string[] userDataArr = new string[3];

            Console.Clear();
            Console.Out.WriteLine("Registrera medlem");
            Console.Out.WriteLine("===========================================");

            Console.Out.Write("Ange Förnamn: ");
            userDataArr[0] = getUserInput("^[a-ö,A-Ö, ,-]+$");

            Console.Out.Write("Ange Efternamn: ");
            userDataArr[1] = getUserInput("^[a-ö,A-Ö, ,-]+$"); 

            Console.Out.Write("Ange Personnummer(ÅÅMMDD-XXXX): ");
            userDataArr[2] = getUserInput("^\\d{6}-\\d{4}$");

            return userDataArr;
        }

        // Uppdatera medlemsinformation
        public string[] updateAndReturnMemberData(string[] userData)
        {
            string[] updatedUserData = new string[3];

            Console.Clear();
            Console.Out.WriteLine("Ändra medlemsuppgifter");
            Console.Out.WriteLine("====================================");
            
            Console.Out.WriteLine("Lämnna tomt för att ignorera ändring");
            
            Console.Out.WriteLine("\nFörnamn: " + userData[0]);
            Console.Out.Write("Nytt Förnamn: ");
            updatedUserData[0] = getUserInput("^[a-ö,A-Ö, ,-]*$");
            
            Console.Out.WriteLine("\nEfternamn: " + userData[1]);
            Console.Out.Write("Nytt Efteramn: ");
            updatedUserData[1] = getUserInput("^[a-ö,A-Ö, ,-]*$"); 
            
            Console.Out.WriteLine("\nPersonnummer: " + userData[2]);
            Console.Out.Write("Nytt Personnummer: ");
            updatedUserData[2] = getUserInput("^(\\d{6}-\\d{4})*$");
            
            updatedUserData = validateIfInputIsEmptySTRING(updatedUserData, userData);
       
            return updatedUserData;
        }

        // Lista tillgängliga båttyper
        public void presentBoatChangeView()
        {
            Console.Clear();
            Console.Out.WriteLine("Ändra båtens uppgifter, lämnna tomt för att ignorera ändring");
            showListOfBoatTypes();
        }

        // Hämta ny båttyp från användaren
        public int updateBoatType(int currentBoatType)
        {            
            Console.Out.WriteLine("Båttyp (ange siffra) \nDin nuvarande typ är:" + (BoatType)currentBoatType + "\n");

            int updatedBoatData = validateIfInputIsEmptyINT(getUpdatedValue("Ange ny båttyp: "), currentBoatType);

            return updatedBoatData;
        }
        // Hämta ny båtlängd från användaren
        public int updateBoatLength(int currentBoatLength)
        {
            Console.Out.WriteLine("\nBåtlängd \nDin nuvarande längd är:" + currentBoatLength + "m\n");            

            int updatedBoatData = validateIfInputIsEmptyINT(getUpdatedValue("Ange ny båtlängd: "), currentBoatLength);
            
            return updatedBoatData;
        }
       
        // Returna tidigare data om fältet lämnats tomt
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

        // Returna tidigare data om fältet lämnats tomt
        public int validateIfInputIsEmptyINT(int updatedData, int dataToCheckAgainst)
        {
            if (updatedData == -1)
            {
                updatedData = dataToCheckAgainst;
            }
           
            return updatedData;
        }

        // Visa lista båttyper
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

        // Visa bekräftelse
        public void confirm(string messageToPrint)
        {
            Console.Out.Write("\n" + messageToPrint);
            Console.ReadLine();
        }
    }
}