using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text time, enemy, level, menuText, score, hiScore;
    public int levelNumber = 1;
    public int min = 3, sec = 59;
    public int enemyCount = 5;
    public int brickCount = 15;
    public static GameManager gameManagerInstance;
    public GameObject pausePanel,resumeButton,nextButton,retryButton, CScore,CHScore;
    public bool finalGateSpawned = false;

    private void Awake()
    {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else Destroy(this);
    }
    private void Start()
    {
        StartCoroutine(Timer());
    }
    public void EnemyDie(int type = 0)
    {
        enemyCount--;
        enemy.text = enemyCount.ToString();
    }
    IEnumerator Timer()
    {
        while (min >= 0)
        {
            time.text = min.ToString() + ":" + sec.ToString("D2");
            sec--;
            if (sec < 0)
            {
                sec = 59;
                min--;
            }
            yield return new WaitForSeconds(1);
        }
        Lose();
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void Retry()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelNumber);
    }
    public void NextLevel()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelNumber+1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void Win()
    {
        //Time.timeScale = 0;
        menuText.text = "Win";

        int curScore = min * 60 + sec;
        score.text = "Score : " + curScore.ToString();
        var curHiScore = PlayerPrefs.GetInt("HiScore" + levelNumber.ToString());
        curHiScore = curHiScore > curScore ? curHiScore : curScore;
        hiScore.text = "HiScore : "+curHiScore.ToString();
        PlayerPrefs.SetInt("HiScore" + levelNumber.ToString(), curHiScore);
        PlayerPrefs.SetInt("Level" + levelNumber.ToString(), 1);

        CScore.SetActive(true);
        CHScore.SetActive(true);
        pausePanel.SetActive(true);
        resumeButton.SetActive(false);
        nextButton.SetActive(true);
    }
    public void Lose()
    {
        //Time.timeScale = 0;
        menuText.text = "Lose";
        pausePanel.SetActive(true);
        resumeButton.SetActive(false);
        retryButton.SetActive(true);
    }
}
