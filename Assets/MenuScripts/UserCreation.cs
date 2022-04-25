using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserCreation : MonoBehaviour
{
    public InputField nameInput;
    public InputField codeInput;

    public Button ScanButton;

    public GameObject ScanScreen;

    public Text scanResult;
    private MainManager.UserData savedUser;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
#if UNITY_ANDROID
        codeInput.gameObject.SetActive(false);
#else
        ScanButton.gameObject.SetActive(false);
#endif
    }

    public void onSaveClick()
    {
        if (codeInput.text != "")
        {  
            savedUser = CodeTool.Instance.CodeToUserData(codeInput.text);
            savedUser.Name = nameInput.text;
            MainManager.Instance.users[MainManager.Instance.UL.currentUser] = savedUser;
        }
        else
        {
            MainManager.Instance.users[MainManager.Instance.UL.currentUser].Name = nameInput.text;
        }
     
        FindObjectOfType<UserSelection>(true).gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void onBackPressed()
    {
        codeInput.text = scanResult.text;
        ScanScreen.SetActive(false);
    }    

    public void onScanPressed()
    {
        CodeTool.Instance.ReadQRCode();
        ScanScreen.SetActive(true);
    }
}
