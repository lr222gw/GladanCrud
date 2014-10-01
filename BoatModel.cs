using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladanCRUD
{
    enum BoatType
    {
        Segelbåt,
        Motorseglare,
        Motorbåt,
        Kajak_Kanot,
        Övrigt
    }
    class BoatModel
    {
        private BoatType boatType;
        private int length;
        private MemberModel member;

        public BoatModel(BoatType boatType, int length, MemberModel member)
        {
            this.boatType = boatType;
            this.length = length;
            this.member = member;
        }
        
    }
}
