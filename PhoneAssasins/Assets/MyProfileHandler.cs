using UnityEngine;
using System.Collections;

public class MyProfileHandler : MonoBehaviour {
    
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

    public void ShowYoSelf()
    {
        ScreenButtonParent.SetActive(true);
        myGame.UpdateUserData();
        SetupRefresh();
    }

    public void SetupRefresh()
    {
        Profile myProf = myGame.getMyProfile();
        UsernameText.text = myProf._name;
        KillsText.text = myProf._killCount.ToString();
        WinsText.text = myProf._gamesWon.ToString();
        PlayedText.text = myProf._gamesPlayed.ToString();
    }

    public void BackButtonPressed()
    {
        myGame.ChangeScreenState(SCREENSTATE.SELF_PROFILE_VIEW, SCREENSTATE.MAIN_MENU);
    }
}
