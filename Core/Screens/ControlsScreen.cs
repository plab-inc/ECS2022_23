using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Screens;

internal class ControlsScreen : GameScreen
{
    #region Initialization

    private Texture2D _controls;
    
    public ControlsScreen()
    {
        
    }

    #endregion Initialization

    #region Handle Input
    
    public override void HandleInput(InputState input)
    {
        PlayerIndex playerIndex;

        // We pass in our ControllingPlayer, which may either be null (to
        // accept input from any player) or a specific index. If we pass a null
        // controlling player, the InputState helper returns to us which player
        // actually provided the input. We pass that through to our Accepted and
        // Cancelled events, so they can tell which player triggered them.

        if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
        {
            ExitScreen();
        }
    }
    
    #endregion Handle Input

    public override void LoadContent()
    {
        base.LoadContent();
        ContentManager content = new ContentManager(ScreenManager.Game.Services, "Content/gameStateManagement");
        _controls = content.Load<Texture2D>("controls");
    }

    public override void Draw(GameTime gameTime)
    {
        SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
        SpriteFont font = ScreenManager.Font;

        // Darken down any other screens that were drawn beneath the popup.
        ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 2.5f);
        
        spriteBatch.Begin();

        spriteBatch.Draw(_controls,Vector2.Zero,Color.White);

        spriteBatch.End();
    }
}