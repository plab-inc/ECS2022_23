using System.IO;
using System.Xml.Serialization;

namespace ECS2022_23.Helper;
public static class Helper
{
    public static T CreateDeepCopy<T>(T obj)
    {
        using var ms = new MemoryStream();
        XmlSerializer serializer = new XmlSerializer(obj.GetType());
        serializer.Serialize(ms, obj);
        ms.Seek(0, SeekOrigin.Begin);
        return (T)serializer.Deserialize(ms);
    }
}