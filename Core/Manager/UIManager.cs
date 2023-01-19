using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Ui;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Manager;

public static class UiManager
{
    public static UiContainer StatsContainer { get; set; }

    public static void Init()
    {
        StatsContainer = new UiContainer();
    }

    public static void Update(Player player)
    {
        StatsContainer.HeartCount = player.HP;
        UpdateText(StatsContainer, UiLabel.EpText, player.EP);
        UpdateText(StatsContainer, UiLabel.LevelText, player.Level);
        UpdateText(StatsContainer, UiLabel.ArmorText, player.Armor);
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        StatsContainer.Draw(spriteBatch);
    }

    private static void UpdateText(UiContainer panel, UiLabel label, float stats)
    {
        var uiText = (UiText) panel.GetComponentByLabel(label);
        if (uiText == null) return;
        if (uiText.Text == $"{stats:0.##}") return;

        uiText.Text = stats <= 0 ? "0" : $"{stats:0.##}";
        uiText.SourceRec.Width = (int) uiText.Font.MeasureString(uiText.Text).X;
        uiText.SourceRec.Height = (int) uiText.Font.MeasureString(uiText.Text).Y;
    }
}