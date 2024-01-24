using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;


public class DvPageConditionQTE : DvObject, IBinarySerializable
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
        throw new NotImplementedException();
    }
}


public class DvPage : IBinarySerializable
{
    public uint Version { get; set; }
    public uint Flags { get; set; }
    public uint Start { get; set; }
    public uint End { get; set; }
    public int TransitionCount { get; set; }
    public uint TransitionSize { get; set; }
    public uint SkipFrame { get; set; }
    public uint Index { get; set; }
    public uint SkipLinkIndexNum { get; set; }
    public string Name { get; set; }
    public int Field50 { get; set; }
    public int Field54 { get; set; }
    public int Field58 { get; set; }
    public int Field5C { get; set; }
    public List<Transition> Transitions { get; set; } = new();

    public void Read(BinaryObjectReader reader)
    {
        Version = reader.Read<uint>();
        Flags = reader.Read<uint>();
        Start = reader.Read<uint>() / 100;
        End = reader.Read<uint>() / 100;
        TransitionCount = reader.Read<int>();
        TransitionSize = reader.Read<uint>();
        SkipFrame = reader.Read<uint>() / 100;
        Index = reader.Read<uint>();
        SkipLinkIndexNum = reader.Read<uint>();
        reader.Skip(12);
        Name = reader.ReadString(Encoding.UTF8,StringBinaryFormat.FixedLength, 64);
        if (SkipLinkIndexNum != 0)
        {
            Field50 = reader.Read<int>();
            Field54 = reader.Read<int>();
            Field58 = reader.Read<int>();
            Field5C = reader.Read<int>();
        }
        Transitions.AddRange(reader.ReadObjectArray<Transition>(TransitionCount));

    }

    public void Write(BinaryObjectWriter writer)
    {
        throw new NotImplementedException();
    }
}

public class Transition : IBinarySerializable
{
    public int DestPageIndex { get; set; }
    public int ConditionNum { get; set; }
    public uint ConditionSize { get; set; }
    public List<Condition> Conditions { get; set; } = new();

    public void Read(BinaryObjectReader reader)
    {
        DestPageIndex = reader.Read<int>();
        ConditionNum = reader.Read<int>();
        ConditionSize = reader.Read<uint>();
        reader.Skip(4);
        Conditions.AddRange(reader.ReadObjectArray<Condition>(ConditionNum));


    }

    public void Write(BinaryObjectWriter writer)
    {
        throw new NotImplementedException();
    }
}

public class Condition : IBinarySerializable
{
    // TODO: Find Conditions types
    // Probably using for value type;
    public int ConditionType { get; set; }
    public int ParameterSize { get; set; }
    public byte[] Data { get; set; }

    public void Read(BinaryObjectReader reader)
    {
        ConditionType = reader.Read<int>();
        ParameterSize = reader.Read<int>();
        reader.Skip(8);
        Data = reader.ReadArray<byte>(ParameterSize);
    }

    public void Write(BinaryObjectWriter writer)
    {
        throw new NotImplementedException();
    }
}