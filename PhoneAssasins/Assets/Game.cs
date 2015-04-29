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
    NULL,
    IGNORE
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
    public ActiveLobbyHandler activeLobbyHandler;
    public InGameHandler inGameHandler;

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
            case SCREENSTATE.ACTIVE_GAMES:
                activeLobbyHandler.HideYoSelf();
                break;
            case SCREENSTATE.IN_GAME:
                inGameHandler.HideYoSelf();
                break;
            case SCREENSTATE.IGNORE:
                ChangeScreenState(currentScreen, SCREENSTATE.IGNORE);
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
                createLobbiesScript.showYoSelf();
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
            case SCREENSTATE.ACTIVE_GAMES:
                currentScreen = SCREENSTATE.ACTIVE_GAMES;
                activeLobbyHandler.showYoSelf(myProfile._userId);
                break;
            case SCREENSTATE.IN_GAME:
                currentScreen = SCREENSTATE.IN_GAME;
                break;
            case SCREENSTATE.NULL:
                if (changeTo == SCREENSTATE.LOBBY_SCREEN)
                {
                    currentScreen = SCREENSTATE.LOBBY_SCREEN;
                    Refresh();
                }
                else if(changeTo == SCREENSTATE.IN_GAME)
                {
                    currentScreen = SCREENSTATE.IN_GAME;
                    inGameHandler.ShowYoSelf();
                }
                break;
            
        }
    }

    public void Refresh()
    {
        if( currentScreen == SCREENSTATE.LOBBY_SELECTION)
        {
            createLobbiesScript.showYoSelf();
        }
        else if( currentScreen == SCREENSTATE.LOBBY_SCREEN)
        {
            lobbyHandlerScript.Refresh();
        }
        if( currentScreen == SCREENSTATE.IN_GAME)
        {
            inGameHandler.Refresh();
        }
        if(currentScreen == SCREENSTATE.ACTIVE_GAMES)
        {
            activeLobbyHandler.showYoSelf(myProfile._userId);
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
        ChangeScreenState(currentScreen, SCREENSTATE.LOBBY_SCREEN);

        lobbyHandlerScript.ShowYoSelf(buttonScript.mlobbyID);
    }

	public void EnterLobby (int lobbyId)
	{
		ChangeScreenState(currentScreen, SCREENSTATE.LOBBY_SCREEN);
		lobbyHandlerScript.ShowYoSelf(lobbyId);
	}

    public void EnterActiveLobby(Object lobbyButton)
    {
        ActiveLobbyButton buttonScript = ((GameObject)lobbyButton).GetComponent<ActiveLobbyButton>();
        ChangeScreenState(currentScreen, SCREENSTATE.IN_GAME);

        inGameHandler.ShowYoSelf(buttonScript.mlobbyID);

    }

	public void EnterActiveLobby(int lobbyID)
	{
		ChangeScreenState(currentScreen, SCREENSTATE.IN_GAME);
		inGameHandler.ShowYoSelf(lobbyID);
	}

    public void ViewProfile(Object userButton)
    {
        UserButton buttonScript = ((GameObject)userButton).GetComponent<UserButton>();

        profileViewer.ShowYoSelf(buttonScript.mUserId);
        ChangeScreenState(SCREENSTATE.IGNORE , SCREENSTATE.PROFILE_VIEW);

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

    public ActiveLobbyHandler getActiveLobbyHandler()
    {
        return activeLobbyHandler;
    }

    public InGameHandler getInGameHandler()
    {
        return inGameHandler;
    }
}
