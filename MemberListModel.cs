using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace GladanCRUD.model
{
    class MemberListModel
    {
        private List<MemberModel> memberList;                                                        
        const string FileName = @"Members.bin";
        private int newestId;
        const string newestIdFile = @"NewestId.bin";

        public MemberListModel()
        {
            this.memberList = new List<MemberModel>();
            this.loadMemberList();       
            this.loadNewestId();
        }

        public List<MemberModel> getMemberList()
        {
            return this.memberList;
        }

        public int getNewestAndUpdateId()
        {
            this.newestId += 1;
            return this.newestId;
        }

        public void addMember(MemberModel member)
        {
            this.memberList.Add(member);
            this.saveMemberList();
        }

        public MemberModel getUserFromList(int memberId)
        {
            foreach (MemberModel member in memberList)
                if (member.getThisMemberId() == memberId)
                    return member;

            return null;
        }

        public void deleteUserById(int memberId)
        {
            foreach (MemberModel member in memberList)
                if (member.getThisMemberId() == memberId)
                {
                    memberList.RemoveAt(memberList.IndexOf(member));
                    break;
                }

            saveMemberList();
        }

        private void loadMemberList()
        {
            if (File.Exists(FileName))
            {
                Stream TestFileStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                this.memberList = (List<MemberModel>)deserializer.Deserialize(TestFileStream);
                TestFileStream.Close();
            }
            else
            {
                throw new Exception("Nu blev det något fel, hörru. Filen som allt ska sparas i finns inte");
            }
        }

        public void saveMemberList()
        {
            Stream TestFileStream = File.Create(FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(TestFileStream, this.memberList);
            TestFileStream.Close();
        }
        public void saveNewestId()
        {
            Stream TestFileStream = File.Create(newestIdFile);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(TestFileStream, this.newestId);
            TestFileStream.Close();
        }

        private void loadNewestId()
        {
            if (File.Exists(newestIdFile))
            {
                Stream TestFileStream = File.OpenRead(newestIdFile);
                BinaryFormatter deserializer = new BinaryFormatter();
                this.newestId = (int)deserializer.Deserialize(TestFileStream);
                TestFileStream.Close();
            }
            else
            {
                throw new Exception("Nu blev det något fel, hörru. Filen som allt ska sparas i finns inte");                
            }
        }
    }        
}