using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    public GameObject start,quit,lvl1,lvl2,lvl3;
    public GameObject Level2Button;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("Level1") == 1)
            lvl1.transform.GetChild(0).gameObject.SetActive(true);
        else Level2Button.GetComponent<UnityEngine.UI.Button>().interactable = false;
        lvl2.transform.GetChild(0).gameObject.SetActive(PlayerPrefs.GetInt("Level2") == 1 ? true : false);
    }
    void Update()
    {
        
    }
    public void PlayButton()
    {
        start.SetActive(false);
        quit.SetActive(false);
        lvl1.SetActive(true);
        lvl2.SetActive(true);
        lvl3.SetActive(true);
    }
    public void LevelSelect(int lvl = 1)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(lvl);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ResetButton()
    {
        PlayerPrefs.SetInt("Level1", 0);
        PlayerPrefs.SetInt("Level2", 0);
        PlayerPrefs.SetInt("HiScore1", 0);
        PlayerPrefs.SetInt("HiScore2", 0);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
