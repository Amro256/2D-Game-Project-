using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject text;
    public GameObject redText;
    public GameObject endText;
    public GameObject keyText;
    public Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 inputVector, moveVector;
    private Vector3 groundCheckA, groundCheckB, ceilingCheckA, ceilingCheckB;
    private float yVel;
    private CapsuleCollider2D col;  
    public float gravity = 9.81f;
    public float jumpVel = 9.81f;
    public float speed = 5f;
    public float springForce = 10f;
    public float climbVel = 9.81f;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayers, enemeyLayer, ceilingLayers;
    bool grounded, jumpedPressed, jumping, squishEnemy, extraJump, ceilinged, climbing;
    private Animator animator;
    bool facingRight = true;
    public bool laddered, wasLaddered;
    public AudioManager am;
    float sinceLastFootStep;
    float timeBetweenFootSteps = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
       col = GetComponent<CapsuleCollider2D>();
       sr = GetComponent<SpriteRenderer>();
       animator = GetComponent<Animator>();
       Manager.lastCheckPoint = transform.position;
       am = FindObjectOfType<AudioManager>();
       CalculateScales();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        CalculateMovement();
        ContolAnimation();
        //print("Laddered = " + laddered);
        //animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));        
    }

    void GetInputs()
    {
        if(!Manager.gamePaused)
        {
            inputVector =  new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //Allows the player to move using A/D and the left/right arrow keys
         jumpedPressed = Input.GetButtonDown("Jump"); //allows the player to jump 

         if(Input.GetAxisRaw("Horizontal") > 0 && !facingRight)  //flips the players sprite when facing the other direction
         {
            Flip(); 
         }
         if(Input.GetAxisRaw("Horizontal") < 0 && facingRight) 
         {
           Flip(); 
         }

         void Flip() 
        {
            Vector3 currentScale  = rb.transform.localScale;
            currentScale.x *= -1;
            rb.transform.localScale = currentScale;

            facingRight = !facingRight;
        }

        }
         
        
    }

    void ContolAnimation()
    {
        if(inputVector.x != 0f)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        animator.SetBool("jumping", jumping);
        animator.SetBool("climbing", climbing);

    }

    void CalculateMovement()
    {
        if(!Manager.gamePaused) 
        {
            grounded = CheckCollision(groundCheckA, groundCheckB, groundLayers); //checks if the player is grounded and displays a message in the console//
        ceilinged = CheckCollision(ceilingCheckA, ceilingCheckB, ceilingLayers);

        if(jumpedPressed)
        {
            jumpedPressed = false;
            if(grounded)
            {
                jumping = true;
                yVel = jumpVel;
                am.AudioTrigger(AudioManager.SoundFXCat.Jump, transform.position,0.25f);
            }
            if(extraJump) 
            {
                extraJump = false;
                jumping = true;
                yVel = jumpVel;
            }
            
        }

        if(!grounded && yVel < 0f)
        {
            squishEnemy = CheckCollision(groundCheckA, groundCheckB, enemeyLayer);
            if(squishEnemy) 
            {
                extraJump = true;
                jumping = true;
                yVel = jumpVel * 0.5f;
            }
        }

        if(grounded && yVel <= 0f || ceilinged && yVel > 0f) 
        {
             if(grounded && jumping)
             am.AudioTrigger(AudioManager.SoundFXCat.HitGround, transform.position,1f);
             if(ceilinged && jumping)
             am.AudioTrigger(AudioManager.SoundFXCat.HitCeiling, transform.position,1f);
            
            yVel = 0f;
            jumping = false;
           
        }
        else 
        {
            yVel -= gravity * Time.deltaTime;
        }

        if(laddered && !wasLaddered)
        {
            if(inputVector.y != 0f)
            {
                climbing = true;
                wasLaddered = true;
            }
        }

        if(wasLaddered && !laddered)
        {
            climbing = false;
            wasLaddered = false;
        }

        if(climbing ) 
        {
            yVel = climbVel * inputVector.y;
        }

        moveVector.y = yVel;
        moveVector.x = inputVector.x * speed;

        sinceLastFootStep += Time.deltaTime;
        if(moveVector.x != 0f && grounded)
        {
            if(sinceLastFootStep > timeBetweenFootSteps) 
            {
                sinceLastFootStep = 0f;
                am.AudioTrigger(AudioManager.SoundFXCat.FootStepConcrete, transform.position,0.15f);
            }
        }

        if(moveVector.y != 0f && laddered)
        {
            if(sinceLastFootStep > timeBetweenFootSteps)
            {
                sinceLastFootStep = 0f;
                am.AudioTrigger(AudioManager.SoundFXCat.FootStepWood, transform.position,1f);
            }
        }

        }
           
    }

    bool CheckCollision(Vector3 a, Vector3 b,  LayerMask l)
    {
         Collider2D colA = Physics2D.OverlapCircle(transform.position - a, groundCheckRadius, l);
         Collider2D colB = Physics2D.OverlapCircle(transform.position - b, groundCheckRadius, l);
         if(colA) 
         {
            if(l == enemeyLayer && yVel <0f) 
            {
                colA.gameObject.GetComponent<EnemyHealthSystem>().RecieveHit(1);
            }
            return true;
         }else if(colB)
         {
            if(l == enemeyLayer && yVel <0f) 
            {
                colB.gameObject.GetComponent<EnemyHealthSystem>().RecieveHit(1);
            }
            return true;
         }
         else 
         {
            return false;
         }

    }

    void CalculateScales()
    {
        groundCheckA = - col.offset - new Vector2(col.size.x / 2f - (groundCheckRadius * 1.2f), -col.size.y/2f);
        groundCheckB = - col.offset - new Vector2(- col.size.x / 2f + (groundCheckRadius * 1.2f), -col.size.y / 2f);

        ceilingCheckA = - col.offset - new Vector2(col.size.x / 2f - (groundCheckRadius * 1.2f), col.size.y/2f);
        ceilingCheckB = - col.offset - new Vector2(- col.size.x / 2f + (groundCheckRadius * 1.2f), col.size.y / 2f);
    }

    private void FixedUpdate()
    {  
       rb.velocity = moveVector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position - groundCheckA, groundCheckRadius); //draws small circles on the play to check when they have collided with the ground//
        Gizmos.DrawWireSphere(transform.position - groundCheckB, groundCheckRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position - ceilingCheckA, groundCheckRadius); 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - ceilingCheckB, groundCheckRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)   
    {
        if (other.gameObject.CompareTag("Powerup")) //Speed boost powerup
        {
            speed = speed * 2f; // Increases the speed by 2
            sr.color = Color.blue; // Changes the sr colour to indicate powerup state
            Destroy(other.gameObject); // Once picked up by player, it will destroy
            StartCoroutine(StopSpeed()); //Calls the StopSpeed function. Coroutine is used becuase it cotains the yield return and will return what is contained in the yield return.
        }   

        if(other.gameObject.CompareTag("Jumpboost")) //Jump boost power up
        {
            jumpVel = jumpVel * 1.5f; //increases the jumpVel by 1.5
            sr.color = Color.yellow; //Changes the sr colour to indicate powerup state
            Destroy(other.gameObject); // Once picked up by player, it will destroy
            StartCoroutine(StopJump()); // Calls the stopJump function

        }
        if(other.gameObject.CompareTag("Extalive")) //Extra live
        {
            am.AudioTrigger(AudioManager.SoundFXCat.ExtaLive, transform.position,0.15f);
            Manager.AddLives(1);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Spikes")) //for normal spikes
        {
            Manager.AddCoins(-1);
            if(Manager.coins <=0)
            {
                Manager.coins = 1; //Prevent the coins in the UI going into the negatives 
                GameObject.FindGameObjectWithTag("Player").transform.position = Manager.lastCheckPoint; //if the player has 0 coins, the player will die and be set back to the start/checkpoint
                Manager.AddLives(-1); // if the player has 0 a life will be removed 
                
            }
        }

        if(other.gameObject.CompareTag("Dspikes")) //For the insta kill spikes.
        {
            Manager.AddLives(-1); //if the player lands on these spikes, remove a live and reset their position
            GameObject.FindGameObjectWithTag("Player").transform.position = Manager.lastCheckPoint;
        }

        if(other.gameObject.CompareTag("PopText")) //If the player enters the area the contains the text. it will be tirggered to be activated 
        {
            text.SetActive(true);
        }
        if(other.gameObject.CompareTag("PopText"))
        {
            redText.SetActive(true);
        }
        if(other.gameObject.CompareTag("PopText"))
        {
            endText.SetActive(true);
        }
        if(other.gameObject.CompareTag("PopText"))
        {
            keyText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PopText")) //if the player leaves the area that contains the text it will disappear and the text will be set to false as it is not needed  
        {
            text.SetActive(false);
        }

        if(other.gameObject.CompareTag("PopText"))
        {
            redText.SetActive(false);
        }
        if(other.gameObject.CompareTag("PopText"))
        {
            endText.SetActive(false);
        }
        if(other.gameObject.CompareTag("PopText"))
        {
            keyText.SetActive(false);
        }
        
    }

     

    IEnumerator StopSpeed() //Timer for the speed boost
    {
        yield return new WaitForSeconds(10f); // This is like a timer. After 10 seconds the player will return back to the normal state
        speed = speed / 2;     // Return to the default spped
        sr.color = Color.white; //Character goes back to the normal colour
         
    }

    IEnumerator StopJump() //Timer for jump boost
    {
        yield return new WaitForSeconds(5f); //Jump boost will last 5 seconds 
        jumpVel = jumpVel / 1.5f; // will return to the normal jump vel value
        sr.color = Color.white; //Charater goes back to their normal colour
    }
    


}
