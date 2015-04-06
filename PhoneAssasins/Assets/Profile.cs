using UnityEngine;
using System.Collections;

public class Profile : MonoBehaviour {

    public int _userId;
    public string _name;
    public string _tempPass;
    private string _tempPassCheck;
    public int _killCount;
    public int _gamesPlayed;
    public int _gamesWon;

	// Use this for initialization
	void Start ()
    {
        _killCount = 0;
        _name = "";

	}
	
    void Init(string name, int userId, int killCount, int gamesPlayed, int gamesWon)
    {
        _name = name;
        _killCount = killCount;
        _userId = userId;
        _gamesPlayed = gamesPlayed;
        _gamesWon = gamesWon;
    }

    public void SetName(string name)
    {
        _name = name;

    }

    public void SetName(Object nameObject)
    {
        GameObject temp = (GameObject)nameObject;
        UnityEngine.UI.Text text = (UnityEngine.UI.Text)temp.GetComponent("Text");
        SetName(text.text);
    }

    public void SetPass(Object passObject)
    {
        GameObject temp = (GameObject)passObject;
        UnityEngine.UI.Text text = (UnityEngine.UI.Text)temp.GetComponent("Text");
        SetPass(text.text);

    }

    private void SetPass(string pass)
    {
        _tempPass = pass;
    }

    public void SetPassCheck(Object passObject)
    {
        GameObject temp = (GameObject)passObject;
        UnityEngine.UI.Text text = (UnityEngine.UI.Text)temp.GetComponent("Text");
        SetPassCheck(text.text);

    }

    private void SetPassCheck(string pass)
    {
        _tempPassCheck = pass;
    }

    public bool passwordCheck()
    {
        if (_tempPass == _tempPassCheck)
            return true;
        else
            return false;
    }

}
