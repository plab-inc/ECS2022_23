using System.IO;
using System.Reflection.Metadata;
using System.Xml.Serialization;
using ECS2022_23.Core.Entities;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Characters.Enemy;
using ECS2022_23.Core.Entities.Characters.Enemy.EnemyTypes;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Blob = ECS2022_23.Core.Entities.Characters.Enemy.EnemyTypes.Blob;

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
    public static DeathCause EntityToDeathCause(Entity entity)
    {
        switch (entity)
        {
            case Chaser:
                return DeathCause.Chaser;
            case Blob:
                return DeathCause.Blob;
            case GiantBlob:
                return DeathCause.GiantBlob;
            case Gunner:
                return DeathCause.Gunner;
            case Turret:
                return DeathCause.Turret;
            case ProjectileShot:
                return DeathCause.ProjectileShot;
        }
        return DeathCause.None;
    }
}