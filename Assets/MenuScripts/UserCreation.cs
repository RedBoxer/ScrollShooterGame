using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserCreation : MonoBehaviour
{
    public InputField nameInput;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onSaveClick()
    {
        MainManager.Instance.users[MainManager.Instance.UL.currentUser].Name = nameInput.text;
        //FindObjectOfType<UserSelection>().LoadUsersToSelection();
        FindObjectOfType<UserSelection>(true).gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
