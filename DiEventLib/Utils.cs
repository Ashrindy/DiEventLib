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
    }

