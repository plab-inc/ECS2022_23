#region Using Statements

using ECS2022_23.Core.Animations;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion Using Statements

namespace ECS2022_23.Core.Screens;
internal class GameOverScreen : MenuScreen
{
    private ContentManager content;
    private float StartPosition;
    private bool gameIsWon;
    
    #region Initialization
    public GameOverScreen(bool gameIsWon)
        : base(gameIsWon ? "You escaped!" : "Game Over" )
    {
        this.gameIsWon = gameIsWon;
        MenuEntry replayGameMenuEntry = new MenuEntry("Restart Game");
        MenuEntry backToMenu = new MenuEntry("Return to Menu");
            
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

        if (Spritesheet != null)
        {
            if (gameIsWon)
            {
                //Walk Up Animation
                Animation = new Animation(Spritesheet, 16, 16, 6, new Vector2(1, 5), true);
                AnimationPosition = new Vector2(16 * 18, 16 * 21);
            }
            else
            {
                //Death Animation
                Animation = new Animation(Spritesheet, 16, 16, 1, new Vector2(4, 6), true);
                AnimationPosition = new Vector2(16 * 18, 16 * 21);
            }
            Animation.FrameSpeed = FrameSpeed;
            StartPosition = AnimationPosition.Y;
            SetAnimation(Animation);
        }
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
    #endregion Handle Input

    public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
    {
        if (gameIsWon)
        {
            AnimationPosition += new Vector2(0, -0.5f);
            //Check if at door 
            if (AnimationPosition.Y <= StartPosition - 16*5)
            {
                //Stop walking when through door
                StopAnimation();
            }
        }
        base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
    }
}