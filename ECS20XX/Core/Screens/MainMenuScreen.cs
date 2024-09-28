#region File Description

//-----------------------------------------------------------------------------
// MainMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using ECS2022_23.Core.Loader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion Using Statements

namespace ECS2022_23.Core.Screens;

/// <summary>
///     The main menu screen is the first thing displayed when the game starts up.
/// </summary>
internal class MainMenuScreen : MenuScreen
{
    private ContentManager content;

    #region Initialization

    /// <summary>
    ///     Constructor fills in the menu contents.
    /// </summary>
    public MainMenuScreen()
        : base("ECS20XX")
    {
        ScreenMusic = SoundLoader.Blueberry;

        // Create our menu entries.
        var playGameMenuEntry = new MenuEntry("Play Game");
        var optionsMenuEntry = new MenuEntry("Options");
        var exitMenuEntry = new MenuEntry("Exit");
        var controlsMenuEntry = new MenuEntry("Controls");

        // Hook up menu event handlers.
        playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
        optionsMenuEntry.Selected += OptionsMenuEntrySelected;
        controlsMenuEntry.Selected += ControlsMenuEntrySelected;
        exitMenuEntry.Selected += OnCancel;

        // Add entries to the menu.
        MenuEntries.Add(playGameMenuEntry);
        //MenuEntries.Add(optionsMenuEntry);
        MenuEntries.Add(controlsMenuEntry);
        MenuEntries.Add(exitMenuEntry);
    }

    public override void LoadContent()
    {
        if (content == null)
            content = new ContentManager(ScreenManager.Game.Services, "Content/GameStateManagement");

        Spritesheet = content.Load<Texture2D>("../Sprites/spritesheet");

        if (Spritesheet != null)
        {
            Animation = new Animation.Animation(Spritesheet, 16, 16, 7, new Point(1, 2), true);
            AnimationPosition = new Vector2(16 * 18, 16 * 21);
            Animation.FrameSpeed = FrameSpeed;
            SetAnimation(Animation);
        }
    }

    #endregion Initialization

    #region Handle Input

    /// <summary>
    ///     Event handler for when the Play Game menu entry is selected.
    /// </summary>
    private void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
    {
        LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
            new GameplayScreen());
    }

    private void ControlsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
    {
        ScreenManager.AddScreen(new ControlsScreen(), e.PlayerIndex);
    }

    /// <summary>
    ///     Event handler for when the Options menu entry is selected.
    /// </summary>
    private void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
    {
        ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
    }

    /// <summary>
    ///     When the user cancels the main menu, ask if they want to exit the sample.
    /// </summary>
    protected override void OnCancel(PlayerIndex playerIndex)
    {
        const string message = "Are you sure you want to exit this game?";

        var confirmExitMessageBox = new MessageBoxScreen(message);

        confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

        ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
    }

    /// <summary>
    ///     Event handler for when the user selects ok on the "are you sure
    ///     you want to exit" message box.
    /// </summary>
    private void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
    {
        ScreenManager.Game.Exit();
    }

    #endregion Handle Input
}