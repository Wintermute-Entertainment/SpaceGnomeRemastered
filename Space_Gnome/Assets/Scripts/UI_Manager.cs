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

    public SpriteRenderer plus1;
    public SpriteRenderer plus2;
    public SpriteRenderer plus3;
    public SpriteRenderer plus4;
    public SpriteRenderer plus5;

    public GameObject numbersParent;

    public SpriteRenderer healthUIText;
    public SpriteRenderer pointsUIText;
    public SpriteRenderer timeUIText;

    public SGInput input;

    private void Awake()
    {
      //Time.timeScale = 0;
        //previousHighScoreText.text = PlayerPrefs.GetFloat("PreviousHighScore").ToString("f0"); 
      input = new SGInput();


        startPanel.SetActive(true); 
         gnome.SetActive(false);
    

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
      
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale == 0)
                {
                   startPanel.SetActive(false);
                   gnome.SetActive(true);
                    Time.timeScale = 1;

            }
                else if (Time.timeScale == 1) 
                {
                   
                   startPanel.SetActive(true);
                   gnome.SetActive(false);
                   Time.timeScale = 0;
            }
            }
        //else { startPanel.SetActive(false); }

        if (input.UI.Cancel.triggered)
            {
            if (gameOverPanel.activeInHierarchy)
                {
                    RestartButton();
                }
         
        }
    }
   
    public void Quit()
    {
        Debug.Log("User quit.");
        Application.Quit();
        
    }
    public void StartButton()
    {

        Debug.Log("StartButton used.");
        if (Time.timeScale == 0)
        {
           
            startPanel.SetActive(false);
            gnome.SetActive(true);
            Time.timeScale = 1;

        }
        else if (Time.timeScale == 1)
        {
           
            startPanel.SetActive(true);
            gnome.SetActive(false);
            Time.timeScale = 0;
        }
       // else { startPanel.SetActive(false);}
        
    }
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.DeleteKey("PreviousHighScore");
    }
    public void RestartButton()
    {
        Toolbox.instance.m_playerManager.score = 0;
        gameOverPanel.SetActive(false);
        
        SceneManager.LoadScene(0);
        
    }

}
