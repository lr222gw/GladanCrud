﻿using System;
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
            this.view = new View();//this.list); 
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

                    if (input >= 0 && input <= 9)// 0->9 = meny alternativ
                        break;
                    
                    this.view.showIllegalInputMessage();
                } while (true);

                menuChoiceSwitch(input);

            } while (true);                        
        }

        // Skapa ny medlem och lägg till i MemberList
        public void addUser()
        {
            // Hämta medlemsdata genom inmatning från användaren
            string[] userInfo = this.view.getNewUserInformation();

            // Skapa ny medlem och lägg till i MemberList
            this.list.addMember(new MemberModel(userInfo[0], userInfo[1], userInfo[2], list.newestId +=1));  //list.getUniqueId()));
            this.list.saveNewestId();

            // Visa bekräftelse
            this.view.confirm("Medlemmen är tillagd, tryck 'ENTER' för att fortsätta...");
        }

        // Hämta giltig inmatning från användaren
        private int getUserChoiceOfMember(List<MemberModel> membersList)
        {
            int input;

            do
            {
                input = this.view.getUserChoice("Ange ID: ");
                
                foreach(MemberModel member in membersList)
                    if (member.getThisMemberId() == input)
                        return input;

                if(input == -1){

                }
                this.view.showIllegalInputMessage();
            } while (true);
        }

        // Ta bort en medlem
        private void removeUser()
        {
            // Lista användare
            this.view.showMembersList(this.list.memberList, "Ta bort medlem");
          
            // Hämta användarens val och ta bort användaren
            this.list.deleteUserById(getUserChoiceOfMember(this.list.memberList));
            
            // Visa bekräftelse
            this.view.confirm("Medlemmen borttagen, tryck 'ENTER' för att fortsätta...");
        }

        // Uppdatera en medlems uppgifter
        private void changeUserDetails()
        {
            // Lista användare
            this.view.showMembersList(this.list.memberList, "Ändra medlemsuppgifter");

            // Välj användarens id/nummer via menyn
            int input = getUserChoiceOfMember(this.list.memberList);
            
            // Hämta member baserat på Id
            MemberModel member = list.getUserFromList(input);

            // Hämta medlemsdata från Member
            string[] memberData = member.getUserInfo();

            // Redigera och visa förnam, efternam, personnummer
            memberData = this.view.updateAndReturnMemberData(memberData);

            // Uppdatera medlem med ny uppgifter
            member.updateMember(memberData);

            // Spara MemberList
            list.saveMemberList();

            // Visa bekräftelse
            this.view.confirm("Medlemsuppgifterna har uppdaterats, tryck 'ENTER' för att fortsätta...");
        }

        // Registrera ny båt
        public void registerBoat()
        {
            // Visa lista på medlemmar
            this.view.showMembersList(this.list.memberList, "Registrera båt");

            // Hämta giltigt id genom inamtning av användaren
            int input = getUserChoiceOfMember(this.list.memberList);

            // Hämta medlem från MemberList
            MemberModel member = this.list.getUserFromList(input);
                        
            // Visa lisat på båttyper
            this.view.showBoatTypes();

            // Hämta giltig inmatning från användaren
            int boatType;
            int enumSize = Enum.GetNames(typeof(BoatType)).Length;           
            do{
                boatType = this.view.getUserChoice("Ange båttyp: ");

                if(boatType > 0 && boatType <= enumSize)
                    break;

                this.view.showIllegalInputMessage();
            }while(true);
            
            int boatLength;
            
            do{
                boatLength = this.view.getUserChoice("Ange längd: ");

                if(boatLength > 0)
                    break;

                this.view.showIllegalInputMessage();
            }while(true);
                        
            // Skapa ny båt och lägg till i BoatList
            member.addBoat(new BoatModel((BoatType)Enum.Parse(typeof(BoatType), Enum.GetName(typeof(BoatType), boatType)), boatLength));
            
            // Spara MemberList
            this.list.saveMemberList();

            // Visa bekräftelse
            this.view.confirm("Båten har registrerats, tryck 'ENTER' för att fortsätta...");
        }

        // Skapa lista innehållandes endast båtägare
        public List<MemberModel> getBoatOwnerList()
        {
            List<MemberModel> boatOwnerList = new List<MemberModel>();

            // Undersök vilka medlemmar som har båtar
            for (int i = 0; i < list.memberList.Count(); i++)
            {
                if (list.memberList[i].getBoatListOfUser().Count > 0)
                    boatOwnerList.Add(list.memberList[i]);
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

            // Hämta medlem från MemberList
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
                boatId = this.view.getUserChoice("Ange RadID: ");

                if (boatId >= 1 && boatId <= boatListSize)
                    return boatId;

                this.view.showIllegalInputMessage();
            } while (true);
        }
        
        // Ta bort båt
        public void removeBoat() {
            //TODO: bryt ut till båtens radid funktion..
            
            // Hämta vilken båtägare det gäller
            MemberModel member = getBoatOwnerIdFromUser();

            // Lista medlemmens båtar
            this.view.showMemberBoatsList(member.getBoatListOfUser());

            // Hämta användarens val av båt
            int rowId = getSpecificBoatId(member.getBoatListOfUser());

            // Ta bort båt
            member.getBoatListOfUser().RemoveAt(rowId - 1);
            
            // Spara MemberList
            this.list.saveMemberList();

            // Visa bekräftelse
            this.view.confirm("Båten har tagits bort, tryck 'ENTER' för att fortsätta...");
        }

        // Uppdatera båtinformation
        private void changeBoatDetails()
        {
            // Hämta vilken båtägare det gäller
            MemberModel member = getBoatOwnerIdFromUser();

            // Lista medlemmens båtar
            this.view.showMemberBoatsList(member.getBoatListOfUser());

            // Hämta användarens val av båt
            int rowId = getSpecificBoatId(member.getBoatListOfUser());
            
            // Hämta båt baserat på radId
            BoatModel boat = member.getBoatByIndex(rowId - 1);

            // Hämta befintliga uppgifter, Redigera och visa båttyp och längd
            int[] boatInfoArr = boat.getBoatInfo();

            int enumSize = Enum.GetNames(typeof(BoatType)).Length;

            int newBoatType;

            view.presentBoatChangeView();

            do
            {
                newBoatType = view.updateBoatType(boatInfoArr[0]);

                if (newBoatType > 0 && newBoatType <= enumSize)
                    break;

                this.view.showIllegalInputMessage();
            } while (true);

            //boatInfoArr = this.view.updateAndReturnBoatData(boatInfoArr);
            int newBoatLength;

            do
            {
                newBoatLength = this.view.updateBoatLength(boatInfoArr[1]);

                if (newBoatLength > 0)
                    break;

                this.view.showIllegalInputMessage();
            } while (true);
            
            //updaterar array
            boatInfoArr[0] = newBoatType;
            boatInfoArr[1] = newBoatLength;

            // Uppdatera båtinformation
            boat.updateBoat(boatInfoArr);
            
            // Spara MemberList
            this.list.saveMemberList();

            // Visa bekräftelse
            this.view.confirm("Båtinformationen har uppdaterats, tryck 'ENTER' för att fortsätta...");
        }

        // Hantera användarens val av aktivitet
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

        // Visa detaljerad information om en medlem
        private void showSingleMember()
        {
            // Visa enkel medlemslista
            this.view.showMembersList(this.list.memberList);

            // Välj medlem att visa
            int input = getUserChoiceOfMember(this.list.memberList);
            
            // Skapa ny tillfällig lista innehållandes endast en medlem 
            List<MemberModel> singelMemberList = new List<MemberModel>();
            singelMemberList.Add(this.list.getUserFromList(input));

            // Visa medlemsinformation
            this.view.showDetailedList(singelMemberList, "Medlemsinformation");
        }
    }
}




////ta bort användarens gamla data
//this.list.deleteUserById(input); //TODO: Obs MedlemsId/nummer blir nytt, ska det vara så? 
//                                // ersätta istä'llet gamla värden med de nya? istället för att ta bort.

//bygg den nya användaren
//TODO: ÄNDRA!!!! //MemberModel editedUserToAdd = new MemberModel(updatedUserData[0],updatedUserData[1],updatedUserData[2]);
//lägg till nya datan            
//TODO: ÄNDRA!!! //this.list.addMember(editedUserToAdd);

//DONE! :D