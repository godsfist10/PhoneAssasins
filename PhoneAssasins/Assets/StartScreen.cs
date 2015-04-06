using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

    public Game myGame;

    public GameObject ButtonParentObject;
    public GameObject UsernameInputBox;
    public GameObject PasswordInputBox;
    public GameObject PasswordRetypeBox;
    public GameObject InvalidInputButton;
    public GameObject UsernameTakenButton;
    public GameObject PasswordsMismatchButton;

    private Profile myProfile;
    private PhpInterface myInterface;

    bool newUserHitOnce = false;

    public void Start()
    {
        if(myGame == null)
        {
            Debug.Log("assing game script to startscreen for efficiency");
            myGame = GameObject.FindGameObjectWithTag("GameScript").GetComponent<Game>();
        }
       
        myProfile = myGame.getMyProfile();
        myInterface = myGame.getMyInterface();

        ShowYoSelf();
    }

    public void resetPassBoxes()
    {

        UnityEngine.UI.Text text = PasswordInputBox.GetComponent<InputBox>().getTextbox();
        text.text = "";

        if (newUserHitOnce)
        {
            text = PasswordRetypeBox.GetComponent<InputBox>().getTextbox();
            text.text = "";
        }
        
    }

    public void ShowYoSelf()
    {
        ButtonParentObject.SetActive(true);
        resetAllFields();
    }

    public void HideYoSelf()
    {
        ResetAll();
        ButtonParentObject.SetActive(false);
    }

    public void NewUserAttempt()
    {
        PasswordRetypeBox.SetActive(true);
        newUserHitOnce = true;
    }

    public void NewUserButtonHit()
    {
        if (!newUserHitOnce)
            NewUserAttempt();
        else
        {
            if(myProfile.passwordCheck())
            {
                StartCoroutine(myInterface.createUserData(myProfile._name, myProfile._tempPass));
            }
            else
            {
                PasswordMismatch();
            }
        }
    }

    public void LoginButtonHit()
    {
        StartCoroutine(myInterface.userLogin(myProfile._name, myProfile._tempPass));
    }

    public void resetAllFields()
    {
       
        UnityEngine.UI.Text text = UsernameInputBox.GetComponent<InputBox>().getTextbox();
        text.text = "";

        resetPassBoxes();
    }

    public void ResetAll()
    {
        resetAllFields();
        newUserHitOnce = false;
        InvalidInputButton.SetActive(false);
        UsernameTakenButton.SetActive(false);
        PasswordsMismatchButton.SetActive(false);
        PasswordRetypeBox.SetActive(false);
    }

    public void IncorrectInformation()
    {
        Debug.Log("Incorrect Information");
        InvalidInputButton.SetActive(true);
        UsernameTakenButton.SetActive(false);
        PasswordsMismatchButton.SetActive(false);
        resetPassBoxes();
    }

    public void PasswordMismatch()
    {
        Debug.Log("Password Missmatch");
        InvalidInputButton.SetActive(false);
        UsernameTakenButton.SetActive(false);
        PasswordsMismatchButton.SetActive(true);
        resetPassBoxes();
    }

    public void UsernameTaken()
    {
        Debug.Log("Username Taken");
        UsernameTakenButton.SetActive(true);
        InvalidInputButton.SetActive(false);
        PasswordsMismatchButton.SetActive(false);
        resetPassBoxes();
    }

}
