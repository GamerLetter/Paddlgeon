using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Paddle : MonoBehaviour
{
	public Rigidbody2D my_rb;  //Public rigidbody2d called my_rb            
	public float setMoveSpeed;  //Public float called setMoveSpeed
    private float moveSpeed;    //Private float called moveSpeed (I am not sure why these are two different variables for setting speed, January me made weird decisions with the code)
    //Public KeyCodes for  up, down, and the spacebar
	public KeyCode UpKey;                 
    public KeyCode DownKey;
    public KeyCode SpaceKey;
    public int stopSpeed; //Public int called stopSpeed
    //Setting up GamObjects for a trigger version of the paddle, a collision version of the Paddle, and a punch hitbox
    public GameObject triggerPad;
    public GameObject punch;
    public GameObject collisionPad;
    public Animator animations; //Public animator called animations
    public static bool alreadyPunching = false; //Public static bool called alreadyPunching, set to false 
    public static float punchTime = 0.5f; //Public static float called punchTime, set to 0.5f
    public static float elapsedPunchTime = 0f; //Public static float called elapsedPunchTime, set to 0f
    public float stunTime = 1f; //public float called stunTime, set to 1f
    public float elapsedStunTime = 0f; //public float called elaspsedStunTime, set to 0f


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		my_rb = GetComponent<Rigidbody2D>(); //Gets the rigid body 2D component of the parent object
        moveSpeed = setMoveSpeed; //Sets moveSpeed to equal setMoveSpeed, again not sure why I did it like this but I'm afraid of potentially breaking things if I remove this
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Every frame it calls the moveUp and the moveDown functions, allowing the player to move
        moveUp();
        moveDown();
    }

    void Update()
    {
        //Calls the letGo function to help bring the paddle to a stop
        letGo();
        //This peice of code makes it so the position of the pad with trigger collision is the exact same as the position of the pad with normal colission in both the x and y direction.
        triggerPad.transform.position = new Vector2(collisionPad.transform.position.x, collisionPad.transform.position.y);
        //If the player is punching, then the punch object is set to be the same position as the collision paddle but with a 0.5f addition for the x value
        //ElapsedPunchtime is equal to itself plus delta time
        if (alreadyPunching == true)
        {
			punch.transform.position = new Vector2 (collisionPad.transform.position.x + 0.5f, collisionPad.transform.position.y);
            elapsedPunchTime += Time.deltaTime;
        }
        //If elapsedPunchTime is greater or equal to punchTime
        //alreadyPunching is set to false, the punchs position is set to be off screen, and elapsedPunchTime is reset to 0f
        if (elapsedPunchTime >= punchTime)
        {
            alreadyPunching = false;
			punch.transform.position = new Vector2 (collisionPad.transform.position.x + 0.5f, 20f);
            elapsedPunchTime = 0f;
        }
        //if in the paddle collision handler script the variable stunned is true, then the moveSpeed is set to 0 and elapsedStunTime is set to itself plus delta time
        if (Paddle_Collision_Handler.stunned == true)
        {
            moveSpeed = 0;
            elapsedStunTime += Time.deltaTime;
        }
        //If elapsedStunTime is greater or equal to the stunTime, then stunned in the paddle_collision_handler script is set to false
        //Move speed is equal to setMovespeed (okay so I did use the variable for something, still doesn't explain why I put it in the start function)
        //elapsedStunTime is set to 0f
        if (elapsedStunTime >= stunTime)
        {
            Paddle_Collision_Handler.stunned = false;
            moveSpeed = setMoveSpeed;
            elapsedStunTime = 0f;
        }
    }
    //When this function is called, it checks if the player is pressing the UpKey.  If they are, then a force in the up direction is added to the rigid body
    void moveUp()
    {
        if (Input.GetKey(UpKey))
        {
			my_rb.AddForce(Vector2.up * moveSpeed * Time.deltaTime, ForceMode2D.Impulse );  
        }
    }

    //When this function is called, it checks if the player is pressing the DownKey.  If they are, then a force in the down direction is added to the rigid body
   void moveDown()
    {
        if (Input.GetKey(DownKey))
        {
			my_rb.AddForce(Vector2.down * moveSpeed * Time.deltaTime, ForceMode2D.Impulse );  
        }
    }
    
    void letGo() //A function to competely stop the player if the down or up key is released
    {
        if (Input.GetKeyUp(DownKey) || Input.GetKeyUp(UpKey)) //If the down or up key is let go, then the linear velocity of the rigid body is set to 0 in both the x and y axis.
        {
            my_rb.linearVelocity = new Vector2(0, 0);
        }
    }
    
    //When this function is called, it checks for if the space key is being pressed and alreadyPunching is false
    //If so then the punch is summoned to be on top of the player but 0.5f to the right and alreadyPunching is set to true
    public void summonPunch()
    {
        if (Input.GetKey(SpaceKey) && alreadyPunching == false)
        {
			punch.transform.position = new Vector2 (collisionPad.transform.position.x + 0.5f, collisionPad.transform.position.y);
            alreadyPunching = true;
        }
    }
    //When this function is called, it sets to the game objects local scale to a new Vector2 that consists of 2 in the x and 2 in the y
    //The rigid body's gravity scale is also set to 0
    public void normal()
    {
        gameObject.transform.localScale = new Vector2(2, 2);
        my_rb.gravityScale = 0;
    }
    //When this function is called, it sets to the game objects local scale to a new Vector2 that consists of 4 in the x and 4 in the y
    //The rigid body's gravity scale is also set to 3
    public void giantCurse()
    {
        gameObject.transform.localScale = new Vector2(4, 4);
        my_rb.gravityScale = 3;
    }
}
