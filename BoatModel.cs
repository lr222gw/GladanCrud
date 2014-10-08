using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladanCRUD
{
    public enum BoatType
    {
        Segelbåt = 1,
        Motorseglare,
        Motorbåt,
        Kajak_Kanot,
        Övrigt
    }

    [Serializable]
    class BoatModel
    {
        private BoatType boatType;
        private int length;
        //private MemberModel member;

        public BoatModel(BoatType boatType, int length)//, MemberModel member)
        {
            this.boatType = boatType;
            this.length = length;
            //this.member = member;
        }
     
        public int getBoatType()
        {
            return (int)this.boatType;
        }

        public string getBoatTypeString()
        {
            return this.boatType.ToString();
        }

        public int getBoatLength()
        {
            return this.length;
        }

        // Hitflyttad från MemberList
        public int[] getBoatInfo()
        {
            int[] boatData = new int[2];

            boatData[0] = (int)this.boatType;
            boatData[1] = this.length;

            return boatData;
        }

        public void updateBoat(int[] newBoatData)
        {
            this.boatType = (BoatType)newBoatData[0];
            this.length = newBoatData[1];
        }
    }
}
