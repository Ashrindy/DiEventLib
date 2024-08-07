using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvNodeModelCustom : DvNode
{
    public bool UseMasterLevel = false;
    public string Name1 = "";
    public string Name2 = "";
    public string Name3 = "";
    List<byte> UnkData = new();

    public DvNodeModelCustom() 
    {
        UnkData = new List<byte>();
        for (int i = 0; i < 76; i++)
        {
            UnkData.Add(0);
        }
    }
    public DvNodeModelCustom(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        UseMasterLevel = reader.Read<bool>();
        reader.Align(4);
        Name1 = reader.ReadDvString(Utils.StringEncoding.ShiftJIS);
        Name2 = reader.ReadDvString(Utils.StringEncoding.ShiftJIS);
        Name3 = reader.ReadDvString(Utils.StringEncoding.ShiftJIS);
        UnkData.AddRange(reader.ReadArray<byte>(0x4C));
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Utils.FromBool(UseMasterLevel));
        writer.WriteDvString(Name1, Utils.StringEncoding.ShiftJIS);
        writer.WriteDvString(Name2, Utils.StringEncoding.ShiftJIS);
        writer.WriteDvString(Name3, Utils.StringEncoding.ShiftJIS);
        writer.WriteCollection(UnkData);
    }
}