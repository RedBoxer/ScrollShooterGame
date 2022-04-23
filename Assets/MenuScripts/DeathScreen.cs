using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public Button[] weapons;

    private MainManager.UserData currentUser;

    public GameObject QrCodeScreen;
    public Image QrCode;
    public Text Code;
    // Start is called before the first frame update
    void Start()
    {
        //currentUser = MainManager.Instance.GetCurrentUser();
    }

    private void OnEnable()
    {
        currentUser = MainManager.Instance.GetCurrentUser();
        foreach (Button weapon in weapons)
        {
            if (currentUser.isBossKilled(weapon.gameObject.tag))
            {
                weapon.interactable = true;
            }

            weapon.onClick.AddListener(() => { OnWeaponPressed(weapon.gameObject); });
        }
    }

    public void OnWeaponPressed(GameObject button)
    {
        currentUser = MainManager.Instance.GetCurrentUser();
        if (button.tag != "Standart")
        {
            currentUser.equipWeapon(button.tag);
            Debug.Log(button.tag + "weapon equiped");
        }
        else
        {
            currentUser.DisableAllBarrels();
            Debug.Log("Standart equiped");
        }
    }
    public void OnPlayPressed()
    {
        FindObjectOfType<GameController>().ResetGame();
        gameObject.SetActive(false);
    }

    public void OnExitPressed()
    {
        SceneManager.LoadScene(0);
        MainManager.Instance.SaveGame();
    }

    public void onQRCodePressed()
    {
        Code.text = CodeTool.Instance.UserDataToCode(MainManager.Instance.GetCurrentUser());
        QrCode.material.mainTexture = CodeTool.Instance.CreateQRCode(MainManager.Instance.GetCurrentUser());
        QrCodeScreen.SetActive(true);
    }
    public void OnBackPressed()
    {
        QrCodeScreen.SetActive(false);
    }
}
