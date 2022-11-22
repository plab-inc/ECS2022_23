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
        _panel = panel;
    }

    public void Update(GameTime gameTime, Player player)
    {
        UpdateHearts(player);
        UpdateCoinText(player);
        UpdateXpText(player);
        _panel.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _panel.Draw(spriteBatch);
    }
    private void UpdateHearts(Player player)
    {
        var heartCount = (int) player.HP;
        if (heartCount == _preHeartCount) return;
        var index = _panel.GetIndexFromLabel(UiLabels.HpIcon);
        if (index < 0) return;

        if (heartCount > 0)
        {
          
            var change = heartCount - _preHeartCount;

            if (change > 0)
            {
                for (int i = 1; i <= change; i++)
                {
                    _panel.InsertAtIndex(UiLoader.CreateHeart(), index+i);
                }
            } else if (change < 0)
            {
                change *= -1;
                for (int i = 1; i <= change; i++)
                {
                    _panel.RemoveAtIndex(index+i,UiLabels.Heart);
                }
            }
            _preHeartCount = heartCount;
        } else 
        {
            _preHeartCount = 0;
            _panel.RemoveAll(HasHeartLabel);
        }
      
    }
    private void UpdateCoinText(Player player)
    {
        var index = _panel.GetIndexFromLabel(UiLabels.CoinText);
        if (index < 0) return;
        UiText uiText = (UiText) _panel.GetComponentAtIndex(index);
        if (uiText == null) return;
        uiText.Text = player.Money <= 0 ? "0.00" : $"{player.Money:0.##}";
        uiText.SourceRec.Width = (int) uiText.Font.MeasureString(uiText.Text).X;
        uiText.SourceRec.Height = (int) uiText.Font.MeasureString(uiText.Text).Y;
    }
    
    private void UpdateXpText(Player player)
    {
        var index = _panel.GetIndexFromLabel(UiLabels.XpText);
        if (index < 0) return;
        UiText uiText = (UiText) _panel.GetComponentAtIndex(index);
        if (uiText == null) return;
        uiText.Text = player.XpToNextLevel <= 0 ? "0.00" : $"{player.XpToNextLevel:0.##}";
        uiText.SourceRec.Width = (int) uiText.Font.MeasureString(uiText.Text).X;
        uiText.SourceRec.Height = (int) uiText.Font.MeasureString(uiText.Text).Y;
    }
    
    private static bool HasHeartLabel(Component component)
    {
        return component.UiLabel == UiLabels.Heart;
    }

}