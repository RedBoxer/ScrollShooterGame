using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

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
            else
            {
                userButtons[buttonNum].GetComponentInChildren<Text>().text = "empty";
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
                DeleteUser(0);
            }
            else 
            {
                MainManager.Instance.SetCurrentUser(0);
                SceneManager.LoadScene(1);
            }
        }
        else if (!inDeleteMode)
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
                DeleteUser(1);
            }
            else
            {
                MainManager.Instance.SetCurrentUser(1);
                SceneManager.LoadScene(1);
            }
        }
        else if (!inDeleteMode)
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
                DeleteUser(2); 
            }
            else
            {
                MainManager.Instance.SetCurrentUser(2);
                SceneManager.LoadScene(1);
            }
        }
        else if (!inDeleteMode)
        {
            MainManager.Instance.SetCurrentUser(2);
            UserCreation.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void DeleteUser(int user)
    {
        File.Delete(Application.persistentDataPath + "/" + MainManager.Instance.users[user].Name + ".json");
        MainManager.Instance.users[user].Name = "";
        MainManager.Instance.users[user].HighScore = 0;
        MainManager.Instance.SetCurrentUser(-1);
        LoadUsersToSelection();
        FindObjectOfType<MenuController>().UpdateCurrentUser();
    }

    public void onSwitchMode()
    {
        inDeleteMode = !inDeleteMode;
        if (inDeleteMode)
        {
            currentMode.text = "Delete user";
        }
        else
        {
            currentMode.text = "Choose user";
        }
    }

    public void onBackPressed()
    {
        gameObject.SetActive(false);
    }
}
