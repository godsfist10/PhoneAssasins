using UnityEngine;
using System.Collections;

public class LobbySelectionButton : MonoBehaviour {

    public string lobbyName = "";
    public int mlobbyID;
    public bool mIsHost = false;
    public UnityEngine.UI.Text TextOnButton;

    private Game myGame;

    public void Start()
    {
        TextOnButton.text = lobbyName;
    }

    public void ChangeLobbyName(string name)
    {
        TextOnButton.text = name;
        lobbyName = name;
    }

    public void Setup(string lobbyName, int lobbyID, Game game, bool isHost = false)
    {
        ChangeLobbyName(lobbyName);
        mlobbyID = lobbyID;
        myGame = game;
        mIsHost = isHost;
    }

    public void EnterLobby()
    {
        myGame.EnterLobby(this.gameObject);
    }

}
