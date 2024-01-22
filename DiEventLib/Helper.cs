using DiEventLib.Nodes.NodeTypes;
using HedgeLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DiEventLib
{
    public class Helper
    {
        public static Matrix4x4 ReadMatrix(ExtendedBinaryReader reader)
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.M11 = reader.ReadSingle();
            matrix.M12 = reader.ReadSingle();
            matrix.M13 = reader.ReadSingle();
            matrix.M14 = reader.ReadSingle();
            matrix.M21 = reader.ReadSingle();
            matrix.M22 = reader.ReadSingle();
            matrix.M23 = reader.ReadSingle();
            matrix.M24 = reader.ReadSingle();
            matrix.M31 = reader.ReadSingle();
            matrix.M32 = reader.ReadSingle();
            matrix.M33 = reader.ReadSingle();
            matrix.M34 = reader.ReadSingle();
            matrix.M41 = reader.ReadSingle();
            matrix.M42 = reader.ReadSingle();
            matrix.M43 = reader.ReadSingle();
            matrix.M44 = reader.ReadSingle();
            return matrix;
        }

        public static void WriteMatrix(ExtendedBinaryWriter writer, Matrix4x4 matrix)
        {
            writer.Write(matrix.M11);
            writer.Write(matrix.M12);
            writer.Write(matrix.M13);
            writer.Write(matrix.M14);
            writer.Write(matrix.M21);
            writer.Write(matrix.M22);
            writer.Write(matrix.M23);
            writer.Write(matrix.M24);
            writer.Write(matrix.M31);
            writer.Write(matrix.M32);
            writer.Write(matrix.M33);
            writer.Write(matrix.M34);
            writer.Write(matrix.M41);
            writer.Write(matrix.M42);
            writer.Write(matrix.M43);
            writer.Write(matrix.M44);
        }

        public static string ReadDVString(ExtendedBinaryReader reader)
        {
            byte[] nameBytes = new byte[64];

            for (int x = 0; x < 64; x++)
            {
                nameBytes[x] = reader.ReadByte();
            }

            return Encoding.Unicode.GetString(nameBytes);
        }

        public static Guid ReadGUID(ExtendedBinaryReader reader)
        {
            byte[] guidBytes = new byte[16];

            for (int x = 0; x < 16; x++)
            {
                guidBytes[x] = reader.ReadByte();
            }
            return new Guid(guidBytes);
        }

        public static void WriteGUID(ExtendedBinaryWriter Writer, Guid guid)
        {
            int index = 0;
            foreach (var i in guid.ToByteArray())
            {
                Writer.Write(i);
                index++;
            }
        }

        public static void WriteDvString(ExtendedBinaryWriter Writer, string String)
        {
            foreach (var i in Encoding.Unicode.GetBytes(String))
            {
                Writer.Write(i);
            }
        }
    }
}
