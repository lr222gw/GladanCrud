﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GladanCRUD.view;
using GladanCRUD.model;

namespace GladanCRUD.controller
{
    class MenuController
    {
        private View view;
        private MemberListModel list;

        public MenuController()
        {
            this.list = new MemberListModel();
            this.view = new View();
        }

        // Visa huvudmenyn och hämta användarens menyval
        public void doMenu(){

            int input;
            
            do
            {
                this.view.showMenu();
                
                do
                {
                    input = this.view.getUserChoice("Ange menyalternativ: ");

                    if (input >= 0 && input <= 9) // 0 - 9 = Menyalternativ
                        break;
                    
                    this.view.showIllegalInputMessage();
                } while (true);

                menuChoiceSwitch(input);

            } while (true);                        
        }

        private void addUser()
        {
            // Hämta medlemsdata genom inmatning från användaren
            string[] userInfo = this.view.getNewUserInformation();

            // Skapa ny medlem och lägg till i MemberList
            this.list.addMember(new MemberModel(userInfo[0], userInfo[1], userInfo[2], this.list.getNewestAndUpdateId()));
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
            this.view.showMembersList(this.list.getMemberList(), "Ta bort medlem");
          
            // Hämta användarens val och ta bort användaren
            this.list.deleteUserById(getUserChoiceOfMember(this.list.getMemberList()));
            
            this.view.confirm("Medlemmen borttagen, tryck 'ENTER' för att fortsätta...");
        }

        private void changeUserDetails()
        {
            this.view.showMembersList(this.list.getMemberList(), "Ändra medlemsuppgifter");

            // Visa medlemslista och hämta användarens val av id/nummer
            int input = getUserChoiceOfMember(this.list.getMemberList());
            
            // Hämta medlem baserat på Id
            MemberModel member = this.list.getUserFromList(input);

            // Hämta medlemsdata från MemberModel
            string[] memberData = member.getUserInfo();

            // Redigera och visa förnam, efternam, personnummer
            memberData = this.view.updateAndReturnMemberData(memberData);

            // Uppdatera medlem med ny uppgifter
            member.updateMember(memberData);

            // Spara MemberList
            this.list.saveMemberList();

            this.view.confirm("Medlemsuppgifterna har uppdaterats, tryck 'ENTER' för att fortsätta...");
        }

        private void registerBoat()
        {
            this.view.showMembersList(this.list.getMemberList(), "Registrera båt");

            // Hämta giltigt id genom inmatning från användaren
            int input = getUserChoiceOfMember(this.list.getMemberList());

            // Hämta medlem från MemberList
            MemberModel member = this.list.getUserFromList(input);
                        
            // Visa lista på båttyper
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

            this.view.confirm("Båten har registrerats, tryck 'ENTER' för att fortsätta...");
        }

        // Skapa lista innehållandes endast båtägare
        private List<MemberModel> getBoatOwnerList()
        {
            List<MemberModel> boatOwnerList = new List<MemberModel>();

            // Undersök vilka medlemmar som har båtar
            for (int i = 0; i < this.list.getMemberList().Count(); i++)
            {
                if (this.list.getMemberList()[i].getBoatListOfUser().Count > 0)
                    boatOwnerList.Add(this.list.getMemberList()[i]);
            }

            return boatOwnerList;
        }

        private MemberModel getBoatOwnerIdFromUser()
        {
            // Skapa lista med båtägare
            List<MemberModel> boatOwnerList = getBoatOwnerList();
            
            // Visa listan med båtägare
            this.view.showMembersList(boatOwnerList, "Båtägare");
            
            // Hämta användarens val av båtägare
            int input = getUserChoiceOfMember(boatOwnerList);

            // Hämta medlem från MemberList
            MemberModel member = this.list.getUserFromList(input);            

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
        
        private void removeBoat()
        {
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

            this.view.confirm("Båten har tagits bort, tryck 'ENTER' för att fortsätta...");
        }

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

            // Hämta befintliga uppgifter, redigera och visa båttyp och längd
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

            int newBoatLength;

            do
            {
                newBoatLength = this.view.updateBoatLength(boatInfoArr[1]);

                if (newBoatLength > 0)
                    break;

                this.view.showIllegalInputMessage();
            } while (true);
            
            // Updaterar array med båtdata
            boatInfoArr[0] = newBoatType;
            boatInfoArr[1] = newBoatLength;

            // Uppdatera båtinformation
            boat.updateBoat(boatInfoArr);
            
            // Spara MemberList
            this.list.saveMemberList();

            this.view.confirm("Båtinformationen har uppdaterats, tryck 'ENTER' för att fortsätta...");
        }

        // Hantera användarens val av aktivitet
        private void menuChoiceSwitch(int input)
        {
            switch(input){
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    addUser();
                    break;
                case 2:
                    this.view.showCompactList(this.list.getMemberList());
                    break;
                case 3:
                    this.view.showDetailedList(this.list.getMemberList(), "Medlemslista - Fullständig");
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
            this.view.showMembersList(this.list.getMemberList());

            // Välj medlem att visa
            int input = getUserChoiceOfMember(this.list.getMemberList());
            
            // Skapa ny tillfällig lista innehållandes endast en medlem 
            List<MemberModel> singelMemberList = new List<MemberModel>();
            singelMemberList.Add(this.list.getUserFromList(input));

            // Visa medlemsinformation
            this.view.showDetailedList(singelMemberList, "Medlemsinformation");
        }
    }
}