using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Xml.Serialization;
using ECS2022_23.Core.Entities;
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

public static class Serialization
{
    private static string _executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    public static void Save<T>(T obj)
    {
        
        if (_executingPath != null) Directory.SetCurrentDirectory(_executingPath);

        string savePath = _executingPath + "/Saves/";

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        
        string data = JsonSerializer.Serialize(obj);
        string filename = obj.GetType().Name + ".json";
        
        File.WriteAllText(savePath + filename, data);
        
    }
    
    public static T Load<T>(string filename)
    {
        if (_executingPath != null) Directory.SetCurrentDirectory(_executingPath);

        string loadPath = _executingPath + "/Saves/";

        if (!File.Exists(loadPath + filename))
        {
            return default;
        }

        string jsonString = File.ReadAllText(loadPath + filename);

        return JsonSerializer.Deserialize<T>(jsonString);

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
        /*
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
        */
        return DeathCause.None;
    }
}