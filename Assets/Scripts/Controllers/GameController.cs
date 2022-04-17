using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int score = 0;
    public Text ScoreDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(string tag)
    {
        switch (tag)
        {
            case "Enemy":
                score++;
                break;
            case "Boss":
                score += 150;
                break;
        }

        UpdateScore();
    }

    private void UpdateScore()
    {
        ScoreDisplay.text = "" + score;
    }

    private void OnDestroy()
    {
        MainManager.Instance.SetScore(score);
    }
}
