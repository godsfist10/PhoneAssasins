using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateLobbys : MonoBehaviour {

    private PhpInterface myInterface = null;
    public Game myGame = null;
    public GameObject lobbyButtonPrefab;
    public GameObject activeGameButtonPrefab;
    public GameObject activeGameIndicatorPrefab;
    public GameObject deleteButtonPrefab;
    public GameObject AddNewLobbyButton;
    public GameObject InvalidLobbyNameText;
    public GameObject LobbyCreatedText;
    public GameObject RefreshButton;
    public GameObject NewLobbyInputField;

    public GameObject noLobbiesYouHostIndicator;
    public GameObject noLobbiesAvailableIndicator;

    public GameObject ButtonParentObject;
    public string NewLobbyName;

    private int buttonOffsetY = 50;
    private int deleteXOffset = 100;
    private int activeXOffset = 115;

    public List<GameObject> buttonList;
    public List<GameObject> hostButtonList;
    public List<GameObject> deleteButtonList;

	// Use this for initialization
	void Start ()
    {
        if (myGame == null)
            Debug.Log("assign game to create lobby script");
        else
            myInterface = myGame.getMyInterface();

        if (myInterface == null)
            Debug.Log("assing myinterface to create lobby script");

        buttonList = new List<GameObject>();
        hostButtonList = new List<GameObject>();

        ButtonParentObject.SetActive(false);
	}
	
    public void CreateLobbyButton(string lobbyName, int lobbyId)
    {
        Vector3 pos = new Vector3(AddNewLobbyButton.transform.position.x, AddNewLobbyButton.transform.position.y + ((buttonList.Count + 1) * buttonOffsetY), 0);

        GameObject newButton = (GameObject)Instantiate(lobbyButtonPrefab, pos, transform.rotation);
        newButton.transform.SetParent(ButtonParentObject.transform);
        newButton.GetComponent<LobbySelectionButton>().Setup(lobbyName, lobbyId, myGame);
        buttonList.Add(newButton);
    }
	
    public void CreateHostLobbyButton(string lobbyName, int lobbyId, int lobbyStarted)
    {
        Vector3 pos = new Vector3(RefreshButton.transform.position.x, RefreshButton.transform.position.y + ((hostButtonList.Count + 1) * buttonOffsetY), 0);
        Vector3 deletePos = new Vector3(pos.x + deleteXOffset, pos.y, pos.z);
        Vector3 activePos = new Vector3(pos.x + activeXOffset, pos.y, pos.z);

        GameObject newButton = null;
        if (lobbyStarted == 0)
        {
            newButton = (GameObject)Instantiate(lobbyButtonPrefab, pos, transform.rotation);
            newButton.GetComponent<LobbySelectionButton>().Setup(lobbyName, lobbyId, myGame, true);

            GameObject deleteButton = (GameObject)Instantiate(deleteButtonPrefab, deletePos, transform.rotation);
            deleteButton.transform.SetParent(ButtonParentObject.transform);
            deleteButton.GetComponent<DeleteButton>().Setup(this, lobbyId);
            deleteButtonList.Add(deleteButton);
        }
        else
        {
            newButton = (GameObject)Instantiate(activeGameButtonPrefab, pos, transform.rotation);
            newButton.GetComponent<ActiveLobbyButton>().Setup(lobbyName, lobbyId, myGame);

            GameObject activeButton = (GameObject)Instantiate(activeGameIndicatorPrefab, activePos, transform.rotation);
            activeButton.transform.SetParent(ButtonParentObject.transform);
            deleteButtonList.Add(activeButton);
        }

        newButton.transform.SetParent(ButtonParentObject.transform);
        hostButtonList.Add(newButton);

    }

    public void DestroyLobbyButtons()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            Destroy(buttonList[i]);
        }
        buttonList.Clear();

        for (int i = 0; i < hostButtonList.Count; i++)
        {
            Destroy(hostButtonList[i]);
        }
        hostButtonList.Clear();

        for (int i = 0; i < deleteButtonList.Count; i++)
        {
            Destroy(deleteButtonList[i]);
        }
        deleteButtonList.Clear();

    }

    public void DeleteLobby(int lobbyID)
    {
        StartCoroutine(myInterface.DeleteLobby(lobbyID));
    }

    public void HideYoSelf()
    {
        ResetAll();
        ButtonParentObject.SetActive(false);
        DestroyLobbyButtons();
    }

    public void createLobby()
    {
        //myInterface.createLobbyData(myProfile._userId, )
        if (NewLobbyName != "")
        {
            StartCoroutine(myInterface.createLobbyData(myGame.getMyProfile()._userId, NewLobbyName));
        }
        else
        {
            InvalidLobbyName();
        }
    }

    public void InvalidLobbyName()
    {
        InvalidLobbyNameText.SetActive(true);
        LobbyCreatedText.SetActive(false);
    }

    public void LobbyCreated()
    {
        InvalidLobbyNameText.SetActive(false);
        LobbyCreatedText.SetActive(true);
        myGame.Refresh();
    }

    public void ResetAll()
    {
        InvalidLobbyNameText.SetActive(false);
        LobbyCreatedText.SetActive(false);
    }

    public void showYoSelf()
    {
        //Debug.Log(userID);
        noLobbiesYouHostIndicator.SetActive(false);
        noLobbiesAvailableIndicator.SetActive(false);
        int userID = myGame.getMyProfile()._userId;
        ButtonParentObject.SetActive(true);
        DestroyLobbyButtons();
        PopulateButtons(userID);
    }

    public void NoLobbiesAvailable()
    {
        noLobbiesAvailableIndicator.SetActive(true);
    }

    public void NoHostedLobbiesAvailable()
    {
        noLobbiesYouHostIndicator.SetActive(true);
    }

    public void PopulateButtons(int userId)
    {
        StartCoroutine(myInterface.GetAvailableLobbies(userId));
        StartCoroutine(myInterface.GetHostedLobbies(userId));
    }

    public void SetNewLobbyName(Object button)
    {
        GameObject temp = (GameObject)button;
        UnityEngine.UI.Text text = (UnityEngine.UI.Text)temp.GetComponent("Text");
        NewLobbyName = text.text;
    }

    public void BackButtonPressed()
    {
        myGame.ChangeScreenState(SCREENSTATE.LOBBY_SELECTION, SCREENSTATE.MAIN_MENU);
    }

}
