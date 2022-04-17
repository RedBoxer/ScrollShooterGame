using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    int HighScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetScore(int gameScore)
    {
        if (gameScore > HighScore)
        {
            HighScore = gameScore;
        }
    }

    public int GetScore()
    {
        return HighScore;
    }
}
