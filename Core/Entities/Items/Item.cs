using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public abstract class Item : Entity
{

    protected Item(Vector2 spawn,Texture2D texture) : base(spawn, texture)
    {
    }

    public virtual void Use()
    {
        
    }
}