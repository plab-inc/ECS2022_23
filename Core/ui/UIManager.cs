using ECS2022_23.Core.entities.characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.ui;

public class UiManager
{
    private UiPanel _panel;
    private int _preHeartCount;
    private bool _hasChanged;
    public void AddPanel(UiPanel panel)
    {
        _panel = panel;
    }

    public void Update(GameTime gameTime, Player player)
    {
        _hasChanged = false;
        UpdateHearts(player);
        UpdateText(UiLabels.CoinText, player.Money);
        UpdateText(UiLabels.XpText, player.XpToNextLevel);
        if (_hasChanged)
        {
            _panel.Update(gameTime);
        }
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

        _hasChanged = true;
    }

    private void UpdateText(UiLabels label, float stats)
    {
        UiText uiText = (UiText) _panel.GetComponentByLabel(label);
        if (uiText == null) return;
        if (uiText.Text == $"{stats:0.##}") return;
       
        uiText.Text = stats <= 0 ? "0.00" : $"{stats:0.##}";
        uiText.SourceRec.Width = (int) uiText.Font.MeasureString(uiText.Text).X;
        uiText.SourceRec.Height = (int) uiText.Font.MeasureString(uiText.Text).Y;
        _hasChanged = true;
    }

    private static bool HasHeartLabel(Component component)
    {
        return component.UiLabel == UiLabels.Heart;
    }
    /*
    private void UpdateWeaponIcon(Player player)
    {
        var weapon = player.Weapon;
        var weaponIcon = _panel.GetComponentByLabel(UiLabels.WeaponIcon);
        if (weaponIcon == null) return;
        weaponIcon.SourceRec.X = (int) weapon.IconPosition.X;
        weaponIcon.SourceRec.Y = (int) weapon.IconPosition.Y;
    }
    */
}