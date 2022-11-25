using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLevelGenerator.Core;

namespace ECS2022_23.Core;
public class Level
{
    public List<Room> Rooms;
    public List<Rectangle> GroundLayer;
    public Rectangle Background {
        
        get
        {
            Rectangle background = new Rectangle();

            foreach (var rectangle in GroundLayer)
            {
                background = Rectangle.Union(rectangle, background);
            }
            background.Inflate(200,200);

            return background;

        }
    }
    
    public Level(List<Room> rooms, List<Rectangle> groundLayer)
    {
        Rooms = rooms;
        GroundLayer = groundLayer;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        
        DrawBackground(spriteBatch);
        
        foreach (var room in Rooms)
        {
            room.Draw(spriteBatch);
        }
        
    }

    private void DrawBackground(SpriteBatch spriteBatch)
    {

        //Slows down game insane
        
        var startroom = Rooms.First(room => room.MapName.Contains("start"));
        
        var gid = 92;
        
        var mapTileset = startroom.Map.GetTiledMapTileset(gid);
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