using System.IO;
using System.Text;
using Microsoft.Xna.Framework.Content;
using TiledCS;

namespace TiledPipelineReader;

public class TilemapReader : ContentTypeReader<TiledMap>
{
    protected override TiledMap Read(ContentReader input, TiledMap existingInstance)
    {
        byte[] byteArray = Encoding.ASCII.GetBytes( input.ReadString());
        MemoryStream stream = new MemoryStream( byteArray );
        
        return new TiledMap(stream);
    }
}
public class TilesetReader : ContentTypeReader<TiledTileset>
{
    protected override TiledTileset Read(ContentReader input, TiledTileset existingInstance)
    {
        byte[] byteArray = Encoding.ASCII.GetBytes( input.ReadString());
        MemoryStream stream = new MemoryStream( byteArray );
        
        return new TiledTileset(stream);
    }
}