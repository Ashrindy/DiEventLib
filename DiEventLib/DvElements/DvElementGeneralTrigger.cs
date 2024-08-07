using Amicitia.IO.Binary;
using DiEventLib.Misc;
using System.Text;

namespace DiEventLib;

[DvNodeCategory("Element")]
[DvNodeDescription("General Trigger", "Triggers pre-defined events")]
public class DvElementGeneralTrigger : DvNodeElement
{
    public uint Field_00 = 0;
    public string TriggerName = "";
    public Trigger TriggerEnum = Trigger.None;

    public DvElementGeneralTrigger() : base(DvElementID.GeneralTrigger) { }
    public DvElementGeneralTrigger(BinaryObjectReader reader)
        => Read(reader);
    public void Read(BinaryObjectReader reader)
    {
        Field_00 = reader.Read<uint>();
        TriggerName = reader.ReadString(Encoding.UTF8, StringBinaryFormat.FixedLength, 64);
        switch(TriggerName)
        {
            case "pause_bgm":
                TriggerEnum = Trigger.PauseBGM;
                break;

            case "play_bgm":
                TriggerEnum = Trigger.PlayBGM;
                break;

            case "miss":
                TriggerEnum = Trigger.Miss;
                break;

            case "action":
                TriggerEnum = Trigger.Action;
                break;

            case "damage":
                TriggerEnum = Trigger.Damage;
                break;

            case "damage2":
                TriggerEnum = Trigger.Damage2;
                break;

            default:
                TriggerEnum = Trigger.None;
                break;
        }
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_00);
        switch (TriggerEnum)
        {
            case Trigger.None:
                writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, TriggerName, 64);
                break;
            
            case Trigger.PauseBGM:
                writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, "pause_bgm", 64);
                break;

            case Trigger.PlayBGM:
                writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, "play_bgm", 64);
                break;

            case Trigger.Miss:
                writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, "miss", 64);
                break;

            case Trigger.Action:
                writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, "action", 64);
                break;

            case Trigger.Damage:
                writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, "damage", 64);
                break;

            case Trigger.Damage2:
                writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, "damage2", 64);
                break;
        }
        
    }
}

public enum Trigger
{
    None = -1,

    //Used in all of the boss fight openings
    PauseBGM = 0,
    PlayBGM,

    //Used in zev_end
    Miss,
    Action,

    //Used in ev5040
    Damage,
    Damage2,
}
