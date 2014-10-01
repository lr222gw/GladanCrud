using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GladanCRUD
{
    class NavigationController
    {
        //Controller
        private View view;
        private MemberListModel list;

        // New code ================================================
        private BoatListModel boatList;


        //constructor... 
        public NavigationController()
        {
            this.list = new MemberListModel();
            this.boatList = new BoatListModel();

            this.view = new View(this.list, this.boatList);
            
        }

        public void doNavigate(){

            //Anropa menu så användaren kan göra val 
            int input;
            do
            {
                this.view.showNavigation();

                input = this.view.getUserInput();
                if (input < 11 && input > -1)
                {
                    //Använd valet, och utför valet. (switch)
                    menuChoiceSwitch(input);
                    
                }


            } while (input != 0);                        
            
        }

        public void addUser()
        {
            var userInfo = this.view.getNewUserInformation();

            this.list.addMember(new MemberModel(userInfo[0], userInfo[1], userInfo[2]));

            this.view.confirm("Medlemmen är tillagd, tryck för att fortsätta...");

        }
        private void removeUser()
        {
            //Lista användare
            this.view.showAllUsers();          

            //Välj användarens id/nummer via menyn
            int input = this.view.getUserID("Ange användarens ID, för att ta bort användaren");

            //Ta bort användaren och spara listan
            this.list.deleteUserById(input);
            
            this.view.confirm("Medlemmen borttagen, tryck för att fortsätta...");
        }

        private void changeUserDetails()
        {
            //Lista användare
            this.view.showAllUsers();

            //Välj användarens id/nummer via menyn
            int input = this.view.getUserID("Ange användarens ID, för att ändra användaren");
            
            // Hämta user från lista
            string[] userData = this.list.getUserInfoByID(input);

            // redigera och visa förnam, efternam, personnummer
            string[] updatedUserData = this.view.updateAndReturnMemberData(userData);

            //ta bort användarens gamla data
            this.list.deleteUserById(input);

            //bygg den nya användaren
            MemberModel editedUserToAdd = new MemberModel(updatedUserData[0],updatedUserData[1],updatedUserData[2]);
            //lägg till nya datan            
            this.list.addMember(editedUserToAdd);
            //DONE! :D

            //getUpdatedMemberInfo(memberName, socialSecurityNumber);

        }

        public void registerBoat()
        {
            // Visa lista på medlem
            this.view.showAllUsers();
            
            // Välj medlemsID till vilken en båt skall skapas
            int input = this.view.getUserID("Ange medlemsID för den användare till vilken en ny båt skall registreras");

            // Hämta medlem med medlemsID
            MemberModel member = this.list.getUserFromList(input);
            
            // Fråga om och hämta båttyp
            BoatType boatType = this.view.getNewBoatType();

            // Fråga om och hämta längd

            int boatLength = this.view.getNewBoatLength();

            // Skapa båt
            BoatModel newBoat = new BoatModel(boatType, boatLength, member);
            
            // Lägg till båt i listan            
            this.boatList.addBoat(newBoat);
            
            // Visa bekräftelse 
            this.view.confirm("Båten har registrerats, tryck för att fortsätta...");
        }

        // TEST för att lista båtar
        public void removeBoat() {

            // Lista alla båtar
            this.view.showAllBoats();  

            // TEST för att lista båtar 
            Console.ReadLine();
        }





        public void menuChoiceSwitch(int input)
        {
            switch(input){
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    addUser();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    
                    break;
                case 5:
                    removeUser();
                    break;
                case 6:
                    changeUserDetails();
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    registerBoat();
                    break;
                case 10:
                    removeBoat();
                    break;
                case 11:
                    break;
                default:

                    break;

            }
        }
    }
}
