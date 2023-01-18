#region Using Statements

using System;
using System.Collections.Generic;
using ECS2022_23.Core.Animations;
using ECS2022_23.Core.Loader;
using ECS2022_23.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion Using Statements

namespace ECS2022_23.Core.Screens;

internal class GameOverScreen : MenuScreen
{
    private readonly DeathCause _deathCause;

    private readonly bool _gameIsWon;
    private ContentManager content;
    private readonly string deathMessage;
    private float StartPosition;

    public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
    {
        if (_gameIsWon)
        {
            AnimationPosition += new Vector2(0, -0.5f);
            //Check if at door 
            if (AnimationPosition.Y <= StartPosition - 16 * 5)
                //Stop walking when through door
                StopAnimation();
        }

        base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (_gameIsWon) return;

        var graphics = ScreenManager.GraphicsDevice;
        var spriteBatch = ScreenManager.SpriteBatch;
        var font = ScreenManager.Font;

        spriteBatch.Begin(samplerState: SamplerState.LinearClamp);

        var deathMessageColor = Color.White * TransitionAlpha;
        var deathMessageScale = 0.25f;
        var deathMessagePosition = new Vector2(graphics.Viewport.Width / 2f, graphics.Viewport.Height - 100);
        var deathMessageOrigin = font.MeasureString(deathMessage) / 2;

        var youDiedMessage = "You died!";
        var youDiedScale = 0.75f;
        var youDiedColor = Color.White * TransitionAlpha;
        var youDiedPosition = new Vector2(graphics.Viewport.Width / 2f, graphics.Viewport.Height - 150);
        var youDiedOrigin = font.MeasureString(youDiedMessage) / 2;

        spriteBatch.DrawString(font, youDiedMessage, youDiedPosition, youDiedColor, 0, youDiedOrigin,
            youDiedScale, SpriteEffects.None, 0);

        spriteBatch.DrawString(font, deathMessage, deathMessagePosition, deathMessageColor, 0, deathMessageOrigin,
            deathMessageScale, SpriteEffects.None, 0);

        spriteBatch.End();
    }

    private string GenerateDeathCauseMessage()
    {
        var deathCauses = new List<string>();
        var random = new Random((int) DateTime.Now.Ticks);

        switch (_deathCause)
        {
            case DeathCause.ProjectileShot:
                deathCauses.Add("Maybe avoid that projectile the next time");
                deathCauses.Add("Why not dodge that?");
                break;
            case DeathCause.Water:
                deathCauses.Add("You never learned to swim?");
                deathCauses.Add("Try to find the diving goggles");
                deathCauses.Add("Jumping into water is no way out of here");
                break;
            default:
                deathCauses.Add($"That {_deathCause} did not like you");
                deathCauses.Add($"{_deathCause} is not your friend");
                deathCauses.Add($"Next time you will kill that {_deathCause}");
                break;
        }

        var index = random.Next(deathCauses.Count);

        return deathCauses[index];
    }

    #region Initialization

    public GameOverScreen(DeathCause deathCause = DeathCause.None)
        : base(deathCause == DeathCause.None ? "You escaped!" : "Game Over")
    {
        if (deathCause == DeathCause.None) _gameIsWon = true;

        ScreenMusic = _gameIsWon ? SoundLoader.Blueberry : SoundLoader.Ominous;

        _deathCause = deathCause;
        deathMessage = GenerateDeathCauseMessage();

        var replayGameMenuEntry = new MenuEntry("Restart Game");
        var backToMenu = new MenuEntry("Return to Menu");

        replayGameMenuEntry.Selected += (sender, e) => ReplayGameMenuEntrySelected(e);
        backToMenu.Selected += BackToMenuSelected;

        MenuEntries.Add(replayGameMenuEntry);
        MenuEntries.Add(backToMenu);
    }

    public override void LoadContent()
    {
        if (content == null)
            content = new ContentManager(ScreenManager.Game.Services, "Content/gameStateManagement");
        Spritesheet = content.Load<Texture2D>("../sprites/spritesheet");

        if (Spritesheet == null) return;

        switch (_gameIsWon)
        {
            case true:
                //Walk Up Animation
                Animation = new Animation(Spritesheet, 16, 16, 6, new Point(1, 5), true);
                AnimationPosition = new Vector2(16 * 18, 16 * 21);
                break;

            case false:
                //Death Animation
                Animation = new Animation(Spritesheet, 16, 16, 1, new Point(4, 6), true);
                AnimationPosition = new Vector2(16 * 18, 16 * 21);
                break;
        }

        Animation.FrameSpeed = FrameSpeed;
        StartPosition = AnimationPosition.Y;
        SetAnimation(Animation);
    }

    #endregion Initialization

    #region Handle Input

    private void ReplayGameMenuEntrySelected(PlayerIndexEventArgs e)
    {
        LoadingScreen.Load(ScreenManager, false, e.PlayerIndex, new GameplayScreen());
    }

    private void BackToMenuSelected(object sender, PlayerIndexEventArgs e)
    {
        LoadingScreen.Load(ScreenManager, false, e.PlayerIndex, new BackgroundScreen(),
            new MainMenuScreen());
    }

    protected override void OnCancel(PlayerIndex playerIndex)
    {
        const string message = "Are you sure you want to exit this game?";

        var confirmExitMessageBox = new MessageBoxScreen(message);

        confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

        ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
    }

    private void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
    {
        ScreenManager.Game.Exit();
    }

    #endregion Handle Input
}