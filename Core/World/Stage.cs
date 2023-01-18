using System.Collections.Generic;
using System.IO;
using System.Linq;
using ECS2022_23.Core.Entities.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.World;

public class Stage
{
    public List<Rectangle> GroundLayer;
    public List<Room> Rooms;
    public List<Rectangle> WaterLayer;

    public Stage(List<Room> rooms, List<Rectangle> groundLayer, List<Rectangle> waterLayer)
    {
        Rooms = rooms;
        GroundLayer = groundLayer;
        WaterLayer = waterLayer;
    }

    public Player Player { get; set; }
    public bool IsCompleted { get; private set; }
    public bool PlayerIsInfrontOfBossDoor { get; set; }
    public bool PlayerIsInfrontOfLocker { get; set; }

    public Room StartRoom
    {
        get { return Rooms.First(room => room.MapName.Contains("start")); }
    }

    public Room BossRoom
    {
        get { return Rooms.First(room => room.MapName.Contains("boss")); }
    }

    private Rectangle Background
    {
        get
        {
            var background = new Rectangle();

            foreach (var room in Rooms) background = Rectangle.Union(room.Rectangle, background);
            background.Inflate(200, 200);

            return background;
        }
    }

    public void Update(GameTime gameTime)
    {
        foreach (var room in Rooms.Where(room => room.Rectangle.Contains(Player.Position)))
        {
            Player.Room = room;
            var lockers = room.GetRectanglesRelativeToWorld("Interactables", "Locker");
            var exits = room.GetRectanglesRelativeToWorld("Interactables", "Exit");
            var bossDoors = room.GetRectanglesRelativeToWorld("Interactables", "Bossdoor");

            if (lockers.Any())
            {
                PlayerIsInfrontOfLocker = false;

                if (Player.Rectangle.Intersects(lockers.First())) PlayerIsInfrontOfLocker = true;
            }

            if (bossDoors.Any())
            {
                PlayerIsInfrontOfBossDoor = false;

                if (Player.Rectangle.Intersects(bossDoors.First())) PlayerIsInfrontOfBossDoor = true;
            }

            if (exits.Any())
                if (exits.First().Contains(Player.Rectangle.Center))
                    IsCompleted = true;
            break;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        DrawBackground(spriteBatch);

        foreach (var room in Rooms)
        {
            room.Draw(spriteBatch);

            var whiteRectangle = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] {Color.White});

            if (room.GetRectanglesRelativeToWorld("Interactables").Any())
                foreach (var rectangle in room.GetRectanglesRelativeToWorld("Interactables"))
                {
                    //spriteBatch.Draw(whiteRectangle,rectangle,Color.White);
                }
        }
    }

    private void DrawBackground(SpriteBatch spriteBatch)
    {
        var startroom = Rooms.First(room => room.MapName.Contains("start"));

        var gid = 91;

        var mapTileset = startroom.Map.GetTiledMapTileset(++gid);
        var tileset = startroom.Tilesets[mapTileset.firstgid];
        var tilesetFilename = Path.GetFileNameWithoutExtension(mapTileset.source);

        if (tilesetFilename == null) return;

        var tilesetImageName = tilesetFilename.Replace("_tileset", "_image");
        var tilesetTexture = WorldLoader.TilesetTextures[tilesetImageName];

        var rect = startroom.Map.GetSourceRect(mapTileset, tileset, gid);
        var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);

        var destination = new Rectangle(Background.X, Background.Y, Background.Width, Background.Height);
        spriteBatch.Draw(tilesetTexture, destination, source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
    }

    public void OpenBossDoor()
    {
        var bossDoor = BossRoom.GetAllRectanglesFromLayer("Interactables", "Bossdoor").First();
        BossRoom.ChangeTile(bossDoor.X / BossRoom.Map.TileWidth, bossDoor.Y / BossRoom.Map.TileHeight, 98,
            "decoration");

        GroundLayer.Add(BossRoom.GetRectanglesRelativeToWorld("Interactables", "Bossdoor").First());
    }
}