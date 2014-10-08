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
        private int memberID = 0;
        private List<BoatModel> boatList;//TODO: FIXA READONLY PÅ ALLA LISTOR!!!

        public MemberModel(string firstName, string lastName, string socialSecurityNumber, int newestId)
        {
            this.memberID = newestId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.socialSecurityNumber = socialSecurityNumber;
            this.boatList = new List<BoatModel>();
        }

        //private int getUniqueID(int newestId)
        //{
        //    //MemberListModel memberList = new MemberListModel();
        //    //List<int> idList = new List<int>();
        //    //ändra på detta efter filen, saken prylen. duvet
        //    //for (int i = 0; i < memberList.memberList.Count(); i++ )
        //    //{
        //    //    idList.Add(memberList.memberList[i].getThisMemberId());
        //    //}

        //    if (memberList.memberList.Count() == 0) {
        //        //om inga användare finns i listan, så ska första angivna ID vara 1...
        //        return 1;
        //    }
        //    //

        //    return idList.Max()+1;

        //}

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
        public List<BoatModel> getBoatListOfUser()
        {
            return this.boatList;
        }

        public void addBoat(BoatModel boat)
        {
            this.boatList.Add(boat);
        }
    }
}
