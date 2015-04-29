using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveLobbyHandler : MonoBehaviour {

    private PhpInterface myInterface = null;
    public Game myGame = null;

    public GameObject ButtonParentObject;
    public GameObject ActiveLobbyButtonPrefab;
    public GameObject RefreshButton;
    public GameObject NoActiveGamesIndicator;

	public bool RefreshHit = false;

    public List<GameObject> buttonList;

    public int buttonOffsetY = 50;

	// Use this for initialization
	void Start () 
    {
        buttonList = new List<GameObject>();

        myInterface = myGame.getMyInterface();

        ButtonParentObject.SetActive(false);
        NoActiveGamesIndicator.SetActive(false);
	}

    public void CreateActiveLobbyButton(string lobbyName, int lobbyId)
    {
        Vector3 pos = new Vector3(RefreshButton.transform.position.x, RefreshButton.transform.position.y + ((buttonList.Count + 1) * buttonOffsetY), 0);

        GameObject newButton = (GameObject)Instantiate(ActiveLobbyButtonPrefab, pos, transform.rotation);
        newButton.transform.SetParent(ButtonParentObject.transform);
        newButton.GetComponent<ActiveLobbyButton>().Setup(lobbyName, lobbyId, myGame);
        buttonList.Add(newButton);
    }

    public void DestroyLobbyButtons()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            Destroy(buttonList[i]);
        }
        buttonList.Clear();

    }

    public void HideYoSelf()
    {
        DestroyLobbyButtons();
        ButtonParentObject.SetActive(false);
    }

    public void showYoSelf(int userID)
    {
		if (!RefreshHit) 
		{
			RefreshHit = true;
			//Debug.Log(userID);
			ButtonParentObject.SetActive (true);
			DestroyLobbyButtons ();
			NoActiveGamesIndicator.SetActive (false);
			PopulateButtons (userID);
		}
    }

    public void PopulateButtons(int userId)
    {
        StartCoroutine(myInterface.GetActiveLobbies(userId));
    }


    public void NoActiveLobbies()
    {
        NoActiveGamesIndicator.SetActive(true);
    }

    public void BackButtonPressed()
    {
        myGame.ChangeScreenState(SCREENSTATE.ACTIVE_GAMES, SCREENSTATE.MAIN_MENU);
    }

}
