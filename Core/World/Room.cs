using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ECS2022_23.Core.Entities;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledCS;

namespace ECS2022_23.Core.World;

public class Room
{
    private readonly Point _renderPos;
    public TiledMap Map;
    public string MapName;

    #region Properties

        public Dictionary<int, TiledTileset> Tilesets
        {
            get
            {
                var tilesets = new Dictionary<int, TiledTileset>();

                foreach (var mapTileset in Map.Tilesets)
                {
                    if (mapTileset.source == null) continue;
                    var filename = Path.GetFileNameWithoutExtension(mapTileset.source);
                    if (filename != null)
                        tilesets.Add(mapTileset.firstgid, ContentLoader.Tilesets[filename]);
                }

                return tilesets;
            }
        }
        public int Height => Map.Height * Map.TileHeight;
        public int Width => Map.Width * Map.TileWidth;
        public Point Position => _renderPos;
        public Rectangle Rectangle => new(_renderPos.X, _renderPos.Y, Width, Height);
        public List<Rectangle> GroundLayer
        {
            get
            {
                var groundLayer = Map.Layers.First(l => l.name == "Ground");
                var collisionLayer = new List<Rectangle>();

                foreach (var obj in groundLayer.objects)
                {
                    var x = (int) obj.x + _renderPos.X;
                    var y = (int) obj.y + _renderPos.Y;

                    collisionLayer.Add(new Rectangle(x, y, (int) obj.width, (int) obj.height));
                }

                return collisionLayer;
            }
        }
        public List<Door> Doors
        {
            get
            {
                var doorsObjects = Map.Layers.First(x => x.name == "Doors").objects;
                var doors = new List<Door>();

                foreach (var door in doorsObjects)
                {
                    var doorX = (int) (Math.Floor(door.x / Map.TileWidth) * Map.TileWidth) + _renderPos.X;
                    var doorY = (int) (Math.Floor(door.y / Map.TileHeight) * Map.TileHeight) + _renderPos.Y;

                    var doorDirection = Enum.Parse<Direction>(door.name);

                    doors.Add(new Door(this, doorDirection, doorX, doorY));
                }

                return doors;
            }
        }
        public List<Vector2> Spawns
        {
            get
            {
                var spawnObjects = Map.Layers.First(x => x.name == "Spawn").objects;
                var spawns = new List<Vector2>();

                foreach (var spawnObject in spawnObjects)
                {
                    var spawnX = spawnObject.x + _renderPos.X;
                    var spawnY = spawnObject.y + _renderPos.Y;

                    spawns.Add(new Vector2(spawnX, spawnY));
                }

                return spawns;
            }
        }

    #endregion
    public Room(string mapName, Point renderPos)
    {
        _renderPos = renderPos;

        MapName = mapName;
        Map = Helper.DeepCopy.Create(ContentLoader.Tilemaps[mapName]);
    }
    
    public Vector2 GetRandomSpawnPos(Entity entity)
    {
        var random = new Random((int) DateTime.Now.Ticks);
        var index = random.Next(Spawns.Count);

        var spawnPos = new Vector2(Spawns[index].X - entity.Rectangle.Center.X,
            Spawns[index].Y - entity.Rectangle.Center.X);
        var spawnRect = new Rectangle((int) spawnPos.X, (int) spawnPos.Y, entity.Rectangle.Width,
            entity.Rectangle.Height);

        foreach (var rectangle in GroundLayer)
        {
            if (rectangle.Intersects(spawnRect))
            {
                return spawnPos;
            }
        }

        throw new InvalidOperationException("Spawn of entity failed in Map: " + MapName + "at position: " + spawnPos);
    }

    public void ChangeTile(int x, int y, int newGid, string layerName)
    {
        var layer = Map.Layers.First(layer => layer.name == layerName);
        var index = (y * layer.width) + x;
        layer.data[index] = newGid + 1;
    }

    public int GetTileGid(int x, int y, string layerName)
    {
        var layer = Map.Layers.First(layer => layer.name == layerName);
        var index = (y * layer.width) + x;

        return layer.data[index];
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var tileLayers = Map.Layers.Where(x => x.type == TiledLayerType.TileLayer);

        foreach (var layer in tileLayers)
        {
            for (var y = 0; y < layer.height; y++)
            {
                for (var x = 0; x < layer.width; x++)
                {
                    var index = (y * layer.width) +
                                x; // Assuming the default render order is used which is from right to bottom
                    var gid = layer.data[index];

                    var tileX = (x * Map.TileWidth + _renderPos.X);
                    var tileY = (y * Map.TileHeight + _renderPos.Y);

                    var effects = SpriteEffects.None;
                    float rotation = 0f;

                    if (gid == 0)
                    {
                        continue;
                    }

                    var mapTileset = Map.GetTiledMapTileset(gid);
                    var tileset = Tilesets[mapTileset.firstgid];
                    var tilesetFilename = Path.GetFileNameWithoutExtension(mapTileset.source);

                    var tilesetImageName = tilesetFilename.Replace("_tileset", "_image");
                    var tilesetTexture = ContentLoader.TilesetTextures[tilesetImageName];

                    var rect = Map.GetSourceRect(mapTileset, tileset, gid);
                    var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);

                    var destination = new Rectangle(tileX, tileY, Map.TileWidth, Map.TileHeight);

                    Trans tileTrans = Trans.None;
                    if (Map.IsTileFlippedHorizontal(layer, x, y)) tileTrans |= Trans.Flip_H;
                    if (Map.IsTileFlippedVertical(layer, x, y)) tileTrans |= Trans.Flip_V;
                    if (Map.IsTileFlippedDiagonal(layer, x, y)) tileTrans |= Trans.Flip_D;

                    switch (tileTrans)
                    {
                        case Trans.Flip_H:
                            effects = SpriteEffects.FlipHorizontally;
                            break;
                        case Trans.Flip_V:
                            effects = SpriteEffects.FlipVertically;
                            break;

                        case Trans.Rotate_90:
                            rotation = (float) Math.PI * .5f;
                            destination.X += Map.TileWidth;
                            break;

                        case Trans.Rotate_180:
                            rotation = (float) Math.PI;
                            destination.X += Map.TileWidth;
                            destination.Y += Map.TileHeight;
                            break;

                        case Trans.Rotate_270:
                            rotation = (float) Math.PI * 3 / 2;
                            destination.Y += Map.TileHeight;
                            break;

                        case Trans.Rotate_90AndFlip_H:
                            effects = SpriteEffects.FlipHorizontally;
                            rotation = (float) Math.PI * .5f;
                            destination.X += Map.TileWidth;
                            break;
                    }

                    spriteBatch.Draw(tilesetTexture, destination, source, Color.White, rotation, Vector2.Zero, effects,
                        0);
                }
            }
        }
    }
}

[Flags]
internal enum Trans
{
    None = 0,
    Flip_H = 1 << 0,
    Flip_V = 1 << 1,
    Flip_D = 1 << 2,

    Rotate_90 = Flip_D | Flip_H,
    Rotate_180 = Flip_H | Flip_V,
    Rotate_270 = Flip_V | Flip_D,

    Rotate_90AndFlip_H = Flip_H | Flip_V | Flip_D,
}