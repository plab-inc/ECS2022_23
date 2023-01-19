#region File Description

//-----------------------------------------------------------------------------
// MessageBoxScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using System;
using ECS2022_23.Core.Manager.ScreenManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion Using Statements

namespace ECS2022_23.Core.Screens;

/// <summary>
///     A popup message box screen, used to display "are you sure?"
///     confirmation messages.
/// </summary>
internal class MessageBoxScreen : GameScreen
{
    #region Handle Input

    /// <summary>
    ///     Responds to user input, accepting or cancelling the message box.
    /// </summary>
    public override void HandleInput(InputState input)
    {
        PlayerIndex playerIndex;

        // We pass in our ControllingPlayer, which may either be null (to
        // accept input from any player) or a specific index. If we pass a null
        // controlling player, the InputState helper returns to us which player
        // actually provided the input. We pass that through to our Accepted and
        // Cancelled events, so they can tell which player triggered them.
        if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
        {
            // Raise the accepted event, then exit the message box.
            if (Accepted != null)
                Accepted(this, new PlayerIndexEventArgs(playerIndex));

            ExitScreen();
        }
        else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
        {
            // Raise the cancelled event, then exit the message box.
            if (Cancelled != null)
                Cancelled(this, new PlayerIndexEventArgs(playerIndex));

            ExitScreen();
        }
    }

    #endregion Handle Input

    #region Draw

    /// <summary>
    ///     Draws the message box.
    /// </summary>
    public override void Draw(GameTime gameTime)
    {
        var spriteBatch = ScreenManager.SpriteBatch;
        var font = ScreenManager.Font;

        // Darken down any other screens that were drawn beneath the popup.
        ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 2.5f);

        // Center the message text in the viewport.
        var viewport = ScreenManager.GraphicsDevice.Viewport;
        var viewportSize = new Vector2(viewport.Width, viewport.Height);

        var messageSize = font.MeasureString(message);
        var messagePosition = viewportSize - messageSize / 2;
        messagePosition.Y /= 1.5f;

        var usageTextSize = font.MeasureString(usageText);
        var usageTextPosition = viewportSize - usageTextSize / 2;


        spriteBatch.Begin();

        // Draw the background rectangle.

        // Draw the message box text.
        spriteBatch.DrawString(font, message, messagePosition, Color.White, 0,
            messagePosition, 0.5f, SpriteEffects.None, 0);

        if (usageText != "")
            spriteBatch.DrawString(font, usageText, usageTextPosition, Color.White, 0,
                usageTextPosition, 0.5f, SpriteEffects.None, 0);

        spriteBatch.End();
    }

    #endregion Draw

    #region Fields

    private readonly string message;
    private readonly string usageText;

    #endregion Fields

    #region Events

    public event EventHandler<PlayerIndexEventArgs> Accepted;

    public event EventHandler<PlayerIndexEventArgs> Cancelled;

    #endregion Events

    #region Initialization

    /// <summary>
    ///     Constructor automatically includes the standard "A=ok, B=cancel"
    ///     usage text prompt.
    /// </summary>
    public MessageBoxScreen(string message)
        : this(message, true)
    {
    }

    /// <summary>
    ///     Constructor lets the caller specify whether to include the standard
    ///     "A=ok, B=cancel" usage text prompt.
    /// </summary>
    public MessageBoxScreen(string message, bool showUsageText)
    {
        this.message = message;

        if (showUsageText)
            usageText = "\nConfirm with Space or Enter." + "\nEsc to Cancel.";
        else
            usageText = "";

        IsPopup = false;
        TransitionOnTime = TimeSpan.FromSeconds(0.2);
        TransitionOffTime = TimeSpan.FromSeconds(0.2);
    }

    /// <summary>
    ///     Loads graphics content for this screen. This uses the shared ContentManager
    ///     provided by the Game class, so the content will remain loaded forever.
    ///     Whenever a subsequent MessageBoxScreen tries to load this same content,
    ///     it will just get back another reference to the already loaded data.
    /// </summary>
    public override void LoadContent()
    {
        var content = ScreenManager.Game.Content;
    }

    #endregion Initialization
}