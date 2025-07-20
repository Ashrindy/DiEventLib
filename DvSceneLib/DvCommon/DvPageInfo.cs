using Amicitia.IO.Binary;
using System.Text;

namespace DvSceneLib;


public class DvPageInfo : DvObject, IBinarySerializable
{
    public List<DvPage> Entries { get; set; } = new();

    public void Read(BinaryObjectReader reader)
    {
        Count = reader.Read<int>();
        AllocatedSize = reader.Read<int>();
        reader.Skip(8);
        Entries.AddRange(reader.ReadObjectArray<DvPage>(Count));
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Entries.Count);
        long sizePos = writer.Position;
        writer.WriteNulls(12);
        AllocatedSize = (int)writer.Position;
        writer.WriteObjectCollection(Entries);
        AllocatedSize = (int)writer.Position - AllocatedSize;
        writer.WriteAt(sizePos, AllocatedSize);
    }
}


public class DvPage : IBinarySerializable
{
    public uint Version { get; set; }
    public uint Flags { get; set; }
    public uint Start { get; set; }
    public uint End { get; set; }
    public uint SkipFrame { get; set; }
    public uint Index { get; set; }
    public string Name { get; set; }
    public List<int> UnkFields { get; set; } = new();
    public List<Transition> Transitions { get; set; } = new();

    public void Read(BinaryObjectReader reader)
    {
        Version = reader.Read<uint>();
        Flags = reader.Read<uint>();
        Start = reader.Read<uint>() / 100;
        End = reader.Read<uint>() / 100;
        var TransitionCount = reader.Read<int>();
        var TransitionSize = reader.Read<uint>();
        SkipFrame = reader.Read<uint>() / 100;
        Index = reader.Read<uint>();
        var SkipLinkIndexNum = reader.Read<int>();
        reader.Skip(12);
        Name = reader.ReadDvString(Utils.StringEncoding.UTF8, 32);
        UnkFields.AddRange(reader.ReadArray<int>(SkipLinkIndexNum));
        Transitions.AddRange(reader.ReadObjectArray<Transition>(TransitionCount));
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(Version); 
        writer.Write(Flags);
        writer.Write(Start * 100);
        writer.Write(End * 100);
        writer.Write(Transitions.Count);
        var transitionSizePos = writer.Position;
        writer.WriteNulls(4);
        writer.Write(SkipFrame * 100);
        writer.Write(Index);
        writer.Write(UnkFields.Count);
        writer.WriteNulls(12);
        writer.WriteDvString(Name, Utils.StringEncoding.UTF8, 32);
        writer.WriteCollection(UnkFields);
        var transitionSize = writer.Position;
        writer.WriteObjectCollection(Transitions);
        transitionSize = writer.Position - transitionSize;
        writer.WriteAt(transitionSizePos, (int)transitionSize);
    }
}

public class Transition : IBinarySerializable
{
    public int DestPageIndex { get; set; }
    public List<Condition> Conditions { get; set; } = new();

    public void Read(BinaryObjectReader reader)
    {
        DestPageIndex = reader.Read<int>();
        var ConditionNum = reader.Read<int>();
        var ConditionSize = reader.Read<uint>();
        reader.Skip(4);
        for (int i = 0; i < ConditionNum; i++)
        {
            var type = reader.Read<Condition.Type>();
            reader.Skip(-4);
            switch (type)
            {
                case Condition.Type.PageEnd:
                    Conditions.Add(new ConditionPageEnd(reader));
                    break;

                case Condition.Type.QTE:
                    Conditions.Add(new ConditionQTE(reader));
                    break;

                default:
                    throw new Exception("Unknown DvCondition type!");
                    break;
            }
        }
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.Write(DestPageIndex); 
        writer.Write(Conditions.Count);
        var conditionSizePos = writer.Position;
        writer.WriteNulls(8);
        var conditionSize = writer.Position;
        writer.WriteObjectCollection(Conditions);
        conditionSize = writer.Position - conditionSize;
        writer.WriteAt(conditionSizePos, (int)conditionSize);
    }
}

public class Condition : IBinarySerializable
{
    public enum Type : uint
    {
        PageEnd = 4,
        QTE = 1000
    }

    public Type ConditionType { get; set; }
    long parameterSizePos = 0;
    long parameterSize = 0;

    public Condition() { }
    public Condition(BinaryObjectReader reader) => Read(reader);

    public virtual void Read(BinaryObjectReader reader)
    {
        ConditionType = reader.Read<Type>();
        var ParameterSize = reader.Read<int>();
        reader.Skip(8);
    }

    public virtual void Write(BinaryObjectWriter writer)
    {
        writer.Write(ConditionType);
        parameterSizePos = writer.Position;
        writer.WriteNulls(12);
        parameterSize = writer.Position;
    }

    protected void FinishWrite(BinaryObjectWriter writer)
    {
        parameterSize = writer.Position - parameterSize;
        writer.WriteAt(parameterSizePos, (int)parameterSize);
    }
}

public class ConditionPageEnd : Condition
{
    public ConditionPageEnd() { }
    public ConditionPageEnd(BinaryObjectReader reader) => Read(reader);

    public override void Write(BinaryObjectWriter writer)
    {
        base.Write(writer);
        FinishWrite(writer);
    }
}

public class ConditionQTE : Condition
{
    public bool Failed { get; set; }
    public int Unk0 { get; set; }
    public int Unk1 { get; set; }
    public int Unk2 { get; set; }

    public ConditionQTE() { }
    public ConditionQTE(BinaryObjectReader reader) => Read(reader);

    public override void Read(BinaryObjectReader reader)
    {
        base.Read(reader);
        Failed = reader.Read<bool>();
        reader.Align(4);
        Unk0 = reader.Read<int>();
        Unk1 = reader.Read<int>();
        Unk2 = reader.Read<int>();
    }

    public override void Write(BinaryObjectWriter writer)
    {
        base.Write(writer);
        writer.Write(Failed);
        writer.Align(4);
        writer.Write(Unk0);
        writer.Write(Unk1);
        writer.Write(Unk2);
        FinishWrite(writer);
    }
}