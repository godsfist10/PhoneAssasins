using UnityEngine;
using System.Collections;

public class ProfileViewer : MonoBehaviour {

    public Game myGame;
    public GameObject ScreenButtonParent;

    public UnityEngine.UI.Text UsernameText;
    public UnityEngine.UI.Text KillsText;
    public UnityEngine.UI.Text WinsText;
    public UnityEngine.UI.Text PlayedText;

    public void Start()
    {
        HideYoSelf();
    }

    public void HideYoSelf()
    {
        ScreenButtonParent.SetActive(false);
    }

    public void ShowYoSelf(int playerId)
    {
        ScreenButtonParent.SetActive(true);
        SetupRefresh(playerId);
    }

    public void SetupRefresh(int playerId)
    {
        StartCoroutine(myGame.getMyInterface().getUserData(playerId));
    }

    public void BackButtonPressed()
    {
        myGame.ChangeScreenState(SCREENSTATE.PROFILE_VIEW, SCREENSTATE.NULL);
    }
}
