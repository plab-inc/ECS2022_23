using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Entities.Items;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.World;

public class Level
{
    public List<Room> Rooms;
    public List<Rectangle> GroundLayer;
    public List<Rectangle> WaterLayer;

    public Player Player { get; set; }
    public bool IsCompleted { get; private set; }
    public bool PlayerIsInfrontOfBossDoor{ get; set; }

    public Room StartRoom
    {
        get
        {
            return Rooms.First(room => room.MapName.Contains("start"));
        }
    }
    public Room BossRoom
    {
        get
        {
            return Rooms.First(room => room.MapName.Contains("boss"));
        } 
    }
    public Level(List<Room> rooms, List<Rectangle> groundLayer, List<Rectangle> waterLayer)
    {
        Rooms = rooms;
        GroundLayer = groundLayer;
        WaterLayer = waterLayer;
    }
    public void Update(GameTime gameTime)
    {
        foreach (var room in Rooms.Where(room => room.Rectangle.Contains(Player.Position)))
        {
            Player.Room = room;

            if (room.GetInteractablePositions("Locker").Any())
            {
                if (Player.Rectangle.Contains(room.GetInteractablePositions("Locker").First()))
                {
                    Console.WriteLine("Wow a locker");
                }
            }
            if (room.GetInteractablePositions("Bossdoor").Any())
            {
                PlayerIsInfrontOfBossDoor = false;
                
                if (Player.Rectangle.Contains(room.GetInteractablePositions("Bossdoor").First()))
                {
                    PlayerIsInfrontOfBossDoor = true;
                }
            }
            if (room.GetInteractablePositions("Chest").Any())
            {
                if (Player.Rectangle.Contains(room.GetInteractablePositions("Chest").First()))
                {
                    Console.WriteLine("Wow a chest");
                }
            }
            if (room.GetInteractablePositions("Exit").Any())
            {
                if (Player.Rectangle.Contains(room.GetInteractablePositions("Exit").First()))
                {
                    IsCompleted = true;
                }
            }

        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        DrawBackground(spriteBatch);
        
        foreach (var room in Rooms)
        {
            room.Draw(spriteBatch);
        }
    }
    private Rectangle Background {
        
        get
        {
            var background = new Rectangle();

            foreach (var room in Rooms)
            {
                background = Rectangle.Union(room.Rectangle, background);
            }
            background.Inflate(200,200);

            return background;
        }
    }
    private void DrawBackground(SpriteBatch spriteBatch)
    {
        var startroom = Rooms.First(room => room.MapName.Contains("start"));
        
        var gid =  91;
        
        var mapTileset = startroom.Map.GetTiledMapTileset(++gid);
        var tileset = startroom.Tilesets[mapTileset.firstgid];
        var tilesetFilename = Path.GetFileNameWithoutExtension(mapTileset.source);

        if (tilesetFilename == null) return;
        
        var tilesetImageName = tilesetFilename.Replace("_tileset", "_image");
        var tilesetTexture = ContentLoader.TilesetTextures[tilesetImageName];
        
        var rect = startroom.Map.GetSourceRect(mapTileset, tileset, gid);
        var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);
        
        var destination = new Rectangle(Background.X,Background.Y, Background.Width, Background.Height);
        spriteBatch.Draw(tilesetTexture, destination, source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
    }
    public void OpenBossDoor()
    {
        var bossdoorPos = BossRoom.GetInteractableMapPositions("Bossdoor").First();
        BossRoom.ChangeTile((int) bossdoorPos.X / BossRoom.Map.TileWidth,(int) bossdoorPos.Y / BossRoom.Map.TileHeight,98,"decoration");
        
        bossdoorPos = BossRoom.GetInteractablePositions("Bossdoor").First();
        var x = (int) Math.Floor(bossdoorPos.X / BossRoom.Map.TileWidth) * BossRoom.Map.TileWidth;
        var y = (int) Math.Floor(bossdoorPos.Y / BossRoom.Map.TileHeight) * BossRoom.Map.TileHeight;
        var rect = new Rectangle(x, y, 16, 16);
        
        GroundLayer.Add(rect);
    }
}