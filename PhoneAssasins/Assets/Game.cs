using UnityEngine;
using System.Collections;
using Amazon;

public enum SCREENSTATE { 

    START_SCREEN,
    MAIN_MENU,
    LOBBY_SELECTION,
    LOBBY_SCREEN,
    SELF_PROFILE_VIEW,
    PROFILE_VIEW,
    ACTIVE_GAMES,
    IN_GAME,
    NULL
};


public class Game : MonoBehaviour {

    public GameObject startScreenButtonGroup;
    public GameObject lobbySelectionScreenButtonGroup;
    public GameObject lobbyScreenButtonGroup;
    public GameObject InGameScreenButtonGroup;
    public GameObject profileViewScreenButtonGroup;

    private SCREENSTATE currentScreen;
    private SCREENSTATE previousScreen;

    public PhpInterface myInterface = null;
    public Profile myProfile = null;
    public CreateLobbys createLobbiesScript;
    public Lobby lobbyHandlerScript;
    public StartScreen startScreenHandler;
    public MainMenuHandler mainMenuHandler;
    public MyProfileHandler myProfileHandler;
    public ProfileViewer profileViewer;

	// Use this for initialization
	void Start () 
    {
        previousScreen = SCREENSTATE.NULL;
        currentScreen = SCREENSTATE.START_SCREEN;
	    if(myInterface == null)
        {
            Debug.Log("you had one job.... give me my interface");
        }
        if(myInterface == null)
        {
            Debug.Log("you had one more job.... give me my profile");
        }
	}   

    public void UpdateUserData()
    {
        StartCoroutine(myInterface.getUserData(myProfile._userId));
    }

    public void ChangeScreenState(SCREENSTATE from, SCREENSTATE to)
    {
        SCREENSTATE changeTo = to;

        switch(from)
        {
            case SCREENSTATE.START_SCREEN:
                startScreenHandler.HideYoSelf();
                break;
            case SCREENSTATE.LOBBY_SELECTION:
                createLobbiesScript.HideYoSelf();
                break;
            case SCREENSTATE.LOBBY_SCREEN:
                lobbyHandlerScript.HideYoSelf();
                break;
            case SCREENSTATE.PROFILE_VIEW:
                profileViewer.HideYoSelf();
                changeTo = previousScreen;
                break;
            case SCREENSTATE.SELF_PROFILE_VIEW:
                myProfileHandler.HideYoSelf();
                break;
            case SCREENSTATE.MAIN_MENU:
                mainMenuHandler.HideYoSelf();
                break;
        }

        switch (to)
        {
            case SCREENSTATE.START_SCREEN:
                startScreenHandler.ShowYoSelf();
                currentScreen = SCREENSTATE.START_SCREEN;
                break;
            case SCREENSTATE.MAIN_MENU:
                mainMenuHandler.ShowYoSelf();
                currentScreen = SCREENSTATE.MAIN_MENU;
                break;
            case SCREENSTATE.LOBBY_SELECTION:
                createLobbiesScript.showYoSelf(myProfile._userId);
                currentScreen = SCREENSTATE.LOBBY_SELECTION;
                break;
            case SCREENSTATE.LOBBY_SCREEN:
                currentScreen = SCREENSTATE.LOBBY_SCREEN;
                break;
            case SCREENSTATE.SELF_PROFILE_VIEW:
                myProfileHandler.ShowYoSelf();
                currentScreen = SCREENSTATE.SELF_PROFILE_VIEW;
                break;
            case SCREENSTATE.PROFILE_VIEW:
                previousScreen = currentScreen;
                currentScreen = SCREENSTATE.PROFILE_VIEW;
                break;
            case SCREENSTATE.NULL:
                if (changeTo == SCREENSTATE.LOBBY_SCREEN)
                {
                    currentScreen = SCREENSTATE.LOBBY_SCREEN;
                    Refresh();
                    
                }
                break;
            
        }
    }

    public void Refresh()
    {
        if( currentScreen == SCREENSTATE.LOBBY_SELECTION)
        {
            createLobbiesScript.showYoSelf(myProfile._userId);
        }
        else if( currentScreen == SCREENSTATE.LOBBY_SCREEN)
        {
            lobbyHandlerScript.ShowYoSelf(-1, true);
        }
    }

    public void AttemptToJoinLobby(Object lobbyButton)
    {
        Debug.Log("attempting to enter lobby");
        LobbySelectionButton buttonScript = ((GameObject)lobbyButton).GetComponent<LobbySelectionButton>();
        StartCoroutine(myInterface.userJoinLobby(myProfile._userId, buttonScript.mlobbyID));
    }

    public void EnterLobby(Object lobbyButton)
    {
        LobbySelectionButton buttonScript = ((GameObject)lobbyButton).GetComponent<LobbySelectionButton>();
        Debug.Log("entering lobby: '" + buttonScript.lobbyName + "'");
        ChangeScreenState(currentScreen, SCREENSTATE.LOBBY_SCREEN);

        lobbyHandlerScript.ShowYoSelf(buttonScript.mlobbyID, false, buttonScript.mIsHost);
    }

    public void ViewProfile(Object userButton, bool fromLobby = true)
    {
        UserButton buttonScript = ((GameObject)userButton).GetComponent<UserButton>();
        Debug.Log("Attempting to view profile of: '" + buttonScript.mUserName + "'");

        if (fromLobby)
        {
            profileViewer.ShowYoSelf(buttonScript.mUserId);
            ChangeScreenState(SCREENSTATE.LOBBY_SCREEN, SCREENSTATE.PROFILE_VIEW);
        }

        //previousScreen = currentScreen;
        //IMPLEMENT SWAP TO PROFILE VIEW

    }

    public Profile getMyProfile()
    {
        return myProfile;
    }

    public PhpInterface getMyInterface()
    {
        return myInterface;
    }

    public CreateLobbys getLobbyCreationScript()
    {
        return createLobbiesScript;
    }

    public StartScreen getStartScreenScript()
    {
        return startScreenHandler;
    }

    public Lobby getLobbyHandler()
    {
        return lobbyHandlerScript;
    }
	
    public ProfileViewer getProfileViewer()
    {
        return profileViewer;
    }
}
