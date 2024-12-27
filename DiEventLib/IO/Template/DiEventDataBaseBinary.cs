using Amicitia.IO.Binary;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib.IO.Template;

public class DiEventDataBaseBinary
{
    public class Base
    {
        public string Name = "";
        public Dictionary<string, string> Descriptions = new();

        public void ReadBase(BinaryObjectReader reader)
        {
            Name = reader.ReadStringTableEntry();
            if (DataBaseBinaryHandler.Descriptions)
            {
                int descriptionCount = reader.Read<int>();
                for (int i = 0; i < descriptionCount; i++)
                {
                    string key = reader.ReadStringTableEntry();
                    string value = reader.ReadStringTableEntry();
                    Descriptions.Add(key, value);
                }
            }
        }

        public void WriteBase(BinaryObjectWriter writer)
        {
            writer.WriteStringTableEntry(Name);
            if (DataBaseBinaryHandler.Descriptions)
            {
                writer.Write(Descriptions.Count);
                foreach (var i in Descriptions)
                {
                    writer.WriteStringTableEntry(i.Key);
                    writer.WriteStringTableEntry(i.Value);
                }
            }
        }
    }

    public class Struct : IBinarySerializable
    {
        public string StructName = "";
        public List<Field> Fields = new List<Field>();

        public void Read(BinaryObjectReader reader)
        {
            StructName = reader.ReadStringTableEntry();
            int fieldCount = reader.Read<int>();
            for(int i = 0; i < fieldCount; i++)
            {
                Field field = new();
                field.Read(reader);
                Fields.Add(field);
            }
        }

        public void Write(BinaryObjectWriter writer)
        {
            writer.WriteStringTableEntry(StructName);
            writer.Write(Fields.Count);
            foreach(var i in Fields)
                i.Write(writer);
        }
    }

    public class Field : Base
    {
        public enum DataType : byte
        {
            none = 255, u8 = 0, s8, u16, s16, u32, s32, f32, vec2, vec3, vec4, mat4x4, curve, str, enm, strct, guid, array, arraysize, boolean, rgba8, rgb32, rgba32, padding
        };

        public DataType Type = DataType.none;
        public DataType SubType = DataType.none;
        public Enum EnumType = new();
        public Struct StructValue = new();
        public short Size = 0;
        public string ArraySizeField = "";

        public class Enum
        {
            public string Name = "";
            public Dictionary<string, int> Values = new();

            public void Read(BinaryObjectReader reader)
            {
                Name = reader.ReadStringTableEntry();
                int valueCount = reader.Read<int>();
                for(int i = 0; i < valueCount; i++)
                {
                    string key = reader.ReadStringTableEntry();
                    int value = reader.Read<int>();
                    Values.Add(key, value);
                }
            }

            public void Write(BinaryObjectWriter writer)
            {
                writer.WriteStringTableEntry(Name);
                writer.Write(Values.Count);
                foreach(var i in Values)
                {
                    writer.WriteStringTableEntry(i.Key);
                    writer.Write(i.Value);
                }
            }
        }

        public void Read(BinaryObjectReader reader)
        {
            ReadBase(reader);
            Type = reader.Read<DataType>();
            SubType = reader.Read<DataType>();
            Size = reader.Read<short>();
            switch (Type)
            {
                case DataType.array or DataType.arraysize:
                    ArraySizeField = reader.ReadStringTableEntry();
                    break;
                case DataType.strct:
                    StructValue.Read(reader); 
                    break;
                case DataType.enm:
                    EnumType.Read(reader); 
                    break;
            }
        }

        public void Write(BinaryObjectWriter writer)
        {
            WriteBase(writer);
            writer.Write(Type);
            writer.Write(SubType);
            writer.Write(Size);
            switch(Type)
            {
                case DataType.array or DataType.arraysize:
                    writer.WriteStringTableEntry(ArraySizeField);
                    break;
                case DataType.strct:
                    StructValue.Write(writer);
                    break;
                case DataType.enm:
                    EnumType.Write(writer);
                    break;
            }
        }
    }

    public class Node : Base
    {
        public string FullName = "";
        public int NodeCategory = 0;
        public List<Field> Fields = new();

        public void Read(BinaryObjectReader reader)
        {
            ReadBase(reader);
            FullName = reader.ReadStringTableEntry();
            NodeCategory = reader.Read<int>();
            int fieldCount = reader.Read<int>();
            for(int i = 0; i < fieldCount; i++)
            {
                Field field = new();
                field.Read(reader);
                Fields.Add(field);
            }
        }

        public void Write(BinaryObjectWriter writer)
        {
            WriteBase(writer);
            writer.WriteStringTableEntry(FullName);
            writer.Write(NodeCategory);
            writer.Write(Fields.Count);
            foreach(var i in Fields)
                i.Write(writer);
        }
    }

    public const string FileExtension = ".dievtdb";
    public const string Signature = "DiEvtDB";

    public int Version = 1;
    public List<Node> Nodes = new();
    public List<Node> Elements = new();
    public Endianness Endianness = Endianness.Little;

    [Flags]
    public enum Flags : byte
    {
        BigEndian = 1,
        Descriptions = 2
    }

    public void Read(BinaryObjectReader reader)
    {
        string signature = reader.ReadString(StringBinaryFormat.FixedLength, 7);
        if (signature != Signature)
            throw new Exception("Not a DiEvtDB file!");
        Flags flags = reader.Read<Flags>();
        if (flags.HasFlag(Flags.BigEndian))
            Endianness = Endianness.Big;
        if (flags.HasFlag(Flags.Descriptions))
            DataBaseBinaryHandler.Descriptions = true;
        reader.Endianness = Endianness;
        Version = reader.Read<int>();
        short nodeCount = reader.Read<short>();
        short elementCount = reader.Read<short>();
        for(int i = 0; i < nodeCount; i++)
        {
            Node node = new();
            node.Read(reader);
            Nodes.Add(node);
        }
        for(int i = 0; i < elementCount; i++)
        {
            Node node = new();
            node.Read(reader);
            Elements.Add(node);
        }
    }

    public void Write(BinaryObjectWriter writer)
    {
        writer.WriteString(StringBinaryFormat.FixedLength, Signature, 7);
        Flags flags = 0;
        if(writer.Endianness == Endianness.Big)
            flags |= Flags.BigEndian;
        foreach (var i in Nodes)
            if (i.Descriptions.Count > 0)
            {
                DataBaseBinaryHandler.Descriptions = true;
                break;
            }
        if (DataBaseBinaryHandler.Descriptions)
            flags |= Flags.Descriptions;
        Endianness = writer.Endianness;
        writer.Write(flags);
        writer.Write(Version);
        writer.Write((short)Nodes.Count);
        writer.Write((short)Elements.Count);
        foreach(var i in Nodes)
            i.Write(writer);
        foreach (var i in Elements)
            i.Write(writer);
        writer.WriteStringTable();
        writer.Dispose();
        StringTableHandler.ClearHandler();
    }
}

public static class DataBaseBinaryHandler
{
    public static bool Descriptions = false;
}

public static class StringTableHandler
{
    public static Dictionary<long, long> StringTableOffsets = new();
    public static Dictionary<string, long> stringTableRaw = new();
    public static string StringTable = "";

    public static void WriteStringTableEntry(this BinaryObjectWriter writer, string rawEntry)
    {
        string entry = rawEntry;

        if (rawEntry == "")
        {
            writer.Write<long>(0);
            return;
        }

        if (!stringTableRaw.ContainsKey(entry))
        {
            StringTableOffsets.Add(writer.Position, StringTable.Length);
            stringTableRaw.Add(entry, StringTable.Length);
            writer.Write<long>(StringTable.Length);
            foreach (var i in entry.ToCharArray())
                StringTable += i;

            StringTable += '\0';
        }
        else
        {
            StringTableOffsets.Add(writer.Position, stringTableRaw[entry]);
            writer.Write<long>(stringTableRaw[entry]);
        }
    }

    public static void WriteStringTable(this BinaryObjectWriter writer)
    {
        long stringtableoffset = writer.Position;
        writer.WriteString(StringBinaryFormat.FixedLength, StringTable, StringTable.Length);

        foreach (var i in StringTableOffsets)
        {
            writer.Seek(i.Key, SeekOrigin.Begin);
            writer.Write(i.Value + stringtableoffset);
        }
    }

    public static void ClearHandler()
    {
        StringTableOffsets = new();
        stringTableRaw = new();
        StringTable = "";
    }
}