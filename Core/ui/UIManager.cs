using ECS2022_23.Core.entities.characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.ui;

public class UiManager
{
    private UiPanel _panel;
    private int _preHeartCount;
    
    public void AddPanel(UiPanel panel)
    {
        this._panel = panel;
    }

    public void Update(GameTime gameTime, Player player)
    {
        UpdateHearts(player);
      //  panel.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _panel.Draw(spriteBatch);
    }

    private void UpdateHearts(Player player)
    {
        var heartCount = (int) player.HP;
        if (heartCount > 0)
        {
            var change = heartCount - _preHeartCount;

            if (change > 0)
            {
                for (int i = 1; i <= change; i++)
                {
                    _panel.InsertAtIndex(UiLoader.CreateHeart(), _preHeartCount+i);
                }
            } else if (change < 0)
            {
                change *= -1;
                for (int i = 0; i < change; i++)
                {
                    _panel.RemoveAtIndex(heartCount-i,Labels.Heart);
                }
            }
            _preHeartCount = heartCount;
        } else 
        {
            _preHeartCount = 0;
            _panel.RemoveAtIndex(1, Labels.Heart);
        }
    }

  

}