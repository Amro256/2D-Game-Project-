using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public int initalHitPoints = 2;
    private int hitPointsLeft;
    private SpriteRenderer sr;
    private Color IntialSpriteColour;
    public Color deathColour = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        IntialSpriteColour = sr.color;
        hitPointsLeft = initalHitPoints;
    }

    public void RecieveHit(int damage)
    {
        hitPointsLeft = hitPointsLeft - damage;
        ChangeColour();

        if(hitPointsLeft <= 0) 
        {
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.Squish, transform.position,1f);
            Destroy(gameObject);
        }
    }

    void ChangeColour() 
    {
        float percentageOfDamageTaken = 1f - ((float)hitPointsLeft/(float)initalHitPoints);
        Color newHealthColour = Color.Lerp(IntialSpriteColour, deathColour, percentageOfDamageTaken);
        sr.color = newHealthColour;
    }

    
}
