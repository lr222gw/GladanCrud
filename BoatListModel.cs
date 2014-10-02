using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace GladanCRUD
{
    //class BoatListModel
    //{
    //    public List<BoatModel> boatList;
    //    const string FileName = @"Boats.bin";

    //    public BoatListModel()
    //    {
    //        this.boatList = new List<BoatModel>();
    //        this.getBoatList();
    //    }

        

    //    public void saveBoatList()
    //    {
    //        Stream FileStream = File.Create(FileName);
    //        BinaryFormatter serializer = new BinaryFormatter();
    //        serializer.Serialize(FileStream, this.boatList);
    //        FileStream.Close();
    //    }

    //    public void getBoatList()
    //    {
    //        try
    //        {
    //            if (File.Exists(FileName))
    //            {
    //                Stream FileStream = File.OpenRead(FileName);
    //                BinaryFormatter deserializer = new BinaryFormatter();
    //                this.boatList = (List<BoatModel>)deserializer.Deserialize(FileStream);
    //                FileStream.Close();
    //            }
    //            else
    //            {
    //                Console.Out.WriteLine("Nu blev det något fel, hörru. Filen som allt ska sparas i finns inte");
    //            }
    //        } catch (Exception ex) {
    //            Console.Out.WriteLine("Fel");
    //            Console.In.ReadLine();
    //        }
    //    }
    //}
}
