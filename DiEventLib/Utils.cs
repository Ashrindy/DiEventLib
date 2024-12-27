using Amicitia.IO.Binary;
using DiEventLib.IO.Template;
using System.Numerics;
using System.Text;

namespace DiEventLib;

public static class Utils
{
    public static string ReadStringTableEntry(this BinaryObjectReader reader)
    {
        long ptr = DataBaseBinaryHandler.Bit ? reader.Read<long>() : reader.Read<int>();
        if (ptr > 0)
        {
            string value = "";
            var test = ptr - StringTableHandler.StringTableOffset;
            if (DataBaseBinaryHandler.CompressedStringTable)
                StringTableHandler.StringTableReader.ReadAtOffset(ptr - StringTableHandler.StringTableOffset, () => value = StringTableHandler.StringTableReader.ReadString(StringBinaryFormat.NullTerminated));
            else
                reader.ReadAtOffset(ptr, () => value = reader.ReadString(StringBinaryFormat.NullTerminated));
            return value;
        }
        else
            return "";
    }

    public enum StringEncoding
    { 
        Default,
        ShiftJIS,
        UTF8
    }
    
    public static T[] ReadObjectArray<T>(this BinaryObjectReader reader, int count) where T : IBinarySerializable, new()
    {
        var result = new T[count];
        for (int i = 0; i < count; i++)
            result[i] = reader.ReadObject<T>();

        return result;
    }

    public static string ReadDvString(this BinaryObjectReader reader, StringEncoding stringencoding = StringEncoding.ShiftJIS, int length = 64)
    {
        var value = "";
        switch (stringencoding)
        {
            case StringEncoding.Default:
                value = reader.ReadString(Encoding.Default, StringBinaryFormat.FixedLength, length); 
                break;
            case StringEncoding.ShiftJIS:
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                value = reader.ReadString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, length); 
                break;
            case StringEncoding.UTF8:
                value = reader.ReadString(Encoding.UTF8, StringBinaryFormat.FixedLength, length);
                break;
        }
        return value;
    }

    public static void WriteDvString(this BinaryObjectWriter writer, string value, StringEncoding stringEncoding = StringEncoding.ShiftJIS, int length = 64)
    {
        switch (stringEncoding)
        {
            case StringEncoding.Default:
                writer.WriteString(Encoding.Default, StringBinaryFormat.FixedLength, value, length);
                break;
            case StringEncoding.ShiftJIS:
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                writer.WriteString(Encoding.GetEncoding("Shift-JIS"), StringBinaryFormat.FixedLength, value, length);
                break;
            case StringEncoding.UTF8:
                writer.WriteString(Encoding.UTF8, StringBinaryFormat.FixedLength, value, length); 
                break;
        }
    }

    public static void WriteObjectCollection<T>(this BinaryObjectWriter writer, IEnumerable<T> items) where T : IBinarySerializable
    {
        foreach (var item in items)
            writer.WriteObject(item);
    }

    // From BlueCube3310's SharpNeedle fork 
    public static void WriteNulls(this BinaryObjectWriter writer, int length)
    {
        Span<byte> nulls = length <= 1024 ? stackalloc byte[length] : new byte[length];
        writer.WriteArray(nulls);
    }

    

    // From KnuxLib Helpers
    public static Matrix4x4 ComposeMatrix(Vector3 translation, Vector3 scale, Quaternion rotation)
    {
        // Create the initial matrix from the rotation.
        Matrix4x4 matrix = Matrix4x4.CreateFromQuaternion(rotation);

        // Set the matrix's translation.
        matrix.Translation = translation;

        // Apply the scale values to the matrix.
        matrix.M11 *= scale.X;
        matrix.M12 *= scale.X;
        matrix.M13 *= scale.X;
        matrix.M21 *= scale.Y;
        matrix.M22 *= scale.Y;
        matrix.M23 *= scale.Y;
        matrix.M31 *= scale.Z;
        matrix.M32 *= scale.Z;
        matrix.M33 *= scale.Z;

        // Return the generated matrix.
        return matrix;
    }

    public static Vector3 ToEulerAngles(Quaternion q)
    {
        Vector3 angles = new();

        double sinr_cosp = 2 * (q.W * q.X + q.Y * q.Z);
        double cosr_cosp = 1 - 2 * (q.X * q.X + q.Y * q.Y);
        angles.X = (float)Math.Atan2(sinr_cosp, cosr_cosp);

        double sinp = 2 * (q.W * q.Y - q.Z * q.X);
        if (Math.Abs(sinp) >= 1)
        {
            angles.Y = (float)Math.CopySign(Math.PI / 2, sinp);
        }
        else
        {
            angles.Y = (float)Math.Asin(sinp);
        }

        double siny_cosp = 2 * (q.W * q.Z + q.X * q.Y);
        double cosy_cosp = 1 - 2 * (q.Y * q.Y + q.Z * q.Z);
        angles.Z = (float)Math.Atan2(siny_cosp, cosy_cosp);

        return angles;
    }

    public static Quaternion ToQuaternion(Vector3 v)
    {

        float cy = (float)Math.Cos(v.Z * 0.5);
        float sy = (float)Math.Sin(v.Z * 0.5);
        float cp = (float)Math.Cos(v.Y * 0.5);
        float sp = (float)Math.Sin(v.Y * 0.5);
        float cr = (float)Math.Cos(v.X * 0.5);
        float sr = (float)Math.Sin(v.X * 0.5);

        return new Quaternion
        {
            W = (cr * cp * cy + sr * sp * sy),
            X = (sr * cp * cy - cr * sp * sy),
            Y = (cr * sp * cy + sr * cp * sy),
            Z = (cr * cp * sy - sr * sp * cy)
        };

    }

    public static bool ToBool(uint data)
    {
        if (data == 0)
            return false;
        else if (data == 1)
            return true;
        else
            return false;
    }

    public static uint FromBool(bool data)
    {
        if (data == false)
            return 0;
        else if (data == true)
            return 1;
        else
            return 0;
    }
}

public struct RGBA8
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte A { get; set; }

    public override string ToString() => $"#{R:X2}{G:X2}{B:X2}{A:X2}";
}

public struct RGBA32
{
    public uint A { get; set; }
    public uint R { get; set; }
    public uint G { get; set; }
    public uint B { get; set; }

    public override string ToString() => $"#{R:X2}{G:X2}{B:X2}{A:X2}";
}
public struct RGB32
{
    public uint R { get; set; }
    public uint G { get; set; }
    public uint B { get; set; }

    public override string ToString() => $"#{R:X2}{G:X2}{B:X2}";
}

public struct RGB32F
{
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }

    public override string ToString() => $"#{R:X2}{G:X2}{B:X2}";
}