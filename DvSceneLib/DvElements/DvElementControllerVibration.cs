using Amicitia.IO.Binary;
using DvSceneLib.Misc;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("Controller Vibration", "Plays haptic feedback")]
public class DvElementControllerVibration : DvNodeElement
{
    public bool IgnoreEnd = false;
    public string GroupName = "";
    public string VibrationName = "";

    public DvElementControllerVibration() : base(DvElementID.ControllerVibration) { }
    public DvElementControllerVibration(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        var Flags = reader.Read<uint>();
        IgnoreEnd = (Flags & 2) != 0;
        GroupName = reader.ReadDvString(Utils.StringEncoding.Default);
        VibrationName = reader.ReadDvString(Utils.StringEncoding.Default);
        reader.Skip(12);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        var Flags = 0;
        if (IgnoreEnd) Flags |= 2;
        writer.Write(Flags);
        writer.WriteDvString(GroupName, Utils.StringEncoding.Default);
        writer.WriteDvString(VibrationName, Utils.StringEncoding.Default);
        writer.WriteNulls(12);
    }
}
