using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateLobbys : MonoBehaviour {

    private PhpInterface myInterface = null;
    public Game myGame = null;
    public GameObject lobbyButtonPrefab;
    public GameObject AddNewLobbyButton;
    public GameObject InvalidLobbyNameText;
    public GameObject LobbyCreatedText;
    public GameObject RefreshButton;
    public GameObject NewLobbyInputField;

    public GameObject ButtonParentObject;
    public string NewLobbyName;

    public int buttonOffsetY = 50;

    public List<GameObject> buttonList;
    public List<GameObject> hostButtonList;

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
	
    public void CreateHostLobbyButton(string lobbyName, int lobbyId)
    {
        Vector3 pos = new Vector3(RefreshButton.transform.position.x, RefreshButton.transform.position.y + ((hostButtonList.Count + 1) * buttonOffsetY), 0);

        GameObject newButton = (GameObject)Instantiate(lobbyButtonPrefab, pos, transform.rotation);
        newButton.transform.SetParent(ButtonParentObject.transform);
        newButton.GetComponent<LobbySelectionButton>().Setup(lobbyName, lobbyId, myGame);
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

    public void showYoSelf(int userID)
    {
        //Debug.Log(userID);
        ButtonParentObject.SetActive(true);
        DestroyLobbyButtons();
        PopulateButtons(userID);
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
