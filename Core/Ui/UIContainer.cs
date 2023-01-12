using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Ui;

public class UiContainer : UiComponent
{
    private List<UiComponent> _components = new();
    public float HeartCount;
    public UiContainer()
    {
        DestinationRec = new Rectangle(0, 0, 20 * PixelSize, (int)(Scale.X * PixelSize));
        Scale = new Vector2(2, 2);
    }

    public void Add(UiComponent uiComponent)
    {
        _components.Add(uiComponent);
        SetPositions(uiComponent);
    }
    
    public void SetPositions(UiComponent uiComponent)
    {
        var count = _components.Count;
        if (count <= 0) return;
        var width = PixelSize * Scale.X;
        var heigth = PixelSize * Scale.Y;
        
        if (count == 1)
        {
            uiComponent.DestinationRec =
                new Rectangle(DestinationRec.X, DestinationRec.Y,  (int) width, (int) heigth);
        }
        else
        {
            var prevComponent = _components[count - 1];
            var prevRect = prevComponent.DestinationRec;
            var newRect = new Rectangle((int)width * (count-1), prevRect.Y, (int)width, (int)heigth);
            uiComponent.DestinationRec = newRect;
        }
        
        uiComponent.Scale = this.Scale;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var counter = HeartCount;
        foreach (var component in _components)
        {
            if (component.UiLabel == UiLabel.HeartIcon)
            {
                if (counter <= 0) continue;
                counter--;
            }
            
            component.Draw(spriteBatch);
        }
    }
    
    public UiComponent GetComponentByLabel(UiLabel label)
    {
        foreach (var component in _components)
        {
            if (component.UiLabel == label)
            {
                return component;
            }
        }

        return null;
    }
    
    
    
}