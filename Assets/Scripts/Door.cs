using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Manager.DoorKeyColours keyColours;
    GameObject door;
    // Start is called before the first frame update
    void Start()
    {
      door = transform.Find("door").gameObject; 
      SpriteRenderer sr = door.GetComponent<SpriteRenderer>();
      switch(keyColours)
      {
        case Manager.DoorKeyColours.Red: //sets the door colour to the allocated colour 
        sr.color = Color.red;
        break;
        case Manager.DoorKeyColours.Blue:
        sr.color = Color.blue;
        break;
        case Manager.DoorKeyColours.Yellow:
        sr.color = Color.yellow;
        break;
        case Manager.DoorKeyColours.Green:
        sr.color = Color.green;
        break;
    
      }    
    }


    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player" && door!= null)  //the null prevent the sound clip playing each time the player walks through the door
        {
            switch(keyColours) 
            {
                case Manager.DoorKeyColours.Red:
                if(Manager.redKey)  //if the player has the correct colour key then the sound will play and then the door will destroy itself 
                { 

                FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.OpenDoor, transform.position,0.25f); //find the audio manager to tirgger the audio for the door
                Destroy(door);

                } 
                break;
                case Manager.DoorKeyColours.Blue:
                if(Manager.blueKey)
                {
                  FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.OpenDoor, transform.position,0.25f);
                  Destroy(door);
                } 
                break;
                case Manager.DoorKeyColours.Yellow:
                if(Manager.yellowKey)
                {
                  FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.OpenDoor, transform.position,0.25f);
                  Destroy(door);
                } 
                break;
                case Manager.DoorKeyColours.Green:
                if(Manager.greenKey)
                {
                  FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.OpenDoor, transform.position,0.25f);
                  Destroy(door);
                } 
                break;


            }



        }
    }


}
