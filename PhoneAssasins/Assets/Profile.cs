using UnityEngine;
using System.Collections;

public class Profile : MonoBehaviour {

    public int _userId;
    public string _name;
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

}
