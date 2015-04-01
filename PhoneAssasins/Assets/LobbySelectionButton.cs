using UnityEngine;
using System.Collections;

public class LobbySelectionButton : MonoBehaviour {

    public string lobbyName = "";
    public int mlobbyID;
    public UnityEngine.UI.Text TextOnButton;

    public void Start()
    {
        TextOnButton.text = lobbyName;
    }

    public void ChangeLobbyName(string name)
    {
        TextOnButton.text = name;
        lobbyName = name;
    }

    public void Setup(string lobbyName, int lobbyID)
    {
        ChangeLobbyName(lobbyName);
        mlobbyID = lobbyID;

    }
}
