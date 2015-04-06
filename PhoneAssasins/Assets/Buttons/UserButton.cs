using UnityEngine;
using System.Collections;

public class UserButton : MonoBehaviour {

    public string mUserName = "";
    public int mUserId;
    public UnityEngine.UI.Text TextOnButton;

    private Game myGame;

    public void Start()
    {
        TextOnButton.text = mUserName;
    }

    public void ChangeUserName(string name)
    {
        TextOnButton.text = name;
        mUserName = name;
    }

    public void Setup(string userName, int userId, Game game)
    {
        ChangeUserName(userName);
        mUserName = userName;
        myGame = game;
    }

    public void GoToProfile()
    {
        myGame.ViewProfile(this.gameObject);
    }
}
