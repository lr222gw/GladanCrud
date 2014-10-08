using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GladanCRUD
{
    class MenuController
    {
        private View view;
        private MemberListModel list;

        // Constructor 
        public MenuController()
        {
            this.list = new MemberListModel();
            //this.view = new View(this.list);              CHANGED - Removed dependancy from Model in View
            this.view = new View(); 
        }

        public void doMenu(){

            //Anropa menu så användaren kan göra val 
            int input;
            do
            {
                this.view.showMenu();

                input = this.view.getUserInput();
                if (input <= 11 && input > -1)
                {
                    //Använd valet, och utför valet. (switch)
                    menuChoiceSwitch(input);
                    
                }

            } while (input != 0);                        
        }

        public void addUser()
        {
            var userInfo = this.view.getNewUserInformation();

            this.list.addMember(new MemberModel(userInfo[0], userInfo[1], userInfo[2], list.newestId +=1));
            this.list.saveNewestId();
            this.view.confirm("Medlemmen är tillagd, tryck 'ENTER' för att fortsätta...");

        }
        private void removeUser()
        {
            //Lista användare
            //this.view.showAllUsers();             REMOVED
            this.view.showMembersList(this.list.memberList);

            //Välj användarens id/nummer via menyn
            int input = this.view.getUserID("Ange medlemsID för den medlem som \nskall tas bort: ");

            //Ta bort användaren och spara listan
            this.list.deleteUserById(input);
            
            this.view.confirm("Medlemmen borttagen, tryck 'ENTER' för att fortsätta...");
        }

        private void changeUserDetails()
        {
            //Lista användare
            //this.view.showAllUsers();             REMOVED
            this.view.showMembersList(this.list.memberList);

            //Välj användarens id/nummer via menyn
            int input = this.view.getUserID("Ange medlemsID för den medlem som \nskall ändras: ");
            
            // Hämta user från lista
            string[] userData = this.list.getUserInfoByID(input);

            // redigera och visa förnam, efternam, personnummer
            string[] updatedUserData = this.view.updateAndReturnMemberData(userData);

            //ta bort användarens gamla data
            this.list.deleteUserById(input); //TODO: Obs MedlemsId/nummer blir nytt, ska det vara så? 
                                            // ersätta istä'llet gamla värden med de nya? istället för att ta bort.
            
            //bygg den nya användaren
            //TODO: ÄNDRA!!!! //MemberModel editedUserToAdd = new MemberModel(updatedUserData[0],updatedUserData[1],updatedUserData[2]);
            //lägg till nya datan            
            //TODO: ÄNDRA!!! //this.list.addMember(editedUserToAdd);
            this.view.confirm("Medlemsuppgifterna har ändrats, tryck 'ENTER' för att fortsätta...");
            //DONE! :D
        }

        public void registerBoat()
        {
            //this.view.showAllUsers();                     REMOVED
            this.view.showMembersList(this.list.memberList);
            
            int input = this.view.getUserID("Ange medlemsID för den medlem till vilken en \nny båt skall registreras: ");

            MemberModel member = this.list.getUserFromList(input);

            BoatType boatType = this.view.getNewBoatType();
            int boatLength = this.view.getNewBoatLength();

            BoatModel newBoat = new BoatModel(boatType, boatLength, member);
            member.addBoat(newBoat);
            this.list.saveMemberList();

            this.view.confirm("Båten har registrerats, tryck 'ENTER' för att fortsätta...");
        }

        public List<MemberModel> getBoatOwnerList()
        {
            List<MemberModel> boatOwnerList = new List<MemberModel>();

            //hämta alla medlemmar, kolla om de har båtar, 
            //lista båtarna och ange index till båtarna
            for (int i = 0; i < list.memberList.Count(); i++)
            {
                if (list.memberList[i].getBoatListOfUser().Count > 0)
                {
                    boatOwnerList.Add(list.memberList[i]);
                }
            }

            return boatOwnerList;
        }

        public MemberModel getBoatOwnerIdFromUser()
        {
            // Lista alla båtägare
            List<MemberModel> boatOwnerList = getBoatOwnerList();
            //hämta båtägare i listan genom id
            //this.view.showMembersOfList(boatOwnerList);               CHANGED
            this.view.showMembersList(boatOwnerList, "Båtägare");
            
            //int memberId = this.view.getUserInput();                  CHANGED
            int memberId = this.view.getUserID("Ange medlemsID: "); // BUHU! Komma på en snyggare lösning
            MemberModel member = list.getUserFromList(memberId);            
            //Hämta användarvalet

            return member;
        }

        public void removeBoat() {

            MemberModel member = getBoatOwnerIdFromUser();
            //this.view.listMembersBoats(member.getBoatListOfUser());   CHANGED
            this.view.showMemberBoatsList(member.getBoatListOfUser());

            //int boatId = this.view.getUserInput();                    CHANGED
            int boatId = this.view.getUserID("Ange RadId för den båt som \nskall tas bort: ");

            //ta bort båt
            member.getBoatListOfUser().RemoveAt(boatId);
            //spara
            list.saveMemberList();

            this.view.confirm("Båten har tagits bort, tryck 'ENTER' för att fortsätta...");
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
                    this.view.showCompactList(this.list.memberList);
                    break;
                case 3:
                    this.view.showDetailedList(this.list.memberList, "Medlemslista - Fullständig");
                    break;
                case 4:
                    removeUser();
                    break;
                case 5:
                    changeUserDetails();
                    break;
                case 6:
                    showSingleMember();
                    break;
                case 7:
                    registerBoat();
                    break;
                case 8:
                    removeBoat();
                    break;
                case 9:
                    changeBoatDetails();
                    break;
                default:
                    break;
            }
        }

        private void showSingleMember()
        {
            // Visa enkel medlemslista
            this.view.showMembersList(this.list.memberList);

            // Välj medlem
            int memberId = this.view.getUserID("Ange medlemsID för den medlem som \ndu önskar visa mera information: ");
            
            // Skapa nya lista innehållandes endast en medlem 
            List<MemberModel> singelMemberList = new List<MemberModel>();
            singelMemberList.Add(this.list.getUserFromList(memberId));

            // Visa medlemsinformation
            this.view.showDetailedList(singelMemberList, "Medlemsinformation");
        }

        private void changeBoatDetails()
        {
            // hämta båtR hoss användare som har bå¨tar ; anv'änd gamla kod
            MemberModel member = getBoatOwnerIdFromUser();
            this.view.listMembersBoats(member.getBoatListOfUser());
            int boatId = this.view.getUserInput();
            int[] boatInfoArr = this.list.getBoatInfoByID(member, boatId);
            boatInfoArr = this.view.updateAndReturnBoatData(boatInfoArr);

            member.getBoatListOfUser()[boatId].setNewBoatDetails(boatInfoArr);
            list.saveMemberList();
            view.confirm("Båtdetaljerna har ändrats");
        }
    }
}
