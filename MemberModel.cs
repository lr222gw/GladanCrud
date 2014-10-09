using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GladanCRUD.model
{
    [Serializable]
    class MemberModel 
    {
        private string firstName;
        private string lastName;
        private string socialSecurityNumber;
        private int memberID = 0;
        private List<BoatModel> boatList;

        public MemberModel(string firstName, string lastName, string socialSecurityNumber, int uniqueId)
        {
            setThisMemberId(uniqueId);
            setUserFirstName(firstName);
            setUserLastName(lastName);
            setSocialSecurityNumber(socialSecurityNumber);
            this.boatList = new List<BoatModel>();
        }
        private void setThisMemberId(int memberID)
        {
            if (memberID > 0)
            {
                this.memberID = memberID;
            }
            else
            {
                throw new Exception("Invalid MemberID...");
            }
        }

        private bool validateName(string name)
        {
            return (Regex.IsMatch(name, "^[a-ö,A-Ö, ,-]+$"));    
        }

        private void setUserFirstName(string firstName)
        {
            if(validateName(firstName)){
                this.firstName = firstName;
            }
            else
            {
                throw new Exception("Invalid firstName...");
            }           
        }

        private void setUserLastName(string lastName)
        {
            if (validateName(lastName))
            {
                this.lastName = lastName;
            }
            else
            {
                throw new Exception("Invalid lastName...");
            }  
        }

        private void setSocialSecurityNumber(string socialSecurityNumber)
        {
            if (Regex.IsMatch(socialSecurityNumber, "^\\d{6}-\\d{4}$"))
            {
                this.socialSecurityNumber = socialSecurityNumber;
            }
            else
            {
                throw new Exception("Invalid setSocialSecurityNumber...");
            }
        }

        public int getThisMemberId()
        {
            return this.memberID;
        }

        public string getUserFirstName()
        {
            return this.firstName;
        }

        public string getUserLastName()
        {
            return this.lastName;
        }

        public string getSocialSecurityNumber()
        {
            return this.socialSecurityNumber;
        }

        public string[] getUserInfo()
        {
            string[] userInfoArr = new string[3];

            userInfoArr[0] = this.firstName;
            userInfoArr[1] = this.lastName;
            userInfoArr[2] = this.socialSecurityNumber;

            return userInfoArr;
        }

        public void updateMember(string[] newData)
        {
            setUserFirstName(newData[0]);
            setUserLastName(newData[1]);
            setSocialSecurityNumber(newData[2]);            
        }

        public void addBoat(BoatModel boat)
        {
            this.boatList.Add(boat);
        }

        public List<BoatModel> getBoatListOfUser()
        {
            return this.boatList;
        }

        public BoatModel getBoatByIndex(int index)
        {
            return this.boatList[index];
        }
    }
}