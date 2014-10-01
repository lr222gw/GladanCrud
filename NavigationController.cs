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

        //constructor... 
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

            MemberModel userToAdd = new MemberModel(userInfo[0], userInfo[1], userInfo[2]);

            this.view.confirm("Medlemmen är tillagd, tryck för att fortsätta...");

        }
        private void removeUser()
        {
            //Lista användare
            this.view.showAllUsers();          

            //Välj användarens id/nummer
            int input = this.view.deleteUserByID();

            this.list.deleteUserById(input);
            //Spara listan efter att användaren är borta.

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
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                default:

                    break;

            }
        }
    }
}
