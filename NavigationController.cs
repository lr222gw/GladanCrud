using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GladanCRUD
{
    class NavigationController
    {
        private View view;
        private MemberListModel list;

        // Constructor 
        public NavigationController()
        {
            this.list = new MemberListModel();
            this.view = new View(this.list);
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
        }

        public void registerBoat()
        {
            this.view.showAllUsers();
            
            int input = this.view.getUserID("Ange medlemsID för den användare till vilken en ny båt skall registreras");

            MemberModel member = this.list.getUserFromList(input);
            BoatType boatType = this.view.getNewBoatType();
            int boatLength = this.view.getNewBoatLength();

            BoatModel newBoat = new BoatModel(boatType, boatLength, member);
            member.addBoat(newBoat);
            this.list.saveMemberList();

            this.view.confirm("Båten har registrerats, tryck för att fortsätta...");
        }

        public void removeBoat() {

            List<MemberModel> boatOwnerList = new List<MemberModel>();

            //hämta alla medlemmar, kolla om de har båtar, 
            //lista båtarna och ange index till båtarna
            for (int i = 0; i < list.memberList.Count(); i++ )
            {
                if (list.memberList[i].getBoatListOfUser().Count > 0)
                {
                    boatOwnerList.Add(list.memberList[i]);
                }
            }
            // Lista alla båtägare
            this.view.showMembersOfList(boatOwnerList);  
            //hämta båtägare i listan genom id
            int memberId = this.view.getUserInput();
            MemberModel member = list.getUserFromList(memberId);
            this.view.listMembersBoats(member.getBoatListOfUser());
            //Hämta användarvalet
            int boatId = this.view.getUserInput();
            //ta bort båt
            member.getBoatListOfUser().RemoveAt(boatId - 1);
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
