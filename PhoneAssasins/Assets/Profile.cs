using UnityEngine;
using System.Collections;

public class Profile : MonoBehaviour {

    public string _Name;
    public Texture2D _Picture; //maybe
    public string _Bio;
    public int _KillCount;
    public int _TotalKillCount;



	// Use this for initialization
	void Start ()
    {
        _KillCount = 0;
        _Name = "";
        _Bio = "";

	}
	
    void Init(string name)
    {
        _Name = name;
    }
}
