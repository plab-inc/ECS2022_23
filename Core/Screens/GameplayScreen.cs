#region File Description

//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using System;
using Comora;
using ECS2022_23.Core.Entities.Characters;
using ECS2022_23.Core.Game;
using ECS2022_23.Core.Loader;
using ECS2022_23.Core.Manager;
using ECS2022_23.Core.Ui;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Action = ECS2022_23.Enums.Action;

#endregion Using Statements

namespace ECS2022_23.Core.Screens;

/// <summary>
/// This screen implements the actual game logic. It is just a
/// placeholder to get the idea across: you'll probably want to
/// put some more interesting gameplay in here!
/// </summary>
internal class GameplayScreen : GameScreen
{
    #region Fields

    private ContentManager content;
    private SpriteFont gameFont;

    private Player _player;
    private Escape _escape;
    private Camera _camera;
        
    private float pauseAlpha;

    #endregion Fields

    #region Initialization

    /// <summary>
    /// Constructor.
    /// </summary>
    public GameplayScreen()
    {
        TransitionOnTime = TimeSpan.FromSeconds(1.5);
        TransitionOffTime = TimeSpan.FromSeconds(0.5);
    }

    /// <summary>
    /// Load graphics content for the game.
    /// </summary>
    public override void LoadContent()
    {
        Console.WriteLine("Loading");

        if (content == null)
            content = new ContentManager(ScreenManager.Game.Services, "Content");

        ScreenMusic = content.Load<SoundEffect>("Sounds/Music/music_background");
        
        ContentLoader.Load(content);
        SoundLoader.LoadSounds(content);
        AnimationLoader.Load(content);
        ItemLoader.Load(content);
        UiLoader.Load(content, ScreenManager.GraphicsDevice);

        _player = new Player(content.Load<Texture2D>("sprites/astro"), AnimationLoader.CreatePlayerAnimations())
        {
            Weapon = ItemLoader.CreatePhaserWeapon(Vector2.Zero)
        };
        
        InventoryManager.Init(_player);
        
        _camera = new Camera(ScreenManager.GraphicsDevice)
        {
            Zoom = 3f
        };
        
        _escape = new Escape(_player, 3,1);
        _escape.AttachCamera(_camera);
        // once the load has finished, we use ResetElapsedTime to tell the game's
        // timing mechanism that we have just finished a very long frame, and that
        // it should not try to catch up.
        ScreenManager.Game.ResetElapsedTime();
    }

    /// <summary>
    /// Unload graphics content used by the game.
    /// </summary>
    public override void UnloadContent()
    {
        Console.WriteLine("Unloading");
        ContentLoader.Unload(content);
        content.Unload();
    }

    #endregion Initialization

    #region Update and Draw

    /// <summary>
    /// Updates the state of the game. This method checks the GameScreen.IsActive
    /// property, so the game will stop updating when the pause menu is active,
    /// or if you tab away to a different application.
    /// </summary>
    public override void Update(GameTime gameTime, bool otherScreenHasFocus,
        bool coveredByOtherScreen)
    {
        base.Update(gameTime, otherScreenHasFocus, false);
        
        // Gradually fade in or out depending on whether we are covered by the pause screen.
        if (coveredByOtherScreen)
        {
            pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
        }
        else
        {
            pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);
        }

        if (IsActive)
        {
            _escape.Update(gameTime);

            if (_escape.WasSuccessful)
            {
                LoadingScreen.Load(ScreenManager, false, null,
                    new BackgroundScreen(true, true), new GameOverScreen(true));
            }
            if (_escape.Failed)
            {
                LoadingScreen.Load(ScreenManager, false, null,
                    new BackgroundScreen(true, false), new GameOverScreen(false));
            }
                
            UiManager.Update(_player);
            CombatManager.Update(gameTime, _player);
            InventoryManager.Update(_player);
        }
    }

    /// <summary>
    /// Lets the game respond to player input. Unlike the Update method,
    /// this will only be called when the gameplay screen is active.
    /// </summary>
    public override void HandleInput(InputState input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        // Look up inputs for the active player profile.
        int playerIndex = (int) ControllingPlayer.Value;
        

        if (input.IsPauseGame(ControllingPlayer))
        {
            ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
        }
        else
        {
            Input.Update(input,playerIndex);
            
            Action action = Input.GetPlayerAction();

            _player.Moves(Input.GetMovementDirection());
            _player.Aims(Input.GetAimDirection());
                
            if (action == Action.Attacks)
            {
                _player.Attack();
            }
            if (action == Action.PicksUpItem)
            {
                ItemManager.PickItemUp(_player);
            }
            if (action == Action.OpensLocker)
            {
                if (_player.Level.PlayerIsInfrontOfLocker)
                {
                    ScreenManager.AddScreen(new LockerMenuScreen(),ControllingPlayer);
                }
            }
            if (action == Action.UseItem)
            {
                InventoryManager.UseItemAtIndex(_player, Input.ToolbarKeyDownIndex());
            }
            
        }
    }
    /// <summary>
    /// Draws the gameplay screen.
    /// </summary>
    public override void Draw(GameTime gameTime)
    {
        SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

        spriteBatch.Begin(_camera, samplerState: SamplerState.PointClamp);
        _escape.Draw(spriteBatch);
        CombatManager.Draw(spriteBatch);
        ItemManager.Draw(spriteBatch);
        spriteBatch.End();
            
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        UiManager.Draw(spriteBatch);
        InventoryManager.Draw(spriteBatch);
        spriteBatch.End();
            
        // If the game is transitioning on or off, fade it out to black.
        if (TransitionPosition > 0 || pauseAlpha > 0)
        {
            float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

            ScreenManager.FadeBackBufferToBlack(alpha);
        }
    }

    #endregion Update and Draw
}