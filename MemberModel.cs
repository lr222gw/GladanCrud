using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladanCRUD
{
    [Serializable]
    class MemberModel 
    {

        private string firstName;
        private string lastName;
        private string socialSecurityNumber;
        private int memberID;        

        public MemberModel(string firstName, string lastName, string socialSecurityNumber)
        {
            this.memberID = getUniqueID();
            this.firstName = firstName;
            this.lastName = lastName;
            this.socialSecurityNumber = socialSecurityNumber;


        }

        private int getUniqueID()
        {
            MemberListModel memberList = new MemberListModel();
            List<int> idList = new List<int>();

            for (int i = 0; i < memberList.memberList.Count(); i++ )
            {
                idList.Add(memberList.memberList[i].getThisMemberId());
            }

            return idList.Max()+1;

        }

        public int getThisMemberId()
        {
            return this.memberID;
        }

        public string toString()
        {
            return "ID: " + this.memberID + " Förnamn: " + this.firstName + " Efternamn: " + this.lastName + " Personnummer: " + this.socialSecurityNumber;
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
    }
}
