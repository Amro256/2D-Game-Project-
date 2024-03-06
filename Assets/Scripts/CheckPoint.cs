using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    GameObject flag;
    // Start is called before the first frame update
    void Start()
    {
        flag = transform.Find("flag").gameObject; 
    }


    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player" && flag!= null) 
        {
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.Flag, transform.position,1f);
            flag.GetComponent<SpriteRenderer>().color = Color.red;
            Manager.UpdateCheckPoints(gameObject);
        }
    }

    public void LowerFlag() 
    {
        flag.GetComponent<SpriteRenderer>().color = Color.white; //if the player passes through another checkpoint the previous one will go back to white 
    }


}
