using Amicitia.IO.Binary;
using DiEventLib.IO.Template;
using System;
using System.IO;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Xml.Linq;
namespace DiEventLib;

public class DvNodeTemplate : DvNode
{
    public string Category = "";
    public Dictionary<string, Field> Fields = new();
    Dictionary<string, Field> arraycounts = new();

    public override string ToString() => $"{NodeName} - {Category}";

    public void Read(BinaryObjectReader reader, DiEventDataBase.Node db)
    {
        foreach(var i in db.Fields)
        {
            Tuple<string, Field> fld = ReadField(reader, i);
            if(fld != null)
            {
                if(i.Type == DiEventDataBase.Field.DataType.arraysize)
                    arraycounts.Add(fld.Item1, fld.Item2);
                else
                    Fields.Add(fld.Item1, fld.Item2);
            }
        }
    }

    protected Tuple<string, Field> ReadField(BinaryObjectReader reader, DiEventDataBase.Field fld)
    {
        Field? field = ReadFieldValue(reader, fld);
        if (field == null)
            return null;
        return new(fld.Name, (Field)field);
    }

    protected Field? ReadFieldValue(BinaryObjectReader reader, DiEventDataBase.Field fld)
    {
        Field? field = null;
        switch (fld.Type)
        {
            case DiEventDataBase.Field.DataType.u8 or DiEventDataBase.Field.DataType.s8:
                field = new() { Value = reader.Read<byte>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.u16:
                field = new() { Value = reader.Read<ushort>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.s16:
                field = new() { Value = reader.Read<short>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.u32:
                field = new() { Value = reader.Read<uint>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.s32:
                field = new() { Value = reader.Read<int>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.boolean:
                field = new() { Value = ((reader.Read<byte>() == 1) ? true : false), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.str:
                field = new() { Value = reader.ReadString(StringBinaryFormat.FixedLength, fld.Size), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.f32:
                field = new() { Value = reader.Read<float>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.vec2:
                field = new() { Value = reader.Read<Vector2>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.vec3:
                field = new() { Value = reader.Read<Vector3>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.vec4:
                field = new() { Value = reader.Read<Vector4>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.mat4x4:
                field = new() { Value = reader.Read<Matrix4x4>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.curve:
                field = new() { Value = reader.ReadArray<float>(fld.Size), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.array:
                if(fld.GetType() == typeof(DiEventDataBase.FieldDynamicArray))
                {
                    DiEventDataBase.FieldDynamicArray fDA = (DiEventDataBase.FieldDynamicArray)fld;
                    List<Field> flds = new();
                    for(int i = 0; i < Convert.ToInt32(arraycounts[fDA.ArraySizeField].Value); i++)
                        flds.Add((Field)ReadFieldValue(reader, new() { Type = fDA.SubType, Size = fDA.Size }));
                    field = new() { Value = flds, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                }
                else if (fld.GetType() == typeof(DiEventDataBase.FieldStructDynamicArray))
                {
                    DiEventDataBase.FieldStructDynamicArray fDA = (DiEventDataBase.FieldStructDynamicArray)fld;
                    List<Field> flds = new();
                    for (int i = 0; i < Convert.ToInt32(arraycounts[fDA.ArraySizeField].Value); i++)
                    {
                        Dictionary<string, Field> fldstf = new();
                        foreach (var x in fDA.StructValue.Fields)
                        {
                            Tuple<string, Field> fd = ReadField(reader, x);
                            fldstf.Add(fd.Item1, fd.Item2);
                        }
                        flds.Add(new Field() { Value = fldstf, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) });
                    }
                    field = new() { Value = flds, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                }
                else if (fld.GetType() == typeof(DiEventDataBase.FieldStructArray))
                {
                    DiEventDataBase.FieldStructArray fA = (DiEventDataBase.FieldStructArray)fld;
                    Field[] flds = new Field[fA.Size];
                    for (int i = 0; i < fA.Size; i++)
                    {
                        Dictionary<string, Field> fldstf = new();
                        foreach (var x in fA.StructValue.Fields)
                        {
                            Tuple<string, Field> fd = ReadField(reader, x);
                            fldstf.Add(fd.Item1, fd.Item2);
                        }
                        flds[i] = new Field() { Value = fldstf, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                    }
                    field = new() { Value = flds, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                }
                else
                {
                    DiEventDataBase.FieldArray fA = (DiEventDataBase.FieldArray)fld;
                    Field[] flds = new Field[fA.Size];
                    for (int i = 0; i < fld.Size; i++)
                        flds[i] = (Field)(ReadFieldValue(reader, new() { Type = fA.SubType, Size = fA.Size }));
                        
                    field = new() { Value = flds, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                }
                break;

            case DiEventDataBase.Field.DataType.arraysize:
                field = ReadFieldValue(reader, new() { Type = ((DiEventDataBase.FieldArraySize)fld).SubType });
                break;

            case DiEventDataBase.Field.DataType.rgba8:
                field = new() { Value = reader.Read<RGBA32>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.rgb32:
                field = new() { Value = reader.Read<RGB32F>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.padding:
                reader.Skip(fld.Size);
                break;

            case DiEventDataBase.Field.DataType.strct:
                DiEventDataBase.FieldStruct fS = (DiEventDataBase.FieldStruct)fld;
                Dictionary<string, Field> fldst = new();
                foreach(var i in fS.StructValue.Fields)
                {
                    Tuple<string, Field> fd = ReadField(reader, i);
                    fldst.Add(fd.Item1, fd.Item2);
                }
                field = new() { Value = fldst, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.enm:
                DiEventDataBase.FieldEnum fE = (DiEventDataBase.FieldEnum)fld;
                Enum enm = new();
                enm.Value = Convert.ToInt32(ReadFieldValue(reader, new() { Type = fE.EnumType.Type }).Value.Value);
                enm.Values = fE.EnumType.Values;
                field = new() { Value = enm, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.rgba32:
                field = new() { Value = reader.Read<RGBA32>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.guid:
                field = new() { Value = reader.Read<Guid>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;
        }
        return field;
    }

    public void Write(BinaryObjectWriter writer, DiEventDataBase.Node db)
    {
        foreach(var i in db.Fields)
        {
            if (Fields.ContainsKey(i.Name))
                WriteFieldValue(writer, Fields[i.Name], i);
            else
                WriteFieldValue(writer, new() { DataType = (DataType)((byte)i.Type), Value = null }, i);
        }  
    }

    protected void WriteFieldValue(BinaryObjectWriter writer, Field field, DiEventDataBase.Field fld)
    {
        switch(fld.Type)
        {
            case DiEventDataBase.Field.DataType.u8 or DiEventDataBase.Field.DataType.s8:
                writer.Write(Convert.ToByte(field.Value));
                break;

            case DiEventDataBase.Field.DataType.u16:
                writer.Write(Convert.ToUInt16(field.Value));
                break;

            case DiEventDataBase.Field.DataType.s16:
                writer.Write(Convert.ToInt16(field.Value));
                break;

            case DiEventDataBase.Field.DataType.u32:
                writer.Write(Convert.ToUInt32(field.Value));
                break;

            case DiEventDataBase.Field.DataType.s32:
                writer.Write(Convert.ToInt32(field.Value));
                break;

            case DiEventDataBase.Field.DataType.boolean:
                writer.Write((byte)(((bool)field.Value) ? 1 : 0));
                break;

            case DiEventDataBase.Field.DataType.str:
                writer.WriteString(StringBinaryFormat.FixedLength, (string)field.Value, fld.Size);
                break;

            case DiEventDataBase.Field.DataType.f32:
                writer.Write((float)field.Value);
                break;

            case DiEventDataBase.Field.DataType.vec2:
                writer.Write((Vector2)field.Value);
                break;

            case DiEventDataBase.Field.DataType.vec3:
                writer.Write((Vector3)field.Value);
                break;

            case DiEventDataBase.Field.DataType.vec4:
                writer.Write((Vector4)field.Value);
                break;

            case DiEventDataBase.Field.DataType.mat4x4:
                writer.Write((Matrix4x4)field.Value);
                break;

            case DiEventDataBase.Field.DataType.curve:
                writer.WriteArray((float[])field.Value);
                break;

            case DiEventDataBase.Field.DataType.array:
                if (fld.GetType() == typeof(DiEventDataBase.FieldDynamicArray))
                {
                    DiEventDataBase.FieldDynamicArray fDA = (DiEventDataBase.FieldDynamicArray)fld;
                    List<Field> flds = (List<Field>)field.Value;
                    for (int i = 0; i < Convert.ToInt32(arraycounts[fDA.ArraySizeField].Value); i++)
                        WriteFieldValue(writer, flds[i], new() { Type = fDA.SubType });
                }
                else if (fld.GetType() == typeof(DiEventDataBase.FieldStructDynamicArray))
                {
                    DiEventDataBase.FieldStructDynamicArray fDA = (DiEventDataBase.FieldStructDynamicArray)fld;
                    List<Field> flds = (List<Field>)field.Value;
                    for (int i = 0; i < Convert.ToInt32(arraycounts[fDA.ArraySizeField].Value); i++)
                    {
                        Dictionary<string, Field> fldstf = (Dictionary<string, Field>)flds[i].Value;
                        for (int x = 0; x < fldstf.Count; x++)
                            WriteFieldValue(writer, fldstf.ElementAt(x).Value, fDA.StructValue.Fields[x]);
                    }
                }
                else if (fld.GetType() == typeof(DiEventDataBase.FieldStructArray))
                {
                    DiEventDataBase.FieldStructArray fA = (DiEventDataBase.FieldStructArray)fld;
                    Field[] flds = (Field[])field.Value;
                    for (int i = 0; i < fld.Size; i++)
                    {
                        Dictionary<string, Field> fldstf = (Dictionary<string, Field>)flds[i].Value;
                        for (int x = 0; x < fldstf.Count; x++)
                            WriteFieldValue(writer, fldstf.ElementAt(x).Value, fA.StructValue.Fields[x]);
                    }
                }
                else
                {
                    DiEventDataBase.FieldArray fA = (DiEventDataBase.FieldArray)fld;
                    Field[] flds = (Field[])field.Value;
                    for (int i = 0; i < fld.Size; i++)
                        WriteFieldValue(writer, flds[i], new() { Type = fA.SubType });
                }
                break;

            case DiEventDataBase.Field.DataType.arraysize:
                if (GetType() == typeof(DvNodeTemplate))
                    WriteFieldValue(writer, new() { Value = ((List<Field>)Fields[((DiEventDataBase.FieldArraySize)fld).ArraySizeField].Value).Count }, new() { Type = ((DiEventDataBase.FieldArraySize)fld).SubType });
                else if(GetType() == typeof(DvElementTemplate))
                    WriteFieldValue(writer, new() { Value = ((List<Field>)((DvElementTemplate)this).ElementFields[((DiEventDataBase.FieldArraySize)fld).ArraySizeField].Value).Count }, new() { Type = ((DiEventDataBase.FieldArraySize)fld).SubType });
                break;

            case DiEventDataBase.Field.DataType.rgba8:
                writer.Write((RGBA32)field.Value);
                break;

            case DiEventDataBase.Field.DataType.rgb32:
                writer.Write((RGB32F)field.Value);
                break;

            case DiEventDataBase.Field.DataType.padding:
                writer.WriteNulls(fld.Size);
                break;

            case DiEventDataBase.Field.DataType.strct:
                DiEventDataBase.FieldStruct fS = (DiEventDataBase.FieldStruct)fld;
                Dictionary<string, Field> fldst = (Dictionary<string, Field>)field.Value;
                for(int i = 0; i < fldst.Count; i++)
                    WriteFieldValue(writer, fldst.ElementAt(i).Value, fS.StructValue.Fields[i]);
                break;

            case DiEventDataBase.Field.DataType.enm:
                DiEventDataBase.FieldEnum fE = (DiEventDataBase.FieldEnum)fld;
                Enum enm = (Enum)field.Value;
                WriteFieldValue(writer, new() { Value = enm.Value, DataType = (DataType)((byte)fE.EnumType.Type) }, new() { Type = fE.EnumType.Type });
                break;

            case DiEventDataBase.Field.DataType.rgba32:
                writer.Write((RGBA32)field.Value);
                break;

            case DiEventDataBase.Field.DataType.guid:
                writer.Write((Guid)field.Value);
                break;
        }
    }

    public enum DataType
    {
        UByte = 0, Byte, UShort, Short, UInt, Int, Float, Vector2, Vector3, Vector4, Matrix4x4, Curve, String, Enum, Struct, Guid, Array, Boolean, RGBA, RGB32, RGBA32
    }

    public struct Field
    {
        public object Value;
        public Dictionary<string, string> Descriptions;
        public DataType DataType;
    }

    public struct Enum
    {
        public int Value;
        public Dictionary<string, int> Values;
    }
}

public class DvElementTemplate : DvNodeTemplate
{
    public string ElementName = "";
    public Dictionary<string, Field> ElementFields = new();

    public void ReadElement(BinaryObjectReader reader, DiEventDataBase.Node db)
    {
        foreach (var i in db.Fields)
        {
            Tuple<string, Field> fld = ReadField(reader, i);
            if (fld != null)
                ElementFields.Add(fld.Item1, fld.Item2);
        }
    }

    public void WriteElement(BinaryObjectWriter writer, DiEventDataBase.Node db)
    {
        foreach (var i in db.Fields)
        {
            if (ElementFields.ContainsKey(i.Name))
                WriteFieldValue(writer, ElementFields[i.Name], i);
            else
                WriteFieldValue(writer, new() { DataType = (DataType)((byte)i.Type), Value = null }, i);
        }
    }
}