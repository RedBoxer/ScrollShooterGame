using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuController : MonoBehaviour
{
    public Text username;
    public Text score;
    public Text placeHolder;

    public UserSelection UserSelection;
    // Start is called before the first frame update
    void Start()
    {
        UpdateCurrentUser();   
    }

    void UpdateCurrentUser()
    {
        if (MainManager.Instance.UL.currentUser != -1)
        {
            username.gameObject.SetActive(true);
            score.gameObject.SetActive(true);
            placeHolder.gameObject.SetActive(false);

            username.text = MainManager.Instance.GetCurrentUserName();
            score.text = "High Score: " + MainManager.Instance.GetCurrentUserScore();
        }
        else
        {
            username.gameObject.SetActive(false);
            score.gameObject.SetActive(false);
            placeHolder.gameObject.SetActive(true);

            placeHolder.text = "No Active User";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNew()
    {
        //UserSelection userSelection = FindObjectOfType<UserSelection>();
        //userSelection.LoadUsersToSelection();
        FindObjectOfType<UserSelection>(true).gameObject.SetActive(true);
        //SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        MainManager.Instance.SaveGame();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}