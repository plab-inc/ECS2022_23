using ECS2022_23.Core.Manager;
using ECS2022_23.Enums;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Screens;

internal class LockerMenuScreen : MenuScreen
{
    private string usageText;
    public LockerMenuScreen() : base("Locker")
    {
        usageText =  "Select With Left/Right Arrow Keys" + "\nEnter To Transfer Item" + "\nEsc or E to Cancel.";
    }
    protected override Vector2 PlaceTitle(GraphicsDevice graphics)
    {
        return new Vector2(graphics.Viewport.Width / 2f, 100);
    }

    public override void HandleInput(InputState input)
    {
        PlayerIndex playerIndex;

        if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
        {
            OnCancel(playerIndex);
        }
        
        Input.Update(input, (int) playerIndex);
        
        if (Input.GetPlayerAction() == Action.LockerAction)
        {
            LockerManager.HandleInput(Input.LockerKeyDownAction());
        }
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        
        SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
        SpriteFont font = ScreenManager.Font;
        Color fontColor = Color.White * TransitionAlpha;
        
        Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
        
        Vector2 usageTextSize = font.MeasureString(usageText);
        Vector2 usageTextPosition = new Vector2
        {
            X = viewport.Width / 2f,
            Y = viewport.Height - 160
        };
        Vector2 usageTextOrigin = font.MeasureString(usageText) / 2;

        
        spriteBatch.Begin(samplerState: SamplerState.LinearClamp);
        spriteBatch.DrawString(font, usageText, usageTextPosition, fontColor, 0,
            usageTextOrigin, 0.5f, SpriteEffects.None, 0);
        spriteBatch.End();
        
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        LockerManager.Draw(spriteBatch);
        spriteBatch.End();
    }
}