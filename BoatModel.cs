using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladanCRUD
{
    [Serializable]
    class BoatModel
    {
        private BoatType boatType;
        private int length;

        public BoatModel(BoatType boatType, int length)
        {
            this.boatType = boatType;
            this.length = length;
        }
     
        public BoatType getBoatType()
        {
            return this.boatType;
        }

        public int getBoatLength()
        {
            return this.length;
        }

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

    public enum BoatType
    {
        Segelbåt = 1,
        Motorseglare,
        Motorbåt,
        Kajak_Kanot,
        Övrigt
    }
}
