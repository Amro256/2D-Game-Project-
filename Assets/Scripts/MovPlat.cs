using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPlat : MonoBehaviour
{
    public Transform pos1, pos2; //Finds the position of the 1st position and the 2nd position
    public float moveSpeed; // Allows the move speed of the platform to be confiqured in the inspector
    public Transform startPos; //Finds the position of the starting position of the platform
    Vector3 nextPos; // Finds what the next position will be for the platform & the position
    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position; //When the game starts the next position will be set to the starting position
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == pos1.position)
        {
            nextPos = pos2.position; //Moves the platform to the 2nd point
        }
        if(transform.position == pos2.position)
        {
            nextPos = pos1.position; // moves the platform back to the first position
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime); //Moves the platform to the points
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {   
        collision.transform.SetParent(transform);
        
    }
    private void OnCollisionExit2D(Collision2D collision) 
    {   
        collision.transform.SetParent(null);
    }
}


