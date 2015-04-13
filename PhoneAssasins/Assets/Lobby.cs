using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Amazon;

public class Lobby : MonoBehaviour 
{
    public Game myGame = null;
    private PhpInterface myInterface = null;
   
    public GameObject ButtonParentObject;
    public GameObject lobbyUserButtonPrefab;
    public GameObject JoinLobbyButton;
    public GameObject LeaveLobbyButton;
    public GameObject DisbandLobbyButton;
    public GameObject StartGameButton;

    public GameObject morePlayersRequiredIndicator;

    public bool mIsHost = false;
    public int currentLobbyId = 0;
    private List<GameObject> userList;
    private int buttonOffsetY = 50;


    public void Start()
    {
        if (myGame == null)
            Debug.Log("assign game to lobby script");
        else
            myInterface = myGame.getMyInterface();

        userList = new List<GameObject>();

        ButtonParentObject.SetActive(false);
    }

    public void JoinLobby()
    {
        StartCoroutine(myInterface.userJoinLobby(myGame.getMyProfile()._userId, currentLobbyId));
    }

    public void LeaveLobby()
    {
        StartCoroutine(myInterface.userLeaveLobby(myGame.getMyProfile()._userId, currentLobbyId));
    }

    public void StartGame()
    {
        if (userList.Count < 2)
        {
            morePlayersRequiredIndicator.SetActive(true);
        }
        else
        {
            StartCoroutine(myInterface.startGame(myGame.getMyProfile()._userId, currentLobbyId));
            BackButtonPressed();
        }
    }

    public void CreateUserButton(string userName, int userId)
    {
        Vector3 pos = new Vector3(JoinLobbyButton.transform.position.x, JoinLobbyButton.transform.position.y + ((userList.Count + 1) * buttonOffsetY), 0);

        GameObject newButton = (GameObject)Instantiate(lobbyUserButtonPrefab, pos, transform.rotation);
        newButton.transform.SetParent(ButtonParentObject.transform);
        newButton.GetComponent<UserButton>().Setup(userName, userId, myGame);
        userList.Add(newButton);

        if(myGame.getMyProfile()._userId == userId)
        {
            JoinLobbyButton.SetActive(false);
            LeaveLobbyButton.SetActive(true);
        }
    }

    public void ResetToJoin()
    {
        JoinLobbyButton.SetActive(true);
        LeaveLobbyButton.SetActive(false);
        DisbandLobbyButton.SetActive(false);
        StartGameButton.SetActive(false);
        IsYOUTHEHOST();
    }

    public void HideYoSelf()
    {
        JoinLobbyButton.SetActive(true);
        LeaveLobbyButton.SetActive(false);
        DisbandLobbyButton.SetActive(false);
        StartGameButton.SetActive(false);
        ButtonParentObject.SetActive(false);
    }

    public void IsYOUTHEHOST()
    {
        if (mIsHost)
        {
            DisbandLobbyButton.SetActive(true);
            StartGameButton.SetActive(true);
            JoinLobbyButton.SetActive(false);
            LeaveLobbyButton.SetActive(false);
        }

    }

    public void HostCheck(int hostID)
    {
        if (myGame.getMyProfile()._userId == hostID)
            mIsHost = true;
        else
            mIsHost = false;

        IsYOUTHEHOST();
    }

    public void Refresh()
    {
        ShowYoSelf(-1);
    }

    public void ShowYoSelf(int lobbyId = -1)
    {
        ButtonParentObject.SetActive(true);
        mIsHost = false;
        morePlayersRequiredIndicator.SetActive(false);
        DestroyUserButtons();
        ButtonParentObject.SetActive(true);
        if (lobbyId == -1)
            lobbyId = currentLobbyId;
        else
            currentLobbyId = lobbyId;

        ResetToJoin();
        StartCoroutine(myInterface.getLobbyUsers_Output(lobbyId));
        StartCoroutine(myInterface.getLobbyData(lobbyId));
        
    }

    public void DestroyUserButtons()
    {
        for (int i = 0; i < userList.Count; i++)
        {
            Destroy(userList[i]);
        }
        userList.Clear();
    }

    public void BackButtonPressed()
    {
        myGame.ChangeScreenState(SCREENSTATE.LOBBY_SCREEN, SCREENSTATE.LOBBY_SELECTION);
    }

}
