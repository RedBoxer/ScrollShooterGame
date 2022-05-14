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

    public GameObject QrCodeScreen;
    public Image QrCode;

    private Texture emptyTexture;
    // Start is called before the first frame update
    void Start()
    {
        emptyTexture = QrCode.material.mainTexture;
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

        codeInput.text = "";
        FindObjectOfType<UserSelection>(true).gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void onBackPressed()
    {
        codeInput.text = scanResult.text;
        ScanScreen.SetActive(false);
        if (!this.gameObject.active)
        {
            MainManager.Instance.UL.clientId = codeInput.text;
            codeInput.text = "";
            QrCode.material.mainTexture = emptyTexture;
        }   
    }    

    public void onScanPressed()
    {
#if !UNITY_ANDROID      
        QrCode.material.mainTexture = CodeTool.Instance.CreateQRCode(MainManager.Instance.UL.clientId);
        QrCodeScreen.SetActive(true);
#else
        CodeTool.Instance.ReadQRCode();
        ScanScreen.SetActive(true);
#endif
    }
}
