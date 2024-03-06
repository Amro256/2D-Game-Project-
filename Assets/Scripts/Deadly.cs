using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadly : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collison) 
    {
       if(collison.tag == "Player")  //if the player collides with the kill zone
       {
        collison.gameObject.transform.position = Manager.lastCheckPoint; //The manager will reset the player back to the last checkpoint/area
        Manager.AddLives(-1); //Manager will also remove a life 
       } 
    }
}
