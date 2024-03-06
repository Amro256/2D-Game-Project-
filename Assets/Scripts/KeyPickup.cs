using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public Manager.DoorKeyColours keyColours;
    public GameObject Text; // Allows text to be dragged into the box in the inspector


    void Start() 
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

         switch(keyColours) 
           {
              case Manager.DoorKeyColours.Red: 
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
           Text.SetActive(false); //text is intially set to false becuase it only needs to be seen when the player picks the key up
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player")  //when the player collide with the key, sound willl play and it will destroy itself
        {
            Manager.KeyPickup(keyColours);
             FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.PickupKey, transform.position,0.25f); //Find the audio manager to trigger the audio
             Text.SetActive(true); // Once the player picks up the key, the text will pop up in the top right.
            Destroy(gameObject);
        }
    }

    void onTriggerExit2D()
    {
        Text.SetActive(false); //Text does not disappear. Instead make it fade out 
    } 

}
