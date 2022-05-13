using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;

public class ServerManager : MonoBehaviour
{
    public static ServerManager Instance;

    public Button cloud;

    private bool crossSaveEnabled = false;

    public string ThisUserId;

    public bool connectionActive = false;

    const string serverAddress = "http://192.168.1.9:3000";

    [System.Serializable]
    public class SaveData
    {
        public string User1 = "";
        public string User1Code = "";
        public string User2 = "";
        public string User2Code = "";
        public string User3 = "";
        public string User3Code = "";
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        

        Instance = this;

        DontDestroyOnLoad(gameObject);

        StartCoroutine(CheckConnection());
    }

    public void CheckCon()
    {
        StartCoroutine(CheckConnection());
    }
    IEnumerator CheckConnection()
    {
        UnityWebRequest req = UnityWebRequest.Get(serverAddress + "/check");
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(req.error);
            cloud.interactable = false;
        }
        else 
        {
            Debug.Log(req.downloadHandler.text);
            connectionActive = true;         
        }
    }

    public void SendSave()
    {
        StartCoroutine(SendSaveC());
    }

    public void GetSave(string id)
    {
        StartCoroutine(GetSaveC(id));
    }
    IEnumerator SendSaveC()
    {
        SaveData sd = new SaveData();
        MainManager mm = MainManager.Instance;
        CodeTool ct = CodeTool.Instance;

        sd.User1 = mm.UL.User1;
        sd.User2 = mm.UL.User2;
        sd.User3 = mm.UL.User3;

        sd.User1Code = ct.UserDataToCode(mm.users[0]);
        sd.User2Code = ct.UserDataToCode(mm.users[1]);
        sd.User3Code = ct.UserDataToCode(mm.users[2]);

        string msg = JsonUtility.ToJson(sd);

        using (UnityWebRequest req = UnityWebRequest.Post(serverAddress + "/sendUser/" + mm.UL.clientId, msg))
        {
            req.SetRequestHeader("content-type", "application/json");
            req.uploadHandler.contentType = "application/json";
            req.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(msg));

            yield return req.SendWebRequest();
        };
       
    }

    IEnumerator GetSaveC(string id)
    {
        UnityWebRequest req = UnityWebRequest.Get(serverAddress + "/getSave/" + id);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(req.error);
        }
        else
        {
            MainManager mm = MainManager.Instance;
            CodeTool ct = CodeTool.Instance;
            SaveData sd = JsonUtility.FromJson<SaveData>(req.downloadHandler.text);

            if (sd.User1 != "")
            {
                mm.users[0] = ct.CodeToUserData(sd.User1Code);
                mm.users[0].Name = sd.User1;
                mm.UL.currentUser = 0;
            }

            if (sd.User2 != "")
            {
                mm.users[1] = ct.CodeToUserData(sd.User2Code);
                mm.users[1].Name = sd.User2;
            }

            if (sd.User3 != "")
            {
                mm.users[2] = ct.CodeToUserData(sd.User3Code);
                mm.users[2].Name = sd.User3;
            }

            mm.UpdateUserNames();
            FindObjectOfType<UserSelection>(true).LoadUsersToSelection();
            FindObjectOfType<MenuController>(true).UpdateCurrentUser();
        }
    }

    public void OnCloud()
    {
        string currentId = MainManager.Instance.UL.clientId;

        if (currentId == "")
        {
#if !UNITY_ANDROID
            System.DateTime moment = System.DateTime.Now;
            currentId = moment.ToLongTimeString() + moment.ToShortDateString();
            Debug.Log(currentId);
            MainManager.Instance.SetClientId(currentId);
#endif
            
        }

        ThisUserId = currentId;   
        
        if (connectionActive)
        {
            GetSave(ThisUserId);
        }
    }
}
