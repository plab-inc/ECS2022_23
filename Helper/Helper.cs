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
    public static Direction vector2ToDirection(Vector2 vector2)
    {

        if (vector2.X > 0 && vector2.Y == 0)
        {
            return Direction.Right;
        }
        if (vector2.X < 0 && vector2.Y == 0)
        {
            return Direction.Left;
        }
        if (vector2.X == 0 && vector2.Y > 0)
        {
            return Direction.Down;
        }
        if (vector2.X == 0 && vector2.Y < 0)
        {
            return Direction.Up;
        }

        return Direction.None;
    }
}