using UnityEngine;
using System.Collections;

public class InGameHandler : MonoBehaviour {

    public Game myGame = null;
    private PhpInterface myInterface = null;
    public GameObject ButtonParentObject;
    public GameObject killConfirmedParentObject;
    public GameObject TargetButtonGroup;
    public GameObject pendingIndicator;
    public UnityEngine.UI.Button killedTargetButton;

    public UnityEngine.UI.Text MyKillersName;
    public GameObject myTargetObject;

    //private bool KillButtonPressedOnce = false;
    private int previousId = -1;

    public void Start()
    {
        HideYoSelf();

        myInterface = myGame.getMyInterface();
    }

    public void HideYoSelf()
    {
        MyKillersName.text = "";
        myTargetObject.GetComponent<UserButton>().disableButton();
        ButtonParentObject.SetActive(false);
    }

    public void ShowYoSelf(int lobbyId = -1)
    {
        if (lobbyId != -1)
            previousId = lobbyId;
        else
            lobbyId = previousId;

        TargetButtonGroup.SetActive(true);
        ButtonParentObject.SetActive(true);
        killConfirmedParentObject.SetActive(false);
        pendingIndicator.SetActive(false);
        killedTargetButton.interactable = false;
        StartCoroutine(myInterface.getLobbyUsers(lobbyId));
    }

    public void Refresh()
    {
        ShowYoSelf(-1);
    }

    public void BackButtonPressed()
    {
        myGame.ChangeScreenState(SCREENSTATE.IN_GAME, SCREENSTATE.ACTIVE_GAMES);
    }

    public void setAssasinatorsName(string name)
    {
        MyKillersName.text = name;
    }

    public void setupTargetButton(int playerId, string playerName)
    {

        myTargetObject.GetComponent<UserButton>().Setup(playerName, playerId, myGame);
    }

    public void KilledThemButtonPressed()
    {
        StartCoroutine(myInterface.KillTarget(myGame.getMyProfile()._userId, previousId));
        killedTargetButton.interactable = false;
        pendingIndicator.SetActive(true);
    }

    public void KilledMe_Confirm()
    {
        StartCoroutine(myInterface.setKillConfirmedStatus(myGame.getMyProfile()._userId, previousId, 1));
        myGame.ChangeScreenState(SCREENSTATE.IN_GAME, SCREENSTATE.ACTIVE_GAMES);
    }

    public void KillPending(bool val)
    {
        if(val)
        {
            pendingIndicator.SetActive(true);
            killedTargetButton.interactable = false;
        }
        else
        {
            pendingIndicator.SetActive(false);
            killedTargetButton.interactable = true;
        }

    }

    public void KilledMe_Deny()
    {
        StartCoroutine(myInterface.setKillConfirmedStatus(myGame.getMyProfile()._userId, previousId, -1));
        killConfirmedParentObject.SetActive(false);
    }

    public void KillPendingOnYou(bool val)
    {
        if( val)
        {
            killConfirmedParentObject.SetActive(true);
        }
        else
        {
            killConfirmedParentObject.SetActive(false);
            
        }

    }

    public void DeathThreat(string assasinatorsName)
    {
        setAssasinatorsName(assasinatorsName);
        killConfirmedParentObject.SetActive(true);
    }
}
