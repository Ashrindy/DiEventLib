using Amicitia.IO.Binary;
using DvSceneLib.Misc;
using System.Numerics;

namespace DvSceneLib;

[DvNodeCategory("Element")]
[DvNodeDescription("QTE Accel", "Adds a simple QTE element that accelarates the speed of the cutscene, but isn't required to be won")]
public class DvElementQTEAccel : DvNodeElement
{
    public QTEAccelButton QTEButton = QTEAccelButton.A;
    public float Unk0 = 0;
    public int Unk1 = 0;
    public int Unk2 = 0;
    public string SoundCueName = "";

    public DvElementQTEAccel() : base(DvElementID.QTEAccel) { }
    public DvElementQTEAccel(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        QTEButton = reader.Read<QTEAccelButton>();
        Unk0 = reader.Read<float>();
        Unk1 = reader.Read<int>();
        Unk2 = reader.Read<int>();
        SoundCueName = reader.ReadDvString(Utils.StringEncoding.Default);
    }

    protected override void WriteElement(BinaryObjectWriter writer)
    {
        writer.Write(QTEButton);
        writer.Write(Unk0);
        writer.Write(Unk1);
        writer.Write(Unk2);
        writer.WriteDvString(SoundCueName, Utils.StringEncoding.Default);
    }
}

public enum QTEAccelButton : uint
{
    A = 0,
    B,
    X,
    Y
}
