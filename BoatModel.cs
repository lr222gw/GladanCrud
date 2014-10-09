using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladanCRUD.model
{
    [Serializable]
    class BoatModel
    {
        private BoatType boatType;
        private int length;

        public BoatModel(BoatType boatType, int length)
        {
            setBoatType(boatType);
            setBoatLength(length);
        }
        private void setBoatType(BoatType boatType)
        {
            if (Enum.IsDefined(typeof(BoatType), boatType))
            {
                this.boatType = boatType;
            }
            else
            {
                throw new Exception("Invalid BoatType");
            }
        }

        private void setBoatLength(int length)
        {
            if (length >= 1)
            {
                this.length = length;
            }
            else
            {
                throw new Exception("Invalid Length");
            }
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
            setBoatType((BoatType)newBoatData[0]);
            setBoatLength(newBoatData[1]);
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