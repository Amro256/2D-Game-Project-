using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public enum GameState{MainMenu, Paused, Playing,GameOver};
    public GameState currentState;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI lifeText;
    public Image redKeyUI, blueKeyUI, YellowKeyUI, GreenKeyUI;
    public GameObject allGameUI, mainMenuPanel, pauseMenuPanel, GameOverPanel, titleText;
    // Start is called before the first frame update
   private void Awake()
   {
       if(SceneManager.GetActiveScene().name == "MainMenu")
       {
            CheckGameState(GameState.MainMenu);
       }
       else
       {
             CheckGameState(GameState.Playing);
       }
   }

   public void CheckGameState(GameState newGameState)
   {
        currentState = newGameState;
        switch(currentState) 
        {
            case GameState.MainMenu:
            MainMenuSetup();
            break;
            case GameState.Paused:
            GamePaused();
            Manager.gamePaused = true;
            Time.timeScale = 0f;
            break;
            case GameState.Playing:
            GameActive();
            Time.timeScale = 1f;
            Manager.gamePaused = false;
            break;
            case GameState.GameOver:
            GameOver();
            Time.timeScale = 0f;
            Manager.gamePaused = true;
            break;
        }
   }

   public void MainMenuSetup() 
   {
        allGameUI.SetActive(false);
        mainMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        titleText.SetActive(true);
   }

   public void GameActive() 
   {
        allGameUI.SetActive(true);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        titleText.SetActive(false);
   }

   public void GamePaused() 
   {
        allGameUI.SetActive(true);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
        GameOverPanel.SetActive(false);
        titleText.SetActive(true);
   }
   public void GameOver() 
   {
        allGameUI.SetActive(false);
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        GameOverPanel.SetActive(true);
        titleText.SetActive(true);
   }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
    }


    void CheckInputs() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (currentState == GameState.Playing) 
            {
               AudioSource[] test = FindObjectsOfType<AudioSource>();
               foreach (AudioSource a in test)
               {
                    a.Pause(); //Will pause the BG music  and other sounds when the game is pasued 
               }
                 CheckGameState(GameState.Paused);
            }else if(currentState == GameState.Paused)
            {
               AudioSource[] test = FindObjectsOfType<AudioSource>();
               foreach (AudioSource a in test)
               {
                    a.UnPause(); //Music will resume when resuming game 
               }
                CheckGameState(GameState.Playing);
            }
        }
    }

 public void StartGame()
 {
    SceneManager.LoadScene("Level01");
    CheckGameState(GameState.Playing);
 }

 public void PauseGame()
 {
    CheckGameState(GameState.Paused);
 }

 public void ResumeGame()
 {
    CheckGameState(GameState.Playing);
 }

 public void GoToMainMenu()
 {
      SceneManager.LoadScene("MainMenu");
    CheckGameState(GameState.MainMenu);
 }

 public void QuitGame() 
 {
    Application.Quit();
    print("game quit!"); //Prints a message in the console to test if the button pressed causes the game to quit - for testing 
 }

public void UpdateCoins()
{
     coinText.text = Manager.coins.ToString();
}


public void UpdateLives()
{
     lifeText.text = Manager.lives.ToString();
}

public void UpdateKey(Manager.DoorKeyColours keyColours)
{
     switch(keyColours)
     {
          case Manager.DoorKeyColours.Red: //Upates the colour of the key in the bottom right of the UI, so when you pick up a red key, the UI will update the key to red
          redKeyUI.GetComponent<Image>().color = Color.red;
          break;
          case Manager.DoorKeyColours.Blue:
          blueKeyUI.GetComponent<Image>().color = Color.blue;
          break;
          case Manager.DoorKeyColours.Yellow:
          YellowKeyUI.GetComponent<Image>().color = Color.yellow;
          break;
          case Manager.DoorKeyColours.Green:
          GreenKeyUI.GetComponent<Image>().color = Color.green;
          break;
     }
}





}
