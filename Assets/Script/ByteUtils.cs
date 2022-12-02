using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ByteUtils
{
    public static byte[] Serializable(object obj)
    {
        if (obj == null)
        {
            return null;
        }
        MemoryStream mStream = new MemoryStream();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(mStream, obj);
        mStream.Position = 0;
        byte[] bytes = new byte[mStream.Length];
        mStream.Read(bytes, 0, bytes.Length);
        mStream.Close();
        return bytes;
    }


    public static object DeSerializable(byte[] bytes)
    {
        object obj = null;
        MemoryStream mStream = new MemoryStream(bytes);
        mStream.Position = 0;
        BinaryFormatter formatter = new BinaryFormatter();
        obj = formatter.Deserialize(mStream);
        mStream.Close();
        return obj;
    }
}
