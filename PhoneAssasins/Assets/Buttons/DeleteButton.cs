using UnityEngine;
using System.Collections;

public class DeleteButton : MonoBehaviour {

    public CreateLobbys mLobbyHandler;
    public int mLobbyID;

    public void deleteLobbyClicked()
    {
        mLobbyHandler.DeleteLobby(mLobbyID);
    }

    public void Setup(CreateLobbys lobbyHandler, int lobbyID)
    {
        mLobbyHandler = lobbyHandler;
        mLobbyID = lobbyID;
    }
}
