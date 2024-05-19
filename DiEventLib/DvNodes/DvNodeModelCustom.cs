using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvNodeModelCustom : DvNodeObject
{
    public bool UseMasterLevel { get; set; } = false;
    public string Name1 { get; set; } = "";
    public string Name2 { get; set; } = "";
    public string Name3 { get; set; } = "";
    List<byte> UnkData { get; set; } = new();

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
    public override void Read(BinaryObjectReader reader)
    {
        var TempUseMasterLevel = reader.Read<uint>();
        UseMasterLevel = Utils.ToBool(TempUseMasterLevel);
        Name1 = reader.ReadString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, 64);
        Name2 = reader.ReadString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, 64);
        Name3 = reader.ReadString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, 64);
        UnkData.AddRange(reader.ReadArray<byte>(0x4C));
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Utils.FromBool(UseMasterLevel));
        writer.WriteString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, Name1, 64);
        writer.WriteString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, Name2, 64);
        writer.WriteString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, Name3, 64);
        writer.WriteCollection(UnkData);
    }
}