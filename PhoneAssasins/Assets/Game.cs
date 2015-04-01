using UnityEngine;
using System.Collections;
using Amazon;

public enum SCREENSTATE { 

    START_SCREEN,
    LOBBY_SELECTION,
    LOBBY_SCREEN,
    IN_GAME
};


public class Game : MonoBehaviour {

    public GameObject startScreenButtonGroup;
    public GameObject lobbySelectionScreenButtonGroup;
    public GameObject lobbyScreenButtonGroup;
    public GameObject InGameScreenButtonGroup;

    private bool loggedIn = false;
    private SCREENSTATE currentScreen;

    public PhpInterface myInterface = null;
    public Profile myProfile = null;
    public CreateLobbys createLobbiesScript;

	// Use this for initialization
	void Start () 
    {
        currentScreen = SCREENSTATE.START_SCREEN;
	    if(myInterface == null)
        {
            Debug.Log("you had one job.... give me my interface");
        }
        if(myInterface == null)
        {
            Debug.Log("you had one more job.... give me my profile");
        }

        if (PlayerPrefs.HasKey("UserID"))
        {
            myProfile._userId = PlayerPrefs.GetInt("UserID");
            loggedIn = true;
            UpdateUserData();
            ChangeScreenState(SCREENSTATE.START_SCREEN, SCREENSTATE.LOBBY_SELECTION);
        }
	}   

    public void Update()
    {
        if (loggedIn && currentScreen == SCREENSTATE.START_SCREEN)
        {
            //change screens to lobby selection
        }
    }

    public void CreateNewUser()
    {
        //Debug.Log("Create New User Button Pressed");
        StartCoroutine(myInterface.createUserData(myProfile._name));
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
        switch(from)
        {
            case SCREENSTATE.START_SCREEN:
                startScreenButtonGroup.SetActive(false);
                break;
            case SCREENSTATE.LOBBY_SELECTION:
                createLobbiesScript.HideYoSelf();
                break;
        }

        switch(to)
        {
            case SCREENSTATE.LOBBY_SELECTION:
                createLobbiesScript.showYoSelf(myProfile._userId);
                break;
            case SCREENSTATE.START_SCREEN:
                startScreenButtonGroup.SetActive(true);
                PlayerPrefs.DeleteKey("UserID");
                break;

        }
    }

    public void Refresh()
    {
        if( currentScreen == SCREENSTATE.LOBBY_SELECTION)
        {
            createLobbiesScript.showYoSelf(myProfile._userId);
        }
    }

    public void Logout()
    {
        ChangeScreenState(SCREENSTATE.LOBBY_SELECTION, SCREENSTATE.START_SCREEN);
    }

    public void AttemptToEnterLobby(Object lobbyButton)
    {
        Debug.Log("attempting to enter lobby");
    }

    public void createLobby()
    {
        //myInterface.createLobbyData(myProfile._userId, )
        StartCoroutine(myInterface.createLobbyData(myProfile._userId, createLobbiesScript.NewLobbyName));
        
    }

	// Update is called once per frame
    //_TextRect = GUILayoutUtility.GetRect(new GUIContent(_Longest), "button");
}
