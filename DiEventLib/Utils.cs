using Amicitia.IO.Binary;
using System;
using System.Numerics;

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

    public static float ConvertDegToRad(float degrees)
    {
        return ((float)Math.PI / (float)180) * degrees;
    }

    public static Matrix4x4 GetTranslationMatrix(Vector3 position)
    {
        return new Matrix4x4(1, 0, 0, 0,
                             0, 1, 0, 0,
                             0, 0, 1, 0,
                             position.X, position.Y, position.Z, 1);
    }

    public static Matrix4x4 GetRotationMatrix(Vector3 anglesDeg)
    {
        anglesDeg = new Vector3(ConvertDegToRad(anglesDeg[0]), ConvertDegToRad(anglesDeg[1]), ConvertDegToRad(anglesDeg[2]));

        Matrix4x4 rotationX = new Matrix4x4(1, 0, 0, 0,
                                            0, (float)Math.Cos(anglesDeg[0]), (float)Math.Sin(anglesDeg[0]), 0,
                                            0, (float)-Math.Sin(anglesDeg[0]), (float)Math.Cos(anglesDeg[0]), 0,
                                            0, 0, 0, 1);

        Matrix4x4 rotationY = new Matrix4x4((float)Math.Cos(anglesDeg[1]), 0, (float)-Math.Sin(anglesDeg[1]), 0,
                                            0, 1, 0, 0,
                                            (float)Math.Sin(anglesDeg[1]), 0, (float)Math.Cos(anglesDeg[1]), 0,
                                            0, 0, 0, 1);
        Matrix4x4 rotationZ = new Matrix4x4((float)Math.Cos(anglesDeg[2]), (float)Math.Sin(anglesDeg[2]), 0, 0,
                                            (float)-Math.Sin(anglesDeg[2]), (float)Math.Cos(anglesDeg[2]), 0, 0,
                                            0, 0, 1, 0,
                                            0, 0, 0, 1);
        return rotationX * rotationY * rotationZ;
    }
    public static Matrix4x4 GetScaleMatrix(Vector3 scale)
    {
        return new Matrix4x4(scale.X, 0, 0, 0,
                             0, scale.Y, 0, 0,
                             0, 0, scale.Z, 0,
                             0, 0, 0, 1);
    }

    public static Matrix4x4 ComposeMatrix(Vector3 position, Vector3 rotationAngles, Vector3 scale)
    {
        return GetTranslationMatrix(position) * GetRotationMatrix(rotationAngles) * GetScaleMatrix(scale);
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
}

public struct RGBA32
{
    public uint A;
    public uint R;
    public uint G;
    public uint B;
}

