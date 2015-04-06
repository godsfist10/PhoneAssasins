using UnityEngine;
using System.Collections;
using Amazon;

public enum SCREENSTATE { 

    START_SCREEN,
    LOBBY_SELECTION,
    LOBBY_SCREEN,
    PROFILE_VIEW,
    IN_GAME,
    NULL
};


public class Game : MonoBehaviour {

    public GameObject startScreenButtonGroup;
    public GameObject lobbySelectionScreenButtonGroup;
    public GameObject lobbyScreenButtonGroup;
    public GameObject InGameScreenButtonGroup;
    public GameObject profileViewScreenButtonGroup;

    private bool loggedIn = false;
    private SCREENSTATE currentScreen;
    private SCREENSTATE previousScreen;

    public PhpInterface myInterface = null;
    public Profile myProfile = null;
    public CreateLobbys createLobbiesScript;
    public Lobby lobbyHandlerScript;
    public StartScreen startScreenHandler;

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

    public void Update()
    {
        if (loggedIn && currentScreen == SCREENSTATE.START_SCREEN)
        {
            //change screens to lobby selection
        }
    }

    public void LoggedIn(bool val)
    {
        loggedIn = val;
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
                changeTo = previousScreen;
                break;
        }

        switch (changeTo)
        {
            case SCREENSTATE.LOBBY_SELECTION:
                createLobbiesScript.showYoSelf(myProfile._userId);
                currentScreen = SCREENSTATE.LOBBY_SELECTION;
                break;
            case SCREENSTATE.START_SCREEN:
                startScreenHandler.ShowYoSelf();
                currentScreen = SCREENSTATE.START_SCREEN;
                break;
            case SCREENSTATE.LOBBY_SCREEN:
                if (to == SCREENSTATE.NULL)
                {
                    Refresh();
                }
                currentScreen = SCREENSTATE.LOBBY_SCREEN;
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

    public void Logout()
    {
        ChangeScreenState(SCREENSTATE.LOBBY_SELECTION, SCREENSTATE.START_SCREEN);
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

    public void ViewProfile(Object userButton)
    {
        UserButton buttonScript = ((GameObject)userButton).GetComponent<UserButton>();
        Debug.Log("Attempting to view profile of: '" + buttonScript.mUserName + "'");
        //previousScreen = currentScreen;
        //IMPLEMENT SWAP TO PROFILE VIEW

    }

    public void BackButton()
    {
        //ChangeScreenState(SCREENSTATE)
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
	
}
