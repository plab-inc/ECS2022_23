using Microsoft.Xna.Framework.Graphics;
using MonoGameLevelGenerator.Core;

namespace ECS2022_23.Core.entities.items;

public abstract class Item : Entity
{

    protected Item(Texture2D texture) : base(texture)
    {
    }

    public virtual void Use()
    {
        
    }
}