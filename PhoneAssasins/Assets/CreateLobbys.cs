using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateLobbys : MonoBehaviour {

    public PhpInterface myInterface = null;
    public GameObject lobbyButtonPrefab;

    public GameObject AddNewLobbyGameobject;
    public string NewLobbyName;

    public int buttonOffsetY = 60;
    public int buttonOffsetX;

    public List<GameObject> buttonList;

	// Use this for initialization
	void Start ()
    {
	    if( myInterface == null)
            Debug.Log("assign interface to create lobby script");

        buttonOffsetX = Screen.width / 2;
	}
	
    public void CreateLobbyButton(string lobbyName, int lobbyId)
    {
        Vector3 pos = new Vector3(buttonOffsetX, (buttonList.Count + 1) * buttonOffsetY, 0);

        GameObject newButton = (GameObject)Instantiate(lobbyButtonPrefab, pos, transform.rotation);
        newButton.transform.parent = this.transform;
        newButton.GetComponent<LobbySelectionButton>().Setup(lobbyName, lobbyId);

        buttonList.Add(newButton);
    }
	
    public void DestroyLobbyButtons()
    {
        buttonList.Clear();
    }

    public void HideYoSelf()
    {
        AddNewLobbyGameobject.SetActive(false);
        DestroyLobbyButtons();
    }

    public void showYoSelf(int userID)
    {
        AddNewLobbyGameobject.SetActive(true);
        StartCoroutine(myInterface.GetAvailableLobbies(userID));
    }

    public void SetNewLobbyName(Object button)
    {
        GameObject temp = (GameObject)button;
        UnityEngine.UI.Text text = (UnityEngine.UI.Text)temp.GetComponent("Text");
        NewLobbyName = text.text;
    }

}
