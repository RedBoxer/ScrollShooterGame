using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserSelection : MonoBehaviour
{
    public Button[] userButtons;

    public Text currentMode;

    bool inDeleteMode = false;

    public UserCreation UserCreation;
    // Start is called before the first frame update
    void Start()
    {
        UserCreation = FindObjectOfType<UserCreation>(true);
    }

    private void OnEnable()
    {
        LoadUsersToSelection();
    }

    public void LoadUsersToSelection()
    {
        int buttonNum = 0;
        foreach (MainManager.UserData user in MainManager.Instance.users)
        {
            if (user.Name != "")
            {
                userButtons[buttonNum].GetComponentInChildren<Text>().text = user.Name;
            }

            buttonNum++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void User1Selected()
    {
        if (MainManager.Instance.users[0].Name != "")
        {
            if (inDeleteMode)
            {
                MainManager.Instance.users[0].Name = "";
                MainManager.Instance.users[0].HighScore = 0;
            }
            else
            {
                MainManager.Instance.SetCurrentUser(0);
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            MainManager.Instance.SetCurrentUser(0);
            UserCreation.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void User2Selected()
    {
        if (MainManager.Instance.users[1].Name != "")
        {
            if (inDeleteMode)
            {
                MainManager.Instance.users[1].Name = "";
                MainManager.Instance.users[1].HighScore = 0;
            }
            else
            {
                MainManager.Instance.SetCurrentUser(1);
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            MainManager.Instance.SetCurrentUser(1);
            UserCreation.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void User3Selected()
    {
        if (MainManager.Instance.users[2].Name != "")
        {
            if (inDeleteMode)
            {
                MainManager.Instance.users[2].Name = "";
                MainManager.Instance.users[2].HighScore = 0;
            }
            else
            {
                MainManager.Instance.SetCurrentUser(2);
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            MainManager.Instance.SetCurrentUser(2);
            UserCreation.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void onSwitchMode()
    {
        inDeleteMode = !inDeleteMode;
    }
}
