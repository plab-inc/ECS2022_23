#region File Description

//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

using System;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS2022_23.Core.Screens;

/// <summary>
/// The pause menu comes up over the top of the game,
/// giving the player options to resume or quit.
/// </summary>
internal class PauseMenuScreen : MenuScreen
{
    #region Initialization

    /// <summary>
    /// Constructor.
    /// </summary>
    public PauseMenuScreen()
        : base("Paused")
    {
        // Create our menu entries.
        MenuEntry resumeGameMenuEntry = new MenuEntry("Resume Game");
        MenuEntry quitGameMenuEntry = new MenuEntry("Quit Game");

        // Hook up menu event handlers.
        resumeGameMenuEntry.Selected += OnCancel;
        quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;

        // Add entries to the menu.
        MenuEntries.Add(resumeGameMenuEntry);
        MenuEntries.Add(quitGameMenuEntry);
    }

    #endregion Initialization

    #region Handle Input

    /// <summary>
    /// Event handler for when the Quit Game menu entry is selected.
    /// </summary>
    private void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
    {
        const string message = "Are you sure you want to quit this game?";

        MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

        confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

        ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
    }

    protected override void UpdateMenuEntryLocations()
    {
        
        // start at Y = 175; each X value is generated per entry
        Vector2 position = new Vector2(0f, 300f);

        // update each menu entry's location in turn
        foreach (var menuEntry in menuEntries)
        {
            // each entry is to be centered horizontally
            position.X = ScreenManager.GraphicsDevice.Viewport.Width / 4 + 
                ScreenManager.GraphicsDevice.Viewport.Width / 4  - menuEntry.GetWidth(this) / 4;

            // set the entry's position
            menuEntry.Position = position;

            // move down for the next entry the size of this entry
            position.Y += menuEntry.GetHeight(this);
        }
    }

    protected override Vector2 PlaceTitle(GraphicsDevice graphics)
    {
        return new Vector2(graphics.Viewport.Width / 2f, 200);
    }

    /// <summary>
    /// Event handler for when the user selects ok on the "are you sure
    /// you want to quit" message box. This uses the loading screen to
    /// transition from the game back to the main menu screen.
    /// </summary>
    private void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
    {
        LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
            new MainMenuScreen());
    }
    

    #endregion Handle Input
}