using UnityEngine;
using System.Collections;

public class MainMenuHandler : MonoBehaviour {

    public Game myGame;
    public GameObject MenuScreenButtonParent;
    
    public void Start()
    {
        HideYoSelf();
    }

    public void HideYoSelf()
    {
        MenuScreenButtonParent.SetActive(false);
    }
    
    public void ShowYoSelf()
    {
        MenuScreenButtonParent.SetActive(true);
    }

    public void ActiveGamesButtonPressed()
    {
        myGame.ChangeScreenState(SCREENSTATE.MAIN_MENU, SCREENSTATE.ACTIVE_GAMES);
    }

    public void LobbySelectionButtonPressed()
    {
        myGame.ChangeScreenState(SCREENSTATE.MAIN_MENU, SCREENSTATE.LOBBY_SELECTION);
    }

    public void MyProfileButtonPressed()
    {
        myGame.ChangeScreenState(SCREENSTATE.MAIN_MENU, SCREENSTATE.SELF_PROFILE_VIEW);
    }

    public void LogoutButtonPressed()
    {
        myGame.ChangeScreenState(SCREENSTATE.MAIN_MENU, SCREENSTATE.START_SCREEN);
    }

}
