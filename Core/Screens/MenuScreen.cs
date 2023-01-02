#region File Description

//-----------------------------------------------------------------------------
// MenuScreen.cs
//
// XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using System;
using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Manager;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion Using Statements

namespace ECS2022_23.Core.Screens;

/// <summary>
/// Base class for screens that contain a menu of options. The user can
/// move up and down to select an entry, or cancel to back out of the screen.
/// </summary>
internal abstract class MenuScreen : GameScreen
{
    #region Fields

    protected readonly List<MenuEntry> menuEntries = new List<MenuEntry>();
    private int selectedEntry;
    private string menuTitle;

    private AnimationManager _animationManager = new();
    protected const float FrameSpeed = 0.4f;
    protected Vector2 AnimationPosition;
    protected Animation Animation;
    protected Texture2D Spritesheet;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Gets the list of menu entries, so derived classes can add
    /// or change the menu contents.
    /// </summary>
    protected IList<MenuEntry> MenuEntries => menuEntries;

    #endregion Properties

    #region Initialization

    /// <summary>
    /// Constructor.
    /// </summary>
    public MenuScreen(string menuTitle)
    {
        this.menuTitle = menuTitle;

        TransitionOnTime = TimeSpan.FromSeconds(0.5);
        TransitionOffTime = TimeSpan.FromSeconds(0.5);
        _animationManager.SetScale(new Vector2(2,2));
    }

    #endregion Initialization

    #region Handle Input

    /// <summary>
    /// Responds to user input, changing the selected entry and accepting
    /// or cancelling the menu.
    /// </summary>
    public override void HandleInput(InputState input)
    {
        // Move to the previous menu entry?
        if (input.IsMenuUp(ControllingPlayer))
        {
            selectedEntry--;

            if (selectedEntry < 0)
            {
                selectedEntry = menuEntries.Count - 1;
            }
        }

        // Move to the next menu entry?
        if (input.IsMenuDown(ControllingPlayer))
        {
            selectedEntry++;

            if (selectedEntry >= menuEntries.Count)
            {
                selectedEntry = 0;
            }
        }

        // Accept or cancel the menu? We pass in our ControllingPlayer, which may
        // either be null (to accept input from any player) or a specific index.
        // If we pass a null controlling player, the InputState helper returns to
        // us which player actually provided the input. We pass that through to
        // OnSelectEntry and OnCancel, so they can tell which player triggered them.
        PlayerIndex playerIndex;

        if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
        {
            OnSelectEntry(selectedEntry, playerIndex);
        }
        else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
        {
            OnCancel(playerIndex);
        }
    }

    /// <summary>
    /// Handler for when the user has chosen a menu entry.
    /// </summary>
    protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
    {
        menuEntries[entryIndex].OnSelectEntry(playerIndex);
    }

    /// <summary>
    /// Handler for when the user has cancelled the menu.
    /// </summary>
    protected virtual void OnCancel(PlayerIndex playerIndex)
    {
        ExitScreen();
    }

    /// <summary>
    /// Helper overload makes it easy to use OnCancel as a MenuEntry event handler.
    /// </summary>
    protected void OnCancel(object sender, PlayerIndexEventArgs e)
    {
        OnCancel(e.PlayerIndex);
    }

    #endregion Handle Input

    #region Update and Draw

    /// <summary>
    /// Allows the screen the chance to position the menu entries. By default
    /// all menu entries are lined up in a vertical list, centered on the screen.
    /// </summary>
    protected virtual void UpdateMenuEntryLocations()
    {
        // Make the menu slide into place during transitions, using a
        // power curve to make things look more interesting (this makes
        // the movement slow down as it nears the end).
        float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

        // start at Y = 175; each X value is generated per entry
        Vector2 position = new Vector2(0f, 300f);

        // update each menu entry's location in turn
        foreach (var menuEntry in menuEntries)
        {
            // each entry is to be centered horizontally
            position.X = ScreenManager.GraphicsDevice.Viewport.Width / 2 + 
                ScreenManager.GraphicsDevice.Viewport.Width / 4  - menuEntry.GetWidth(this) / 4;

            // set the entry's position
            menuEntry.Position = position;

            // move down for the next entry the size of this entry
            position.Y += menuEntry.GetHeight(this) / 2f;
        }

    }

    /// <summary>
    /// Updates the menu.
    /// </summary>
    public override void Update(GameTime gameTime, bool otherScreenHasFocus,
        bool coveredByOtherScreen)
    {
        base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

        // Update each nested MenuEntry object.
        for (int i = 0; i < menuEntries.Count; i++)
        {
            bool isSelected = IsActive && (i == selectedEntry);

            menuEntries[i].Update(this, isSelected, gameTime);
        }
        _animationManager.Update(gameTime);
    }

    /// <summary>
    /// Draws the menu.
    /// </summary>
    public override void Draw(GameTime gameTime)
    {
        // make sure our entries are in the right place before we draw them
        UpdateMenuEntryLocations();

        GraphicsDevice graphics = ScreenManager.GraphicsDevice;
        SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
        SpriteFont font = ScreenManager.Font;
        
        spriteBatch.Begin(samplerState: SamplerState.LinearClamp);
        
        Color titleColor = Color.White * TransitionAlpha;
        const float titleScale = 1f;
        Vector2 titlePosition = PlaceTitle(graphics);
        Vector2 titleOrigin = font.MeasureString(menuTitle) / 2;
        
        spriteBatch.DrawString(font, menuTitle, titlePosition, titleColor, 0,
            titleOrigin, titleScale, SpriteEffects.None, 0);
        
        // Draw each menu entry in turn.
        for (int i = 0; i < menuEntries.Count; i++)
        {
            MenuEntry menuEntry = menuEntries[i];

            bool isSelected = IsActive && (i == selectedEntry);

            menuEntry.Draw(this, isSelected, gameTime);
        }
        
        spriteBatch.End();
        
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _animationManager.Draw(spriteBatch, AnimationPosition);
        spriteBatch.End();
    }

    protected void SetAnimation(Animation animation)
    {
        _animationManager ??= new AnimationManager();
        _animationManager.Play(animation);
    }

    protected void StopAnimation()
    {
        _animationManager.Stop();
    }

    protected virtual Vector2 PlaceTitle(GraphicsDevice graphics)
    {
        return new Vector2(graphics.Viewport.Width / 2 + graphics.Viewport.Width / 4, 200);
    }
    

    #endregion Update and Draw
}