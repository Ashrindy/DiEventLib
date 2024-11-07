using Amicitia.IO.Binary;
using System.Reflection;
using System.Text;
using System.Text.Json;
using static DiEventLib.IO.Template.DiEventDataBase;

namespace DiEventLib.IO.Template;

public class DiEventDataBase
{
    public class Base
    {
        public string Name = "";
        public Dictionary<string, string> Descriptions = new();
    }

    public class Node : Base
    {
        public string FullName = "";
        public int NodeCategory = 0;
        public List<Field> Fields = new();
    }

    public class Field : Base
    {
        public enum DataType : byte
        {
            none = 255, u8 = 0, s8, u16, s16, u32, s32, f32, vec2, vec3, vec4, mat4x4, curve, str, enm, strct, array, arraysize, boolean, rgba8, rgb32, padding
        };

        public DataType Type = DataType.u8;
        public int Size = 0;

        public class Enum
        {
            public string Name = "";
            public DataType Type = DataType.none;
            public Dictionary<string, int> Values = new();
        }

        public class Struct
        {
            public string Name = "";
            public List<Field> Fields = new List<Field>();
        }
    }

    public class FieldEnum : Field
    {
        public Enum EnumType = new();
    }

    public class FieldStruct : Field
    {
        public Struct StructValue = new();
    }

    public class FieldArray : Field
    {
        public DataType SubType = DataType.u8;
    }

    public class FieldDynamicArray : Field
    {
        public DataType SubType = DataType.u8;
        public string ArraySizeField = "";
    }

    public class FieldArraySize : Field
    {
        public DataType SubType = DataType.u8;
        public string ArraySizeField = "";
    }

    public int Version = 1;
    public List<Node> Nodes = new();
    public List<Node> Elements = new();

    bool hasDescriptions = false;

    public void Open(DvScene.DvSceneVersion template)
    {
        var assembly = Assembly.GetExecutingAssembly();
        string fullResourceName = $"{assembly.GetName().Name}.{template.ToString()}.dievtdb";

        using (Stream stream = assembly.GetManifestResourceStream(fullResourceName))
        {
            if (stream == null)
                throw new FileNotFoundException("Resource not found", fullResourceName);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                Load(memoryStream.ToArray());
            }
        }
    }

    public void Open(string filename) => Load(File.ReadAllBytes(filename));


    public void Load(byte[] fdata)
    {
        BinaryObjectReader reader = new(new MemoryStream(fdata), Amicitia.IO.Streams.StreamOwnership.Retain, Endianness.Little);
        string signature = reader.ReadString(StringBinaryFormat.FixedLength, 7);
        reader.Dispose();
        if (signature == DiEventDataBaseBinary.Signature)
            LoadBinary(fdata);
        else
            LoadJSON(fdata);
    }

    public void OpenBinary(string filename) => LoadBinary(File.ReadAllBytes(filename));

    public void LoadBinary(byte[] fdata)
    {
        DiEventDataBaseBinary data = new();
        data.Read(new(new MemoryStream(fdata), Amicitia.IO.Streams.StreamOwnership.Retain, Amicitia.IO.Binary.Endianness.Little));
        Version = data.Version;
        foreach (var i in data.Nodes)
        {
            Node node = new();
            node.Name = i.Name;
            node.FullName = i.FullName;
            node.Descriptions = i.Descriptions;
            node.NodeCategory = i.NodeCategory;
            foreach (var x in i.Fields)
                node.Fields.Add(ConvertBinaryFieldToField(x));
            Nodes.Add(node);
        }
        foreach (var i in data.Elements)
        {
            Node node = new();
            node.Name = i.Name;
            node.FullName = i.FullName;
            node.Descriptions = i.Descriptions;
            node.NodeCategory = i.NodeCategory;
            foreach (var x in i.Fields)
                node.Fields.Add(ConvertBinaryFieldToField(x));
            Elements.Add(node);
        }
    }

    Field ConvertBinaryFieldToField(DiEventDataBaseBinary.Field x)
    {
        switch (x.Type)
        {
            case DiEventDataBaseBinary.Field.DataType.strct:
                FieldStruct fStruct = new();
                fStruct.Name = x.Name;
                fStruct.Type = Field.DataType.strct;
                fStruct.Descriptions = x.Descriptions;
                fStruct.StructValue = new();
                fStruct.StructValue.Name = x.StructValue.StructName;
                foreach (var y in x.StructValue.Fields)
                    fStruct.StructValue.Fields.Add(ConvertBinaryFieldToField(y));
                return fStruct;
                break;

            case DiEventDataBaseBinary.Field.DataType.enm:
                FieldEnum fEnm = new();
                fEnm.Name = x.Name;
                fEnm.Type = Field.DataType.enm;
                fEnm.Descriptions = x.Descriptions;
                fEnm.EnumType = new();
                fEnm.EnumType.Name = x.EnumType.Name;
                fEnm.EnumType.Type = (Field.DataType)(byte)x.SubType;
                fEnm.EnumType.Values = x.EnumType.Values;
                return fEnm;
                break;

            case DiEventDataBaseBinary.Field.DataType.array:
                if(x.ArraySizeField != "")
                {
                    FieldDynamicArray fArray = new();
                    fArray.Name = x.Name;
                    fArray.Type = Field.DataType.array;
                    fArray.Descriptions = x.Descriptions;
                    fArray.SubType = (Field.DataType)(byte)x.SubType;
                    fArray.Size = x.Size;
                    fArray.ArraySizeField = x.ArraySizeField;
                    return fArray;
                }
                else
                {
                    FieldArray fArray = new();
                    fArray.Name = x.Name;
                    fArray.Type = Field.DataType.array;
                    fArray.Descriptions = x.Descriptions;
                    fArray.SubType = (Field.DataType)(byte)x.SubType;
                    fArray.Size = x.Size;
                    return fArray;
                }
                break;

            case DiEventDataBaseBinary.Field.DataType.arraysize:
                FieldArraySize fArraySize = new();
                fArraySize.Name = x.Name;
                fArraySize.Type = Field.DataType.arraysize;
                fArraySize.Descriptions = x.Descriptions;
                fArraySize.SubType = (Field.DataType)(byte)x.SubType;
                fArraySize.Size = x.Size;
                fArraySize.ArraySizeField = x.ArraySizeField;
                return fArraySize;
                break;

            default:
                Field f = new();
                f.Name = x.Name;
                f.Type = (Field.DataType)(byte)x.Type;
                f.Descriptions = x.Descriptions;
                f.Size = x.Size;
                return f;
                break;
        }
    }

    public void OpenJSON(string filename) => LoadJSON(File.ReadAllBytes(filename));

    public void LoadJSON(byte[] fdata)
    {
        DiEventDataBaseJSON.MainJSON json = JsonSerializer.Deserialize<DiEventDataBaseJSON.MainJSON>(Encoding.Default.GetString(fdata));
        Version = json.Version;
        hasDescriptions = json.HasDescriptions;
        foreach (var i in json.Nodes)
        {
            Node node = new();
            node.Name = i.Name;
            if (json.HasDescriptions)
                node.Descriptions = i.Descriptions;
            node.FullName = i.FullName;
            node.NodeCategory = i.ID;
            foreach (var x in i.Fields)
                node.Fields.Add(ConvertJSONFieldToField(x));
            Nodes.Add(node);
        }
        if (json.Elements != null)
        {
            foreach (var i in json.Elements)
            {
                Node node = new();
                node.Name = i.Name;
                if (json.HasDescriptions)
                    node.Descriptions = i.Descriptions;
                node.FullName = i.FullName;
                node.NodeCategory = i.ID;
                foreach (var x in i.Fields)
                    node.Fields.Add(ConvertJSONFieldToField(x));
                Elements.Add(node);
            }
        }
    }

    Field ConvertJSONFieldToField(DiEventDataBaseJSON.FieldJSON x)
    {
        switch ((Field.DataType)Enum.Parse(typeof(Field.DataType), x.Type))
        {
            case Field.DataType.strct:
                FieldStruct fStruct = new();
                fStruct.Name = x.Name;
                fStruct.Type = Field.DataType.strct;
                if (hasDescriptions)
                    fStruct.Descriptions = x.Descriptions;
                fStruct.StructValue = new();
                fStruct.StructValue.Name = x.Struct.Name;
                foreach (var y in x.Struct.Fields)
                    fStruct.StructValue.Fields.Add(ConvertJSONFieldToField(y));
                return fStruct;
                break;

            case Field.DataType.enm:
                FieldEnum fEnm = new();
                fEnm.Name = x.Name;
                fEnm.Type = Field.DataType.enm;
                if (hasDescriptions)
                    fEnm.Descriptions = x.Descriptions;
                fEnm.EnumType = new();
                fEnm.EnumType.Name = x.Enum.Name;
                fEnm.EnumType.Type = (Field.DataType)Enum.Parse(typeof(Field.DataType), x.SubType);
                fEnm.EnumType.Values = x.Enum.Values;
                return fEnm;
                break;

            case Field.DataType.array:
                if(x.ArraySizeField != null)
                {
                    FieldDynamicArray fArray = new();
                    fArray.Name = x.Name;
                    fArray.Type = Field.DataType.array;
                    if (hasDescriptions)
                        fArray.Descriptions = x.Descriptions;
                    fArray.SubType = (Field.DataType)Enum.Parse(typeof(Field.DataType), x.SubType);
                    fArray.Size = x.Size;
                    fArray.ArraySizeField = x.ArraySizeField;
                    return fArray;
                }
                else
                {
                    FieldArray fArray = new();
                    fArray.Name = x.Name;
                    fArray.Type = Field.DataType.array;
                    if (hasDescriptions)
                        fArray.Descriptions = x.Descriptions;
                    fArray.SubType = (Field.DataType)Enum.Parse(typeof(Field.DataType), x.SubType);
                    fArray.Size = x.Size;
                    return fArray;
                }
                break;

            case Field.DataType.arraysize:
                FieldArraySize fArraySize = new();
                fArraySize.Name = x.Name;
                fArraySize.Type = Field.DataType.arraysize;
                if (hasDescriptions)
                    fArraySize.Descriptions = x.Descriptions;
                fArraySize.SubType = (Field.DataType)Enum.Parse(typeof(Field.DataType), x.SubType);
                fArraySize.Size = x.Size;
                fArraySize.ArraySizeField = x.ArraySizeField;
                return fArraySize;
                break;

            default:
                Field f = new();
                f.Name = x.Name;
                f.Type = (Field.DataType)Enum.Parse(typeof(Field.DataType), x.Type);
                if (hasDescriptions)
                    f.Descriptions = x.Descriptions;
                f.Size = x.Size;
                return f;
                break;
        }
    }


    public void SaveBinary(string filename)
    {
        DiEventDataBaseBinary binary = new();
        binary.Version = Version;
        foreach(var i in Nodes)
            binary.Nodes.Add(ConvertNodeToBinaryNode(i));
        foreach (var i in Elements)
            binary.Elements.Add(ConvertNodeToBinaryNode(i));
        binary.Write(new(filename, Endianness.Little, Encoding.Default));
    }

    DiEventDataBaseBinary.Node ConvertNodeToBinaryNode(Node i)
    {
        DiEventDataBaseBinary.Node node = new();
        node.Name = i.Name;
        node.Descriptions = i.Descriptions;
        node.FullName = i.FullName;
        node.NodeCategory = i.NodeCategory;
        foreach (var x in i.Fields)
            node.Fields.Add(ConvertFieldToBinaryField(x));
        return node;
    }
    DiEventDataBaseBinary.Field ConvertFieldToBinaryField(Field x)
    {
        DiEventDataBaseBinary.Field field = new();
        field.Name = x.Name;
        field.Descriptions = x.Descriptions;
        field.Type = (DiEventDataBaseBinary.Field.DataType)((byte)x.Type);
        field.Size = (short)x.Size;
        switch(x.Type)
        {
            case Field.DataType.strct:
                FieldStruct str = ((FieldStruct)x);
                field.StructValue = new();
                field.StructValue.StructName = str.StructValue.Name;
                foreach(var i in str.StructValue.Fields)
                    field.StructValue.Fields.Add(ConvertFieldToBinaryField(i));
                break;

            case Field.DataType.enm:
                FieldEnum enm = ((FieldEnum)x);
                field.EnumType = new();
                field.EnumType.Name = enm.EnumType.Name;
                field.SubType = (DiEventDataBaseBinary.Field.DataType)((byte)enm.EnumType.Type);
                field.EnumType.Values = enm.EnumType.Values;
                break;

            case Field.DataType.array:
                if(x.GetType() == typeof(FieldDynamicArray))
                {
                    FieldDynamicArray array = ((FieldDynamicArray)x);
                    field.SubType = (DiEventDataBaseBinary.Field.DataType)((byte)array.SubType);
                    field.ArraySizeField = array.ArraySizeField;
                }
                else
                {
                    FieldArray array = ((FieldArray)x);
                    field.SubType = (DiEventDataBaseBinary.Field.DataType)((byte)array.SubType);
                }
                break;

            case Field.DataType.arraysize:
                FieldArraySize arraysize = ((FieldArraySize)x);
                field.SubType = (DiEventDataBaseBinary.Field.DataType)((byte)arraysize.SubType);
                field.ArraySizeField = arraysize.ArraySizeField;
                break;
        }
        return field;
    }

    public void SaveJSON(string filename)
    {
        DiEventDataBaseJSON.MainJSON json = new();
        json.Version = Version;
        foreach(var i in Nodes)
            if(i.Descriptions.Count > 0)
            {
                hasDescriptions = true;
                break;
            }   
        json.HasDescriptions = hasDescriptions;
        json.Nodes = new();
        json.Elements = new();
        foreach(var i in Nodes)
            json.Nodes.Add(ConvertNodeToJSONNode(i));
        foreach (var i in Elements)
            json.Elements.Add(ConvertNodeToJSONNode(i));
        JsonSerializerOptions options = new();
        options.WriteIndented = true;
        options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        File.WriteAllText(filename, JsonSerializer.Serialize(json, options: options));
    }

    DiEventDataBaseJSON.NodeJSON ConvertNodeToJSONNode(Node i)
    {
        DiEventDataBaseJSON.NodeJSON node = new();
        node.Name = i.Name;
        if (hasDescriptions)
            node.Descriptions = i.Descriptions;
        else
            node.Descriptions = null;
        node.FullName = i.FullName;
        node.ID = i.NodeCategory;
        node.Fields = new();
        foreach (var x in i.Fields)
            node.Fields.Add(ConvertFieldToJSONField(x));
        return node;
    }
    DiEventDataBaseJSON.FieldJSON ConvertFieldToJSONField(Field x)
    {
        DiEventDataBaseJSON.FieldJSON field = new();
        field.Name = x.Name;
        field.Descriptions = x.Descriptions;
        field.Type = x.Type.ToString();
        field.Size = x.Size;
        switch (x.Type)
        {
            case Field.DataType.strct:
                FieldStruct str = ((FieldStruct)x);
                field.Struct = new();
                field.Struct.Name = str.StructValue.Name;
                foreach (var i in str.StructValue.Fields)
                    field.Struct.Fields.Add(ConvertFieldToJSONField(i));
                break;

            case Field.DataType.enm:
                FieldEnum enm = ((FieldEnum)x);
                field.Enum = new();
                field.Enum.Name = enm.EnumType.Name;
                field.SubType = enm.EnumType.Type.ToString();
                field.Enum.Values = enm.EnumType.Values;
                break;

            case Field.DataType.array:
                if(x.GetType() == typeof(FieldDynamicArray))
                {
                    FieldDynamicArray array = ((FieldDynamicArray)x);
                    field.SubType = array.SubType.ToString();
                    field.ArraySizeField = array.ArraySizeField;
                }
                else 
                {
                    FieldArray array = ((FieldArray)x);
                    field.SubType = array.SubType.ToString();
                }
                break;

            case Field.DataType.arraysize:
                FieldArraySize arraysize = ((FieldArraySize)x);
                field.SubType = arraysize.SubType.ToString();
                field.ArraySizeField = arraysize.ArraySizeField;
                break;
        }
        return field;
    }
}
