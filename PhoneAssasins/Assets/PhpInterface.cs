using UnityEngine;
using System.Collections;

public class PhpInterface : MonoBehaviour {

    public const string SECRET = "Rlb7UT6UlQr0TpW";
    private const string FUNCTIONSURL = "http://www.mattstruble.com/PhoneAssassins/Functions/";
    public Game myGame = null;
    private Profile myProfile = null;
    private CreateLobbys lobbyButtonCreator;

    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    public void Start()
    {
        if (myGame == null)
            Debug.Log("attach Game script to PHPinterface");
        else
        {
            myProfile = myGame.getMyProfile();
            lobbyButtonCreator = myGame.getLobbyCreationScript();
        }
    }
    //Inputs

    public IEnumerator createUserData(string username, string password)
    {
        string urlCall = FUNCTIONSURL + "create_user_data.php?";
        string passHash = Md5Sum(password + SECRET);
        string hash = Md5Sum(username + passHash + SECRET);
        string post_url = urlCall + "username=" + WWW.EscapeURL(username) + "&password=" + passHash + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            switch (output)
            {
                case "WRONG_HASH":
                    Debug.Log(output);
                    break;
                case "USERNAME_TAKEN":
                    Debug.Log(output);
                    myGame.getStartScreenScript().UsernameTaken();
                    break;
                case "USER_CREATED":
                    Debug.Log(output);
                    break;
                default:
                    Debug.Log(output);
                    myProfile._userId = int.Parse(output);
                    Debug.Log("User Id: " + output);
                    PlayerPrefs.SetInt("UserID", myProfile._userId);
                    myGame.UpdateUserData();
                    myGame.ChangeScreenState(SCREENSTATE.START_SCREEN, SCREENSTATE.MAIN_MENU);
                    break;
            }
        }
    }

    public IEnumerator userLogin(string username, string password)
    {
        string urlCall = FUNCTIONSURL + "user_login.php?";
        string passHash = Md5Sum(password + SECRET);
        string hash = Md5Sum(username + passHash + SECRET);
        string post_url = urlCall + "username=" + WWW.EscapeURL(username)+ "&password=" + passHash + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            switch (output)
            {
                case "WRONG_HASH":
                    Debug.Log(output);
                    break;
                case "USER_NOT_FOUND":
                    myGame.getStartScreenScript().IncorrectInformation();
                    Debug.Log(output);
                    break;
                default:    
                    myProfile._userId = int.Parse(output);
                    myGame.UpdateUserData();
                    myGame.ChangeScreenState(SCREENSTATE.START_SCREEN, SCREENSTATE.MAIN_MENU);
                    break;
            }
        }
    }

    public IEnumerator userLeaveLobby(int userID, int lobbyID)
    {
        string urlCall = FUNCTIONSURL + "user_leave_lobby.php?";
        string hash = Md5Sum(userID.ToString() + lobbyID.ToString() + SECRET);

        string post_url = urlCall + "userid=" + userID.ToString() + "&lobbyid=" + lobbyID.ToString() + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            switch (output)
            {
                case "WRONG_HASH":  
                case "LOBBY_NOT_FOUND":
                    Debug.Log(output);
                    break;
                case "USER_REMOVED":
                    myGame.Refresh();
                    break;
                default:
                    Debug.Log("Output not consistant with list: " + output);
                    break;
            }
        }
    }

    public IEnumerator userJoinLobby(int userID, int lobbyID)
    {
        string urlCall = FUNCTIONSURL + "user_join_lobby.php?";
        string hash = Md5Sum(userID.ToString() + lobbyID.ToString() + SECRET);

        string post_url = urlCall + "userid=" + userID.ToString() + "&lobbyid=" + lobbyID.ToString() + "&hash=" + hash;
        //Debug.Log("PostUrl:  " + post_url);
        WWW hs_post = new WWW(post_url);
       
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            switch (output)
            {
                case "WRONG_HASH":
                case "USER_ALREADY_ADDED":
                case "LOBBY_NOT_FOUND":
                    Debug.Log(output);
                    break;
                case "USER_ADDED":
                    myGame.Refresh();
                    break;
                default:
                    Debug.Log("Output not consistant with list: " + output);
                    break;
            }
        }
    }

    public IEnumerator updateUserData(int userID, int kills, int wins, int plays)
    {
        string urlCall = FUNCTIONSURL + "update_user_data.php?";
        string hash = Md5Sum(userID + kills + wins + plays + SECRET);

        string post_url = urlCall + "userid=" + userID + "&kills=" + kills + "&wins=" + wins + "&plays=" + plays + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            switch (output)
            {
                case "WRONG_HASH":
                case "USER_UPDATED":
                    Debug.Log(output);
                    break;
                default:
                    Debug.Log("Output not consistant with list: " + output);
                    break;
            }
        }
    }

    public IEnumerator startGame(int userID, int lobbyID)
    {
        string urlCall = FUNCTIONSURL + "start_game.php?";
        string hash = Md5Sum(userID + lobbyID + SECRET);

        string post_url = urlCall + "userid=" + userID + "&lobbyid=" + lobbyID + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            switch (output)
            {
                case "WRONG_HASH":
                case "GAME_STARTED":
                case "LOBBY_NOT_FOUND":
                    Debug.Log(output);
                    break;
                default:
                    Debug.Log("Output not consistant with list: " + output);
                    break;
            }
        }
    }

    public IEnumerator createLobbyData(int userID, string lobbyName)
    {
        string urlCall = FUNCTIONSURL + "create_lobby_data.php?";
        string hash = Md5Sum(userID + lobbyName + SECRET);

        string post_url = urlCall + "userid=" + userID + "&lobbyname=" + WWW.EscapeURL(lobbyName) + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            switch (output)
            {
                case "WRONG_HASH":
                    Debug.Log(output);
                    break;
                default:
                    myGame.getLobbyCreationScript().LobbyCreated();
                    Debug.Log("Lobby ID:  " + output);
                    break;
            }
        }
    }
   
    //Outputs

    public IEnumerator getUserData(int userID)
    {
        string urlCall = FUNCTIONSURL + "get_user_data.php?";
        string hash = Md5Sum(userID + SECRET);

        string post_url = urlCall + "userid=" + userID + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            bool goodOut = false;
            switch (output)
            {
                case "WRONG_HASH":
                case "NOT_FOUND":
                    Debug.Log(output);
                    break;
                default:
                    Debug.Log("Returning data: " + output);
                    goodOut = true;
                    break;
            }

            if (goodOut)
            {
               // username,kills,wins,plays,updated
                string[] splitOutput = output.Split(',');
                //Debug.Log("Username:  " + splitOutput[0]);
                myProfile._name = splitOutput[0];
                //Debug.Log("Kills:  " + splitOutput[1]);
                myProfile._killCount = int.Parse(splitOutput[1]);
                //Debug.Log("Wins:  " + splitOutput[2]);
                myProfile._gamesWon = int.Parse(splitOutput[2]);
                //Debug.Log("Plays:  " + splitOutput[3]);
                myProfile._gamesPlayed = int.Parse(splitOutput[3]);
                //Debug.Log("Updated:  " + splitOutput[4]);
            }
        }
    }

    public IEnumerator getOtherUserData(int userID)
    {
        string urlCall = FUNCTIONSURL + "get_user_data.php?";
        string hash = Md5Sum(userID.ToString() + SECRET);

        string post_url = urlCall + "userid=" + userID.ToString() + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            bool goodOut = false;
            switch (output)
            {
                case "WRONG_HASH":
                case "NOT_FOUND":
                    Debug.Log(output);
                    break;
                default:
                    Debug.Log("Returning data: " + output);
                    goodOut = true;
                    break;
            }

            if (goodOut)
            {
                ProfileViewer temp = myGame.getProfileViewer();
                // username,kills,wins,plays,updated
                string[] splitOutput = output.Split(',');
                //Debug.Log("Username:  " + splitOutput[0]);
                temp.UsernameText.text = splitOutput[0];
                //Debug.Log("Kills:  " + splitOutput[1]);
                temp.KillsText.text = splitOutput[1];
                //Debug.Log("Wins:  " + splitOutput[2]);
                temp.WinsText.text = splitOutput[2];
                //Debug.Log("Plays:  " + splitOutput[3]);
                temp.PlayedText.text = splitOutput[3];
                //Debug.Log("Updated:  " + splitOutput[4]);
            }
        }
    }

    public IEnumerator getLobbyUsers(int lobbyID)
    {
        string urlCall = FUNCTIONSURL + "get_lobby_users.php?";
        string hash = Md5Sum(lobbyID + SECRET);

        string post_url = urlCall + "lobbyid=" + lobbyID + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            bool goodOut = false;
            switch (output)
            {
                case "WRONG_HASH":
                case "NOT_FOUND":
                    Debug.Log(output);
                    break;
                default:
                    Debug.Log("Returning data: " + output);
                    goodOut = true;
                    break;
            }

            if (goodOut)
            {
                //userid,targetid,updated,created|userid,targetid
                string[] splitOutput = output.Split('|');

                int numberOfPeopleInLobby = splitOutput.Length;

                for (int i = 0; i < numberOfPeopleInLobby; i++)
                {
                    //userid,targetid,updated,created
                    string[] userData = splitOutput[i].Split(',');
                    Debug.Log("UserID:  " + userData[0]);
                    Debug.Log("TargetID:  " + userData[1]);
                    Debug.Log("Updated:  " + userData[2]);
                    Debug.Log("Created:  " + userData[3]);
                }

            }
        }
    }

    public IEnumerator getLobbyUsers_Output(int lobbyID)
    {
        string urlCall = FUNCTIONSURL + "get_lobby_users.php?";
        string hash = Md5Sum(lobbyID + SECRET);

        string post_url = urlCall + "lobbyid=" + lobbyID + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            bool goodOut = false;
            switch (output)
            {
                case "WRONG_HASH":
                case "NOT_FOUND":
                    Debug.Log(output);
                    break;
                default:
                    Debug.Log("Returning data: " + output);
                    goodOut = true;
                    break;
            }

            if (goodOut)
            {
                //userid,targetid,updated,created|userid,targetid
                string[] splitOutput = output.Split('|');

                int numberOfPeopleInLobby = splitOutput.Length;

                for (int i = 0; i < numberOfPeopleInLobby - 1; i++)
                {
                    //userid,targetid,updated,created
                    string[] userData = splitOutput[i].Split(',');
                    Debug.Log("UserID:  " + userData[0]);
                    StartCoroutine(getUserButtonData(int.Parse(userData[0])));
                    //myGame.getLobbyHandler().CreateUserButton()
                    /*Debug.Log("TargetID:  " + userData[1]);
                    Debug.Log("Updated:  " + userData[2]);
                    Debug.Log("Created:  " + userData[3]);*/
                }

            }
        }
    }

    public IEnumerator getUserButtonData(int userID)
    {
        string urlCall = FUNCTIONSURL + "get_user_data.php?";
        string hash = Md5Sum(userID + SECRET);

        string post_url = urlCall + "userid=" + userID + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            bool goodOut = false;
            switch (output)
            {
                case "WRONG_HASH":
                case "NOT_FOUND":
                    Debug.Log(output);
                    break;
                default:
                    //Debug.Log("Returning data: " + output);
                    goodOut = true;
                    break;
            }

            if (goodOut)
            {
                // username,kills,wins,plays,updated
                string[] splitOutput = output.Split(',');
                myGame.getLobbyHandler().CreateUserButton(splitOutput[0], userID);
            }
        }
    }

    public IEnumerator getLobbyData(int lobbyID)
    {
        string urlCall = FUNCTIONSURL + "get_lobby_data.php?";
        string hash = Md5Sum(lobbyID + SECRET);

        string post_url = urlCall + "lobbyid=" + lobbyID + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            bool goodOut = false;
            switch (output)
            {
                case "WRONG_HASH":
                case "NOT_FOUND":
                    Debug.Log(output);
                    break;
                default:
                    Debug.Log("Returning data: " + output);
                    goodOut = true;
                    break;
            }

            if (goodOut)
            {
                //lobbyname,hostid,gamestarted,updated,created
                string[] lobbyData = output.Split(',');
                Debug.Log("Lobby Name:  " + lobbyData[0]);
                Debug.Log("Host ID:  " + lobbyData[1]);
                Debug.Log("Game Started (0 false; 1 true):  " + lobbyData[2]);
                //Debug.Log("Updated:  " + lobbyData[3]);
                //Debug.Log("Created:  " + lobbyData[3]);
            }
        }
    }

    public IEnumerator createLobbyButton(int lobbyID)
    {
        string urlCall = FUNCTIONSURL + "get_lobby_data.php?";
        string hash = Md5Sum(lobbyID + SECRET);

        string post_url = urlCall + "lobbyid=" + lobbyID + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            bool goodOut = false;
            switch (output)
            {
                case "WRONG_HASH":
                case "NOT_FOUND":
                    Debug.Log(output);
                    break;
                default:
                    //Debug.Log("Returning data: " + output);
                    goodOut = true;
                    break;
            }

            if (goodOut)
            {
                //lobbyname,hostid,gamestarted,updated,created
                string[] lobbyData = output.Split(',');
                //Debug.Log("Lobby Name:  " + lobbyData[0]);
                lobbyButtonCreator.CreateLobbyButton(lobbyData[0], lobbyID);
            }
        }
    }

    public IEnumerator createHostedLobbyButton(int lobbyID)
    {
        string urlCall = FUNCTIONSURL + "get_lobby_data.php?";
        string hash = Md5Sum(lobbyID + SECRET);

        string post_url = urlCall + "lobbyid=" + lobbyID + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            bool goodOut = false;
            switch (output)
            {
                case "WRONG_HASH":
                case "NOT_FOUND":
                    Debug.Log(output);
                    break;
                default:
                    //Debug.Log("Returning data: " + output);
                    goodOut = true;
                    break;
            }

            if (goodOut)
            {
                //lobbyname,hostid,gamestarted,updated,created
                string[] lobbyData = output.Split(',');
                //Debug.Log("Lobby Name:  " + lobbyData[0]);
                lobbyButtonCreator.CreateHostLobbyButton(lobbyData[0], lobbyID);
            }
        }
    }

    public IEnumerator GetAvailableLobbies(int userId)
    {
        string urlCall = FUNCTIONSURL + "get_available_lobbies.php?";
        string hash = Md5Sum(userId + SECRET);

        string post_url = urlCall + "userid=" + userId + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            bool goodOut = false;
            switch (output)
            {
                case "WRONG_HASH":
                    Debug.Log(output);
                    break;
                case "":
                case "COULD_NOT_FIND_LOBBIES":
                    Debug.Log("No Lobbies Available");
                    break;
                default:
                    //Debug.Log("Returning data: " + output);
                    goodOut = true;
                    break;
            }

            if (goodOut)
            {
                //lobbyname,hostid,gamestarted,updated,created
                string[] AvailableLobbies = output.Split(',');
                for (int i = 0; i < AvailableLobbies.Length; i++)
                {
                    //Debug.Log("Lobby ID:  " + AvailableLobbies[i]);
                    StartCoroutine(createLobbyButton(int.Parse(AvailableLobbies[i])));
                }
            }
        }
    }

    public IEnumerator GetHostedLobbies(int userId)
    {
        string urlCall = FUNCTIONSURL + "get_hosted_lobbies.php?";
        string hash = Md5Sum(userId + SECRET);

        string post_url = urlCall + "userid=" + userId + "&hash=" + hash;

        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error with hs_post: " + hs_post.error);
        }
        else
        {
            string output = hs_post.text;
            bool goodOut = false;
            switch (output)
            {
                case "WRONG_HASH":
                    Debug.Log(output);
                    break;
                case "":
                case "COULD_NOT_FIND_LOBBIES":
                    Debug.Log("No Lobbies You Host Available");
                    break;
                default:
                    //Debug.Log("Returning data: " + output);
                    goodOut = true;
                    break;
            }

            if (goodOut)
            {
                //lobbyname,hostid,gamestarted,updated,created
                string[] AvailableLobbies = output.Split(',');
                for (int i = 0; i < AvailableLobbies.Length; i++)
                {
                    //Debug.Log("Lobby ID:  " + AvailableLobbies[i]);
                    StartCoroutine(createHostedLobbyButton(int.Parse(AvailableLobbies[i])));
                }
            }
        }
    }

}
