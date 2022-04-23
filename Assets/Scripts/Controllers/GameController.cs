using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int score = 0;
    public Text ScoreDisplay;

    private PlayerController player;
    private SpawnController spawner;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        spawner = FindObjectOfType<SpawnController>();
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

    public void ResetGame()
    {
        score = 0;
        UpdateScore();
        player.ResetPlayer();  
        spawner.waveCount = 0;
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }
    }

    private void OnDestroy()
    {
        MainManager.Instance.SetCurrentUserScore(score);
        MainManager.Instance.SaveGame();
    }
}
