#region File Description

//-----------------------------------------------------------------------------
// BackgroundScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

using System;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.Manager.ScreenManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Screens;

/// <summary>
///     The background screen sits behind all the other menu screens.
///     It draws a background image that remains fixed in place regardless
///     of whatever transitions the screens on top of it may be doing.
/// </summary>
internal class BackgroundScreen : GameScreen
{
    #region Fields

    private ContentManager content;
    private Texture2D backgroundTexture;
    
    private readonly bool _gameOver;
    private readonly bool _playerWins;

    #endregion Fields

    #region Initialization

    /// <summary>
    ///     Constructor.
    /// </summary>
    public BackgroundScreen()
    {
        TransitionOnTime = TimeSpan.FromSeconds(0.5);
        TransitionOffTime = TimeSpan.FromSeconds(0.5);
    }

    public BackgroundScreen(bool gameOver, bool playerWins)
    {
        _gameOver = gameOver;
        _playerWins = playerWins;

        TransitionOnTime = TimeSpan.FromSeconds(0.5);
        TransitionOffTime = TimeSpan.FromSeconds(0.5);
    }

    /// <summary>
    ///     Loads graphics content for this screen. The background texture is quite
    ///     big, so we use our own local ContentManager to load it. This allows us
    ///     to unload before going from the menus into the game itself, wheras if we
    ///     used the shared ContentManager provided by the Game class, the content
    ///     would remain loaded forever.
    /// </summary>
    public override void LoadContent()
    {
        if (content == null)
            content = new ContentManager(ScreenManager.Game.Services, "Content/GameStateManagement");

        if (!_gameOver) backgroundTexture = content.Load<Texture2D>("background_default");

        if (_gameOver)
        {
            if (_playerWins) backgroundTexture = content.Load<Texture2D>("background_win");

            if (!_playerWins) backgroundTexture = content.Load<Texture2D>("background_loose");
        }
    }

    /// <summary>
    ///     Unloads graphics content for this screen.
    /// </summary>
    public override void UnloadContent()
    {
        content.Unload();
    }

    #endregion Initialization

    #region Update and Draw

    /// <summary>
    ///     Updates the background screen. Unlike most screens, this should not
    ///     transition off even if it has been covered by another screen: it is
    ///     supposed to be covered, after all! This overload forces the
    ///     coveredByOtherScreen parameter to false in order to stop the base
    ///     Update method wanting to transition off.
    /// </summary>
    public override void Update(GameTime gameTime, bool otherScreenHasFocus,
        bool coveredByOtherScreen)
    {
        base.Update(gameTime, otherScreenHasFocus, false);
    }

    /// <summary>
    ///     Draws the background screen.
    /// </summary>
    public override void Draw(GameTime gameTime)
    {
        var spriteBatch = ScreenManager.SpriteBatch;
        var viewport = ScreenManager.GraphicsDevice.Viewport;
        var fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

        spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        spriteBatch.Draw(backgroundTexture, fullscreen,
            new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
        spriteBatch.End();
    }

    #endregion Update and Draw
}