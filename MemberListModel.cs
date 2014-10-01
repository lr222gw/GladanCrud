﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace GladanCRUD
{
    //[Serializable]
    
    class MemberListModel
    {
        public List<MemberModel> memberList;
        const string FileName = @"Members.bin";


        public MemberListModel()
        {
            this.memberList = new List<MemberModel>();
            this.GetMemberList();
        }

        public void addMember(MemberModel member)
        {
            this.memberList.Add(member);
            this.saveMemberList();
        }
        public void saveMemberList()
        {

            Stream TestFileStream = File.Create(FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(TestFileStream, this.memberList);
            TestFileStream.Close();            

        }

        public void GetMemberList()
        {
            if(File.Exists(FileName)){

                Stream TestFileStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                this.memberList = (List<MemberModel>)deserializer.Deserialize(TestFileStream);                
                TestFileStream.Close();

            }
            else
            {
                Console.Out.WriteLine("Nu blev det något fel, hörru. Filen som allt ska sparas i finns inte");
            }
            

        }


        private MemberModel getUserFromList(int id)
        {
            for (int i = 0; i < this.memberList.Count(); i++)
            {
                if (this.memberList[i].getThisMemberId() == id){


                    return this.memberList[i];
                    
                    
                    

                }                    
            }

            return null;
        }
        private int getUserPositionFromList(int id)
        {
            for (int i = 0; i < this.memberList.Count(); i++)
            {
                if (this.memberList[i].getThisMemberId() == id)
                {
                    return i;
                }
            }

            return -1;
        }
        
        public void deleteUserById(int input)
        {
            //Denna måste fixas, RemoveAt baserar sig på arrayensplats, (array börjar på 0...) 
            //Vi anger IDt istället, vi räknar id från 1... så användare med ID 4 ligger på plats 3 i arrayen. 
            //Om vi anger att vi vill ta bort användare med id't 4 så kommer vi försöka ta bort den 4de platsen i arrayen, vilket som inte finns...
            //this.memberList.RemoveAt(this.getUserFromList(input).getThisMemberId());
            int positionOfUser = this.getUserPositionFromList(input);
            if (positionOfUser != -1) //Säkerhetsspärr, kollar om användaren ej fanns...
            {
                this.memberList.RemoveAt(positionOfUser); //löste genom att göra ny metod...
                this.saveMemberList();

            }else{
                throw new Exception("User by that ID does not exist...");
            }
            
        }

        public void changeUserDetails(string[] newUserDetails)
        {

        }

        public string[] getUserInfoByID(int input)
        {
            MemberModel member = this.getUserFromList(input);

            string[] userInfoArr = new string[3];

            userInfoArr[0] = member.getUserFirstName();
            userInfoArr[1] = member.getUserLastName();
            userInfoArr[2] = member.getSocialSecurityNumber();

            return userInfoArr;
            
        }
    }
}
