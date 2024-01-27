using Amicitia.IO.Binary;

namespace DiEventLib;

public static class Utils
{
    public static T[] ReadObjectArray<T>(this BinaryObjectReader reader, int count) where T : IBinarySerializable, new()
    {
        var result = new T[count];
        for (int i = 0; i < count; i++)
            result[i] = reader.ReadObject<T>();

        return result;
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
}

public struct RGBA32
{
    public uint A;
    public uint R;
    public uint G;
    public uint B;
}

