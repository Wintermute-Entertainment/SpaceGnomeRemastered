using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text finalScoreText;
    public TMP_Text previousHighScoreText;

    public TMP_Text highScoreText;
    
    public GameObject startPanel;
    public GameObject gameOverPanel;

    [SerializeField] GameObject gnome;

    //[SerializeField] string highScore1; //For adding more HighScores later.

   public SGInput input;

    private void Awake()
    {
      Time.timeScale = 0; 

      input = new SGInput();

    if (!startPanel.activeInHierarchy)
    {
       startPanel.SetActive(true);
            gnome.SetActive(false);
    }

        input.UI.Click.performed += ctx => StartButton();

    }
    void OnEnable()
    {
        input.UI.Enable();
    }
    void OnDisable()
    {
        input.UI.Disable();
    }

    public void Update()
    {
        scoreText.text = Toolbox.instance.m_playerManager.score.ToString();
      
            if (Input.GetKeyDown(KeyCode.Escape) )
            {
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                    startPanel.SetActive(false);
                    gnome.SetActive(true);
                }
                else if (Time.timeScale == 1)
                {
                    Time.timeScale = 0;
                    startPanel.SetActive(true);
                    gnome.SetActive(false);
                }
            }
    }
   
    public void Quit()
    {
        Application.Quit();
        Debug.Log("User quit.");
    }
    public void StartButton()
    {
        Debug.Log("StartButton used.");
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            startPanel.SetActive(false);
        }
       else if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            startPanel.SetActive(true);
        }
        
    }
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
    public void RestartButton()
    {
        Toolbox.instance.m_playerManager.score = 0;
        SceneManager.LoadScene(0);
        gameOverPanel.SetActive(false);
        startPanel.SetActive(true);
    }
}
