using Amicitia.IO.Binary;

namespace DvSceneLib;

public class DvNodeBaseAnimationModel : DvNode
{
    public bool UseInternalName = false;
    public string ModelName = "";
    public string SkeletonName = "";
    public string InternalName = "";
    byte[] UnkData = new byte[76];

    public DvNodeBaseAnimationModel() { }
    public DvNodeBaseAnimationModel(BinaryObjectReader reader)
        => Read(reader);

    public void Read(BinaryObjectReader reader)
    {
        UseInternalName = reader.Read<bool>();
        reader.Align(4);
        ModelName = reader.ReadDvString(Utils.StringEncoding.ShiftJIS);
        SkeletonName = reader.ReadDvString(Utils.StringEncoding.ShiftJIS);
        InternalName = reader.ReadDvString(Utils.StringEncoding.ShiftJIS);
        UnkData = reader.ReadArray<byte>(76);
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(UseInternalName);
        writer.Align(4);
        writer.WriteDvString(ModelName, Utils.StringEncoding.ShiftJIS);
        writer.WriteDvString(SkeletonName, Utils.StringEncoding.ShiftJIS);
        writer.WriteDvString(InternalName, Utils.StringEncoding.ShiftJIS);
        writer.WriteCollection(UnkData);
    }

}