using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private PlatformController player; //refernece to the Platform Controller Script so I can access the jump/speed

    void Start()
    {
        player = FindObjectOfType<PlatformController>();
        
    }



    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject); //Once the player collides with the powerup, its gets destroyed 
            player.speed = player.speed * 2; //Multiplies the players speed by what is set in the platform controller script 
            //player.GetComponent<SpriteRenderer>().color = Color.blue; //Changes the colour of the player to indicate power-up state
            Destroy(gameObject); //Once the player collides with the powerup, its gets destroyed
            StartCoroutine(StopSpeed());
            
            
        }
    }


    IEnumerator StopSpeed()
    {
        //player.GetComponent<SpriteRenderer>().color = Color.white; //Character goes back to the normal colour
        yield return new WaitForSeconds(2); // This is like a timer. After 10 seconds the player will return back to the normal state
        player.speed = player.speed / 2;     // Return to the default spped
         
    }
}
