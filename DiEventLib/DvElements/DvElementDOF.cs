﻿using Amicitia.IO.Binary;
using System.Text;

namespace DiEventLib;

public class DvElementDOF : DvNodeObject
{
    public uint Field_60 {  get; set; }
    public float Field_64 {  get; set; }
    public float Field_68 {  get; set; }
    public float Field_6c {  get; set; }
    public float Far1 {  get; set; }
    public float Field_74 {  get; set; }
    public float Field_78 {  get; set; }
    public float Field_7c {  get; set; }
    public float Far2 {  get; set; }
    public float Field_84 {  get; set; }
    public float Field_88 {  get; set; }
    public uint Field_8c {  get; set; }
    public uint Field_90 {  get; set; }
    public float Field_94 {  get; set; }
    public float Field_98 {  get; set; }
    public float Field_9c {  get; set; }
    public float Field_a0 {  get; set; }
    public float Field_a4 {  get; set; }
    public float Field_a8 {  get; set; }
    public float Field_ac {  get; set; }
    public float[] AnimData {  get; set; }

    public DvElementDOF() { }
    public DvElementDOF(BinaryObjectReader reader)
        => Read(reader);
    public override void Read(BinaryObjectReader reader)
    {
        Field_60 = reader.Read<uint>();
        Field_64 = reader.Read<float>();
        Field_68 = reader.Read<float>();
        Field_6c = reader.Read<float>();
        Far1 = reader.Read<float>();
        Field_74 = reader.Read<float>();
        Field_78 = reader.Read<float>();
        Field_7c = reader.Read<float>();
        Far2 = reader.Read<float>();
        Field_84 = reader.Read<float>();
        Field_88 = reader.Read<float>();
        Field_8c = reader.Read<uint>();
        Field_90 = reader.Read<uint>();
        Field_94 = reader.Read<float>();
        Field_98 = reader.Read<float>();
        Field_9c = reader.Read<float>();
        Field_a0 = reader.Read<float>();
        Field_a4 = reader.Read<float>();
        Field_a8 = reader.Read<float>();
        Field_ac = reader.Read<float>();
        AnimData = reader.ReadArray<float>(32);
    }

    public override void Write(BinaryObjectWriter writer)
    {
        writer.Write(Field_60);
        writer.Write(Field_64);
        writer.Write(Field_68);
        writer.Write(Field_6c);
        writer.Write(Far1);
        writer.Write(Field_74);
        writer.Write(Field_78);
        writer.Write(Field_7c);
        writer.Write(Far2);
        writer.Write(Field_84);
        writer.Write(Field_88);
        writer.Write(Field_8c);
        writer.Write(Field_90);
        writer.Write(Field_94);
        writer.Write(Field_98);
        writer.Write(Field_9c);
        writer.Write(Field_a0);
        writer.Write(Field_a4);
        writer.Write(Field_a8);
        writer.Write(Field_ac);
        writer.WriteArray(AnimData);
    }
}