using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ECS2022_23.Core.ui;

public class UiPanel : Component
{
    private List<Component> _components;
    private Texture2D _texture2D;
    public UiPanel(Rectangle sourceRec, Rectangle destRec, UiLabels uiLabel) : base(sourceRec)
    {
        DestinationRec = destRec;
        _components = new List<Component>();
        UiLabel = uiLabel;
    }
    
    public UiPanel(Rectangle sourceRec, Rectangle destRec, Texture2D texture2D, UiLabels uiLabel) : base(sourceRec)
    {
        DestinationRec = destRec;
        _components = new List<Component>();
        _texture2D = texture2D;
        UiLabel = uiLabel;
    }

    public void AddTexture(Texture2D texture)
    {
        _texture2D = texture;
    }
    
    public void Add(Component component)
    {
        _components.Add(component);
        component.DestinationRec = DestinationRec;
        SetPositions();
    }

    public void Remove(Component component)
    {
        _components.Remove(component);
    }

    public void Update(GameTime gameTime)
    {
        SetPositions();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (_texture2D != null)
        {
            spriteBatch.Draw(_texture2D, DestinationRec, SourceRec, Color.White);
        }
        
        foreach (var component in _components)
        {
            component.Draw(spriteBatch);
        }
    }

    private void SetPositions()
    {
        if (_components.Count <= 0)
        {
            return;
        }

        int preWidth = 0;
        foreach (var component in _components)
        {
            component.DestinationRec.X = DestinationRec.X + preWidth;
            component.DestinationRec.Y = DestinationRec.Y;
            preWidth += component.SourceRec.Width;
            SetLength(component);
        }
    }

    private void SetLength(Component component)
    {
        component.DestinationRec.Width = component.SourceRec.Width;
        component.DestinationRec.Height = component.SourceRec.Height;
    }

    public void InsertAtIndex(Component component, int index)
    {
        if (index < 0) return;
        _components.Insert(index, component);
        SetPositions();
    }

    public void RemoveAtIndex(int index, UiLabels componentUiLabel)
    {
        if (index < 0 || index > _components.Count) return;
        
        try
        {
            var component = _components[index];

            if (component.UiLabel == componentUiLabel)
            {
                _components.RemoveAt(index);
                SetPositions();
            }
            
        }
        catch (ArgumentOutOfRangeException e)
        {
            Debug.WriteLine(e.Message);
        }
      
    }

    public int GetIndexFromLabel(UiLabels uiLabel)
    {
        var index = 0;
        foreach (var component in _components)
        {
            
            if (component.UiLabel == uiLabel)
            {
                return index;
            }

            index++;
        }
        return -1;
    }

    public void RemoveAll(Predicate<Component> cPredicate)
    {
        _components.RemoveAll(cPredicate);
        SetPositions();
    }

    public Component GetComponentAtIndex(int index)
    {
        if(index >= 0 && index < _components.Count)
        {
            return _components[index];
        }
        return null;
    }

    public Component GetComponentByLabel(UiLabels label)
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