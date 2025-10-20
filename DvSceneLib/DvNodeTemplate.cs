using Amicitia.IO.Binary;
using DvSceneLib.IO.Template;
using System.Numerics;

namespace DvSceneLib;

public class DvNodeTemplate : DvNode
{
    public string Category = "";
    public Dictionary<string, Field> Fields = new();
    Dictionary<string, Field> arraycounts = new();
    bool AutoAlign = true;

    public DvNodeTemplate() { }

    public DvNodeTemplate(DiEventDataBase.Node node) => Create(node);

    public static object GetDefaultByType(DataType type)
    {
        switch (type)
        {
            case DataType.UByte or DataType.Byte:
                return (byte)0;

            case DataType.UShort:
                return (ushort)0;

            case DataType.Short:
                return (short)0;

            case DataType.UInt:
                return 0u;

            case DataType.Int:
                return 0;

            case DataType.Float:
                return 0.0f;

            case DataType.Vector2:
                return new Vector2(0, 0);

            case DataType.Vector3:
                return new Vector3(0, 0, 0);

            case DataType.Vector4:
                return new Vector4(0, 0, 0, 0);

            case DataType.Matrix4x4:
                return Matrix4x4.Identity;

            case DataType.Boolean:
                return false;

            case DataType.Guid:
                return Guid.NewGuid();

            case DataType.RGB32:
                return new RGB32();

            case DataType.RGB32F:
                return new RGB32F();

            case DataType.RGBA32:
                return new RGBA32() { A = 255 };

            case DataType.RGBA:
                return new RGBA8() { A = 255 };

            default:
                throw new Exception("Unimplemented Array SubType");
        }
    }

    public Field? CreateFieldValue(DiEventDataBase.Field fld)
    {
        Field? field = null;
        switch (fld.Type)
        {
            default:
                field = new() { Value = GetDefaultByType((DataType)((byte)fld.Type)), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.padding:
                break;

            case DiEventDataBase.Field.DataType.str:
                field = new() { Value = new String() { Value = "", Length = fld.Size }, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.curve:
                field = new() { Value = Enumerable.Range(0, fld.Size).Select(i => (float)i * (1.0f/fld.Size)).ToArray(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.array:
                if (fld.GetType() == typeof(DiEventDataBase.FieldDynamicArray))
                {
                    DiEventDataBase.FieldDynamicArray fDA = (DiEventDataBase.FieldDynamicArray)fld;
                    List<Field> flds = new();
                    for (int i = 0; i < Convert.ToInt32(arraycounts[fDA.ArraySizeField].Value); i++)
                        flds.Add((Field)CreateFieldValue(new() { Type = fDA.SubType, Size = fDA.Size }));
                    field = new() { Value = flds, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type), SubDataType = (DataType)((byte)fDA.SubType) };
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
                            Tuple<string, Field> fd = CreateField(x);
                            fldstf.Add(fd.Item1, fd.Item2);
                        }
                        flds.Add(new Field() { Value = fldstf, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fDA.SubType) });
                    }
                    field = new() { Value = flds, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type), SubDataType = (DataType)((byte)fDA.SubType) };
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
                            Tuple<string, Field> fd = CreateField(x);
                            fldstf.Add(fd.Item1, fd.Item2);
                        }
                        flds[i] = new Field() { Value = fldstf, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fA.SubType) };
                    }
                    field = new() { Value = flds, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type), SubDataType = (DataType)((byte)fA.SubType) };
                }
                else
                {
                    DiEventDataBase.FieldArray fA = (DiEventDataBase.FieldArray)fld;
                    Field[] flds = new Field[fA.Size];
                    for (int i = 0; i < fld.Size; i++)
                        flds[i] = (Field)(CreateFieldValue(new() { Type = fA.SubType, Size = fA.Size }));

                    field = new() { Value = flds, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                }
                break;

            case DiEventDataBase.Field.DataType.arraysize:
                field = new() { Value = GetDefaultByType((DataType)((byte)((DiEventDataBase.FieldArraySize)fld).SubType)), Descriptions = fld.Descriptions, DataType = (DataType)((byte)((DiEventDataBase.FieldArraySize)fld).SubType) };
                break;

            case DiEventDataBase.Field.DataType.strct:
                DiEventDataBase.FieldStruct fS = (DiEventDataBase.FieldStruct)fld;
                Dictionary<string, Field> fldst = new();
                foreach (var i in fS.StructValue.Fields)
                {
                    Tuple<string, Field> fd = CreateField(i);
                    fldst.Add(fd.Item1, fd.Item2);
                }
                field = new() { Value = fldst, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.enm:
                DiEventDataBase.FieldEnum fE = (DiEventDataBase.FieldEnum)fld;
                Enum enm = new();
                enm.Value = 0;
                enm.Values = fE.EnumType.Values;
                field = new() { Value = enm, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.flags:
                DiEventDataBase.FieldFlag fF = (DiEventDataBase.FieldFlag)fld;
                Flag fl = new();
                fl.Value = 0;
                fl.Values = fF.FlagType.Values;
                field = new() { Value = fl, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;
        }
        return field;
    }

    public Tuple<string, Field> CreateField(DiEventDataBase.Field field)
    {
        Field? fld = CreateFieldValue(field);
        if (fld == null)
            return null;
        return new(field.Name, (Field)fld);
    }

    public void Create(DiEventDataBase.Node node)
    {
        NodeName = "New Node";
        Guid = Guid.NewGuid();
        Category = node.FullName;
        foreach (var i in node.Fields)
        {
            Tuple<string, Field> fld = CreateField(i);
            if (fld != null)
            {
                if (i.Type == DiEventDataBase.Field.DataType.arraysize)
                    arraycounts.Add(fld.Item1, fld.Item2);
                else
                    Fields.Add(fld.Item1, fld.Item2);
            }
        }
    }

    public override string ToString() => $"{NodeName} - {Category}";

    public void Read(BinaryObjectReader reader, DiEventDataBase.Node db, bool autoAlign)
    {
        AutoAlign = autoAlign;
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

    protected int GetBasicAlign(DiEventDataBase.Field.DataType type)
    {
        switch (type)
        {
            case DiEventDataBase.Field.DataType.u8 
            or DiEventDataBase.Field.DataType.s8 
            or DiEventDataBase.Field.DataType.boolean:
                return 1;
                break;

            case DiEventDataBase.Field.DataType.u16 
            or DiEventDataBase.Field.DataType.s16:
                return 2;
                break;

            case DiEventDataBase.Field.DataType.u32 
            or DiEventDataBase.Field.DataType.s32 
            or DiEventDataBase.Field.DataType.f32 
            or DiEventDataBase.Field.DataType.vec2 
            or DiEventDataBase.Field.DataType.vec3 
            or DiEventDataBase.Field.DataType.vec4 
            or DiEventDataBase.Field.DataType.mat4x4 
            or DiEventDataBase.Field.DataType.curve
            or DiEventDataBase.Field.DataType.guid
            or DiEventDataBase.Field.DataType.rgb32
            or DiEventDataBase.Field.DataType.rgba32:
                return 4;
                break;

            case DiEventDataBase.Field.DataType.str:
                return 4;
                break;
        }
        return 1;
    }

    protected int GetAlign(DiEventDataBase.Field fld)
    {
        switch (fld.Type)
        {
            case DiEventDataBase.Field.DataType.enm:
                return GetBasicAlign(((DiEventDataBase.FieldEnum)fld).EnumType.Type);
                break;

            case DiEventDataBase.Field.DataType.flags:
                return GetBasicAlign(((DiEventDataBase.FieldFlag)fld).FlagType.Type);
                break;

            case DiEventDataBase.Field.DataType.strct:
                var fStr = (DiEventDataBase.FieldStruct)fld;
                int biggestAlign = 1;
                foreach(var i in fStr.StructValue.Fields)
                {
                    int align = GetAlign(i);
                    if (align > biggestAlign)
                        biggestAlign = align;
                }
                return biggestAlign;
                break;

            case DiEventDataBase.Field.DataType.arraysize:
                return 4;
                break;
        }
        return GetBasicAlign(fld.Type);
    }

    protected Field? ReadFieldValue(BinaryObjectReader reader, DiEventDataBase.Field fld)
    {
        Field? field = null;
        if (AutoAlign)
            reader.Align(GetAlign(fld));
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
                field = new() { Value = new String() { Value = reader.ReadString(StringBinaryFormat.FixedLength, fld.Size), Length = fld.Size }, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
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
                    field = new() { Value = flds, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type), SubDataType = (DataType)((byte)fDA.SubType) };
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
                        flds.Add(new Field() { Value = fldstf, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fDA.SubType) });
                    }
                    field = new() { Value = flds, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type), SubDataType = (DataType)((byte)fDA.SubType) };
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
                        flds[i] = new Field() { Value = fldstf, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fA.SubType) };
                    }
                    field = new() { Value = flds, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type), SubDataType = (DataType)((byte)fA.SubType) };
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
                field = new() { Value = reader.Read<RGBA8>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.rgb32:
                field = new() { Value = reader.Read<RGB32>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
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

            case DiEventDataBase.Field.DataType.flags:
                DiEventDataBase.FieldFlag fF = (DiEventDataBase.FieldFlag)fld;
                Flag fl = new();
                fl.Value = Convert.ToInt32(ReadFieldValue(reader, new() { Type = fF.FlagType.Type }).Value.Value);
                fl.Values = fF.FlagType.Values;
                field = new() { Value = fl, Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.rgba32:
                field = new() { Value = reader.Read<RGBA32>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.rgb32f:
                field = new() { Value = reader.Read<RGB32F>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;

            case DiEventDataBase.Field.DataType.guid:
                field = new() { Value = reader.Read<Guid>(), Descriptions = fld.Descriptions, DataType = (DataType)((byte)fld.Type) };
                break;
        }
        if (AutoAlign)
            reader.Align(GetAlign(fld));
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
        if (AutoAlign)
            writer.Align(GetAlign(fld));
        switch (fld.Type)
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
                writer.WriteString(StringBinaryFormat.FixedLength, ((String)field.Value).Value, fld.Size);
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
                writer.Write((RGBA8)field.Value);
                break;

            case DiEventDataBase.Field.DataType.rgb32:
                writer.Write((RGB32)field.Value);
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

            case DiEventDataBase.Field.DataType.flags:
                DiEventDataBase.FieldFlag fF = (DiEventDataBase.FieldFlag)fld;
                Flag fl = (Flag)field.Value;
                WriteFieldValue(writer, new() { Value = fl.Value, DataType = (DataType)((byte)fF.FlagType.Type) }, new() { Type = fF.FlagType.Type });
                break;

            case DiEventDataBase.Field.DataType.rgba32:
                writer.Write((RGBA32)field.Value);
                break;

            case DiEventDataBase.Field.DataType.rgb32f:
                writer.Write((RGB32F)field.Value);
                break;

            case DiEventDataBase.Field.DataType.guid:
                writer.Write((Guid)field.Value);
                break;
        }
        if (AutoAlign)
            writer.Align(GetAlign(fld));
    }

    public enum DataType
    {
        UByte = 0, Byte, UShort, Short, UInt, Int, Float, Vector2, Vector3, Vector4, Matrix4x4, Curve, String, Enum, Struct, Guid, Array, Boolean = 18, RGBA, RGB32, RGBA32, RGB32F, Flag
    }

    public struct Field
    {
        public object Value;
        public Dictionary<string, string> Descriptions;
        public DataType DataType;
        public DataType SubDataType;
    }

    public struct String
    {
        public string Value;
        public long Length;
    }

    public struct Enum
    {
        public int Value;
        public Dictionary<string, int> Values;
    }

    public struct Flag
    {
        public int Value;
        public List<string> Values;
    }
}

public class DvElementTemplate : DvNodeTemplate
{
    public string ElementName = "";
    public Dictionary<string, Field> ElementFields = new();

    public DvElementTemplate() : base() { }

    public DvElementTemplate(DiEventDataBase.Node node, DiEventDataBase.Node elem) : base(node) => CreateElement(elem);

    public void CreateElement(DiEventDataBase.Node elem)
    {
        var field = Fields["Element ID"];
        field.Value = elem.NodeCategory;
        Fields["Element ID"] = field;
        ElementName = elem.FullName;
        foreach (var i in elem.Fields)
        {
            Tuple<string, Field> fld = CreateField(i);
            if (fld != null)
                ElementFields.Add(fld.Item1, fld.Item2);
        }
    }

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