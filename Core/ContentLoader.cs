using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TiledCS;

namespace ECS2022_23.Core;

public static class ContentLoader
{
    private static ContentManager _content;
    
    public static Dictionary<string, TiledMap> Tilemaps =  new();
    public static Dictionary<string, TiledTileset> Tilesets =  new();
    public static Dictionary<string, Texture2D> TilesetTextures = new ();
    public static Texture2D EnemyTexture;
    public static void Load(ContentManager content)
    {
        if (!Directory.Exists("Content")) throw new DirectoryNotFoundException();
        
        _content = content;

        LoadTilemaps();
        LoadTilesets();
        LoadSprites();
    }

    private static void LoadTilemaps()
    {
        LoadRooms();
    }
    private static void LoadTilesets()
    {
        var tilesets = Directory.GetFiles("Content/tilesets","tileset???_*",SearchOption.AllDirectories);
        
        
        foreach (var tileset in tilesets)
        {
            var fileName = Path.GetFileNameWithoutExtension(tileset);

            if (fileName.Contains("_tileset_"))
            {
                Tilesets.Add(fileName,_content.Load<TiledTileset>("tilesets/tilesets/" + fileName));
            }
            if (fileName.Contains("_image_"))
            {
                TilesetTextures.Add(fileName,_content.Load<Texture2D>("tilesets/images/" + fileName));
            }
            
            //TODO: Make sure tileset picture is there
        }
    }
    private static void LoadRooms()
    {
        var rooms = Directory.GetFiles("Content/rooms","room*.xnb");
        var starts = Directory.GetFiles("Content/rooms","start*.xnb");

        foreach (var room in rooms)
        {
            
            var fileName = Path.GetFileNameWithoutExtension(room);
            Tilemaps.Add(fileName, _content.Load<TiledMap>("rooms/" + Path.GetFileNameWithoutExtension(fileName)));
            
        }

        foreach (var start in starts)
        {
            var fileName = Path.GetFileNameWithoutExtension(start);
            Tilemaps.Add(fileName, _content.Load<TiledMap>("rooms/" + Path.GetFileNameWithoutExtension(fileName)));
        }
        
    }

    private static void LoadSprites()
    {
        EnemyTexture = _content.Load<Texture2D>("sprites/astro");
    }

}