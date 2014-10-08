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
            this.view = new View(); 
        }

        // Show main menu and get user choice
        public void doMenu(){

            int input;
            
            do
            {
                this.view.showMenu();
                
                do
                {
                    input = this.view.getUserChoice("Ange menyalternativ: ");

                    if (input >= 0 && input <= 9)
                        break;
                    
                    this.view.showIllegalInputMessage();
                } while (true);

                menuChoiceSwitch(input);

            } while (true);                        
        }

        public void addUser()
        {
            var userInfo = this.view.getNewUserInformation();

            this.list.addMember(new MemberModel(userInfo[0], userInfo[1], userInfo[2], list.newestId +=1));
            this.list.saveNewestId();
            this.view.confirm("Medlemmen är tillagd, tryck 'ENTER' för att fortsätta...");

        }

        private int getUserChoiceOfMember(List<MemberModel> membersList)
        {
            int input;

            do
            {
                input = this.view.getUserChoice("Ange ID: ");
                
                foreach(MemberModel member in membersList)
                    if (member.getThisMemberId() == input)
                        return input;

                this.view.showIllegalInputMessage();
            } while (true);
        }

        private void removeUser()
        {
            // Lista användare
            this.view.showMembersList(this.list.memberList, "Ta bort medlem");
          
            //Hämta användarens val och ta bort användaren
            this.list.deleteUserById(getUserChoiceOfMember(this.list.memberList));
            
            this.view.confirm("Medlemmen borttagen, tryck 'ENTER' för att fortsätta...");
        }

        private void changeUserDetails()
        {
            //Lista användare
            this.view.showMembersList(this.list.memberList, "Ändra medlemsuppgifter");

            //Välj användarens id/nummer via menyn
            int input = getUserChoiceOfMember(this.list.memberList);
            
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
            this.view.showMembersList(this.list.memberList, "Registrera båt");

            int input = getUserChoiceOfMember(this.list.memberList);

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
            
            // Visa lista med båtägare
            this.view.showMembersList(boatOwnerList, "Båtägare");
            
            // Hämta användarens val av båtägare
            int input = getUserChoiceOfMember(boatOwnerList);

            MemberModel member = list.getUserFromList(input);            

            return member;
        }

        // Hämta användarens val av båt
        private int getSpecificBoatId(List<BoatModel> boatList)
        {
            int boatId;
            int boatListSize = boatList.Count();

            do
            {
                boatId = this.view.getUserChoice("Ange BåtID: ");

                if (boatId >= 0 && boatId <= boatListSize - 1)
                    return boatId;

                this.view.showIllegalInputMessage();
            } while (true);
        }
        

        public void removeBoat() {

            // Hämta vilken båtägare det gäller
            MemberModel member = getBoatOwnerIdFromUser();

            // Lista medlemmens båtar
            this.view.showMemberBoatsList(member.getBoatListOfUser());

            // Hämta användarens val av båt
            int boatId = getSpecificBoatId(member.getBoatListOfUser());

            //ta bort båt
            member.getBoatListOfUser().RemoveAt(boatId);
            //spara
            list.saveMemberList();

            this.view.confirm("Båten har tagits bort, tryck 'ENTER' för att fortsätta...");
        }

        private void changeBoatDetails()
        {
            // Hämta vilken båtägare det gäller
            MemberModel member = getBoatOwnerIdFromUser();

            // Lista medlemmens båtar
            this.view.showMemberBoatsList(member.getBoatListOfUser());

            // Hämta användarens val av båt
            int boatId = getSpecificBoatId(member.getBoatListOfUser());
            
            int[] boatInfoArr = this.list.getBoatInfoByID(member, boatId);

            boatInfoArr = this.view.updateAndReturnBoatData(boatInfoArr);

            member.getBoatListOfUser()[boatId].setNewBoatDetails(boatInfoArr);

            list.saveMemberList();

            view.confirm("Båtinformationen har uppdaterats, tryck 'ENTER' för att fortsätta...");
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
            int input = getUserChoiceOfMember(this.list.memberList);
            
            // Skapa ny tillfällig lista innehållandes endast en medlem 
            List<MemberModel> singelMemberList = new List<MemberModel>();
            singelMemberList.Add(this.list.getUserFromList(input));

            // Visa medlemsinformation
            this.view.showDetailedList(singelMemberList, "Medlemsinformation");
        }
    }
}
