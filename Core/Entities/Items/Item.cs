using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Entities.Items;

public abstract class Item : Entity
{

    protected Item(Texture2D texture) : base(texture)
    {
    }

    public virtual void Use()
    {
        
    }
}