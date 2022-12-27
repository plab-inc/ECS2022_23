#region Using Statements

using GameStateManagement;

#endregion Using Statements

namespace ECS2022_23.Core.Screens;
internal class GameOverScreen : MenuScreen
{
    #region Initialization
    public GameOverScreen(bool gameIsWon)
        : base(gameIsWon ? "You escaped!" : "Game Over" )
    {

        MenuEntry replayGameMenuEntry = new MenuEntry("Restart Game");
        MenuEntry backToMenu = new MenuEntry("Return to Menu");
            
        replayGameMenuEntry.Selected += (sender, e) => ReplayGameMenuEntrySelected(e);
        backToMenu.Selected += BackToMenuSelected;
            
        MenuEntries.Add(replayGameMenuEntry);
        MenuEntries.Add(backToMenu);
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
}