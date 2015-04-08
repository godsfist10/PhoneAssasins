using UnityEngine;
using System.Collections;

public class ActiveLobbyButton : MonoBehaviour {

    public string lobbyName = "";
    public int mlobbyID;
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

    public void Setup(string lobbyName, int lobbyID, Game game)
    {
        ChangeLobbyName(lobbyName);
        mlobbyID = lobbyID;
        myGame = game;
    }

    public void EnterLobby()
    {
        myGame.EnterActiveLobby(this.gameObject);
    }
}
