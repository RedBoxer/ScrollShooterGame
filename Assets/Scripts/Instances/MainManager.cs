using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public UsersList UL;

    public UserData[] users;

    [System.Serializable]
    public class UserData
    {
        public string Name = "";
        public int HighScore = 0;
        public SerializableDictionary<string, bool> killedBosses = new SerializableDictionary<string, bool>();

        public void confirmKill(string bossName)
        {
            if (!killedBosses.ContainsKey(bossName))
            {
                killedBosses.Add(bossName, false);
            }
        }

        public void equipWeapon(string bossName)
        {
            killedBosses[bossName] = !killedBosses[bossName];
        }

        public bool isBossKilled(string bossName)
        {
            return killedBosses.ContainsKey(bossName);
        }

        public void DisableAllBarrels()
        {
            List<string> temp = new List<string>(killedBosses.Keys);
            foreach (string key in temp)
            {
                if (key == "Hellicopter")
                {
                    killedBosses["Hellicopter"] = false;
                }

                if (key == "Car")
                {
                    killedBosses["Car"] = false;
                }

                if (key == "Saucer")
                {
                    killedBosses["Saucer"] = false;
                }
            }
        }

        public void DisableAllBullets()
        {
            List<string> temp = new List<string>(killedBosses.Keys);
            foreach (string key in temp)
            {
                if (key == "Tank")
                {
                    killedBosses["Tank"] = false;
                }

                if (key == "Jet")
                {
                    killedBosses["Jet"] = false;
                }

                if (key == "Train")
                {
                    killedBosses["Train"] = false;
                }
            }
        }

        public void DisableAllCases()
        {
            List<string> temp = new List<string>(killedBosses.Keys);
            foreach (string key in temp)
            {
                if (key == "AntiAir")
                {
                    killedBosses["AntiAir"] = false;
                }

                if (key == "Submarine")
                {
                    killedBosses["Submarine"] = false;
                }

                if (key == "Station")
                {
                    killedBosses["Station"] = false;
                }
            }
        }
    }

    [System.Serializable]
    public class UsersList
    {
        public string User1;
        public string User2;
        public string User3;
        public int currentUser = -1;
        public string clientId = "";
        public bool crossSave = false;
    }

    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        UL = new UsersList();

        Instance = this;
        
        DontDestroyOnLoad(gameObject);

        LoadGame();
    }

    public void SetClientId(string id)
    {
        if (UL.clientId == "")
        {
            UL.clientId = id;
        }
    }
    public void SetCurrentUserScore(int gameScore)
    {
        UserData currentUser = users[UL.currentUser];

        if (gameScore > currentUser.HighScore)
        {
            currentUser.HighScore = gameScore;
        }
    }

    public int GetCurrentUserScore()
    {
        return users[UL.currentUser].HighScore;
    }

    public void SetCurrentUserName(string name)
    {
        users[UL.currentUser].Name = name;
    }

    public string GetCurrentUserName()
    {
        return users[UL.currentUser].Name;
    }

    public void SetCurrentUser(int user)
    {
        UL.currentUser = user;
        
    }

    public UserData GetCurrentUser()
    {
        return users[UL.currentUser];
    }

    public void SaveGame()
    {
        UpdateUserNames();
        SaveUserList();
        SaveEachUser();      
    }

    public void UpdateUserNames()
    {
        UL.User1 = users[0].Name;
        UL.User2 = users[1].Name;
        UL.User3 = users[2].Name;
    }

    void SaveUserList()
    {
        string json = JsonUtility.ToJson(UL);
        File.WriteAllText(Application.persistentDataPath + "/UserList.json", json);
        Debug.Log(Application.persistentDataPath + "/UserList.json");
    }

    void SaveEachUser()
    {
        foreach(UserData user in users)
        {
            if (user.Name != "")
            {
                string json = JsonUtility.ToJson(user);
                File.WriteAllText(Application.persistentDataPath + "/" + user.Name + ".json", json);
            }
        }
    }

    public void LoadGame()
    {
        LoadUserList();
        LoadEachUser();
    }

    void LoadUserList()
    {
        string path = Application.persistentDataPath + "/UserList.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            UL = JsonUtility.FromJson<UsersList>(json);
        }
    }

    void LoadEachUser()
    {
        string path = Application.persistentDataPath + "/" + UL.User1 + ".json";
        if (File.Exists(path) && UL.User1 != "")
        {
            string json = File.ReadAllText(path);
            users[0] = JsonUtility.FromJson<UserData>(json);
        }

        path = Application.persistentDataPath + "/" + UL.User2 + ".json";
        if (File.Exists(path) && UL.User2 != "")
        {
            string json = File.ReadAllText(path);
            users[1] = JsonUtility.FromJson<UserData>(json);
        }

        path = Application.persistentDataPath + "/" + UL.User3 + ".json";
        if (File.Exists(path) && UL.User3 != "")
        {
            string json = File.ReadAllText(path);
            users[2] = JsonUtility.FromJson<UserData>(json);
        }

    }
}
