using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public enum DoorKeyColours {Red,  Blue, Yellow, Green};
    public static bool redKey, blueKey, yellowKey, greenKey;
    public static Vector3 lastCheckPoint;
    public static bool gamePaused;
    static GameUI gameUI;
    public static int coins, lives;
   

    

    private void Awake() 
    {
        if(instance ==null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
        gameUI = FindObjectOfType<GameUI>();
        lives = 3;
        coins = 0;
        gameUI.UpdateCoins();
        gameUI.UpdateLives();

    }

    public static void AddCoins(int coinValue)
    {
        coins += coinValue;
        if(coins >= 100) //Extra live. When the player reahces 100 coins, take away 100 and give an extra live
        {
            coins -=100;
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.ExtaLive, Vector3.zero,0.15f);
            AddLives(1);
        }
        gameUI.UpdateCoins();
    }

     public static void AddLives(int LifeValue)
     {
        lives += LifeValue;
        if(LifeValue == -1) //if the player takes remove 1 live and play the death audio
        {
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.Death, Vector3.zero,0.15f);
        }
        if(lives <0 ) //if the player has 0 lives left, the game over UI will pop up
        {
            gameUI.CheckGameState(GameUI.GameState.GameOver);
        }
        else
        {
            gameUI.UpdateLives();
        }
     }

     public static void KeyPickup(DoorKeyColours keyColour) 
        {
           switch(keyColour) 
           {
              case DoorKeyColours.Red: //Changing the colours of the key
              redKey = true;
              break;
              case DoorKeyColours.Blue:
              blueKey = true;
              break;
              case DoorKeyColours.Yellow:
              yellowKey = true;
              break;
              case DoorKeyColours.Green:
              greenKey = true;
              break;
           }
           gameUI.UpdateKey(keyColour);
        }


        public static void UpdateCheckPoints(GameObject flag) 
        {
            lastCheckPoint = flag.transform.position;
            CheckPoint[] allCheckPoints = FindObjectsOfType<CheckPoint>();
            foreach (CheckPoint cp in allCheckPoints)
            {
                if(cp != flag.GetComponent<CheckPoint>()) 
                {
                    cp.LowerFlag();
                }
            }
        }
}
