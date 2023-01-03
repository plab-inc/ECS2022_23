using System.IO;
using System.Xml.Serialization;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;

namespace ECS2022_23.Helper;
public static class DeepCopy
{
    public static T Create<T>(T obj)
    {
        using var ms = new MemoryStream();
        XmlSerializer serializer = new XmlSerializer(obj.GetType());
        serializer.Serialize(ms, obj);
        ms.Seek(0, SeekOrigin.Begin);
        return (T)serializer.Deserialize(ms);
    }
}

public static class Transform
{
    public static Direction Vector2ToDirection(Vector2 vector2)
    {
        var (x, y) = vector2;
        
        if (x > 0 && y == 0)
        {
            return Direction.Right;
        }
        if (x < 0 && y == 0)
        {
            return Direction.Left;
        }
        if (x == 0 && y > 0)
        {
            return Direction.Down;
        }
        if (x == 0 && y < 0)
        {
            return Direction.Up;
        }

        return Direction.None;
    }
}