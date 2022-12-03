using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ECS2022_23.Core.Entities.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.World;

public class Level
{
    public List<Room> Rooms;
    public List<Rectangle> GroundLayer;
    public Player Player { get; set; }

    public Room StartRoom
    {
        get
        {
            return Rooms.First(room => room.MapName.Contains("start"));
        }
    }
    public Level(List<Room> rooms, List<Rectangle> groundLayer)
    {
        Rooms = rooms;
        GroundLayer = groundLayer;
    }
    public void Update(GameTime gameTime)
    {
        foreach (var room in Rooms.Where(room => room.Rectangle.Contains(Player.Position)))
        {
            Player.Room = room;
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
        var tilesetImageName = tilesetFilename.Replace("_tileset", "_image");
        var tilesetTexture = ContentLoader.TilesetTextures[tilesetImageName];
        
        var rect = startroom.Map.GetSourceRect(mapTileset, tileset, gid);
        var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);
        
        var destination = new Rectangle(Background.X,Background.Y, Background.Width, Background.Height);
        spriteBatch.Draw(tilesetTexture, destination, source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
        
    }
}