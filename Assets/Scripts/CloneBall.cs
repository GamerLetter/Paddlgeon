using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloneBall : MonoBehaviour

//NOTE I am not too familar with coding randomized movement besides basic enemy left and right movement.
//So I had to look up a tutorial to help me with the ball movement.
//I do understand how all of this code works and made sure to comment everything, hopefully that is okay!
{
    [SerializeField] AudioSource audioSource;
    public float speed; //Public float variable calleed speed, this will be used to control the speed of the ball
    private float notPunchedSpeed; //Public float called notPunchedSpeed, used to get the speed when the ball isn't punched
    private bool slimed = false;//Bool that checks if the ball has touched the slime enemy
    public Rigidbody2D ball_rb; //Public rigidbody2d variable called ball_rb, this will be used to get the balls rigid body
    public int score;//Score, kept by the ball
    private float randomness; //Float that contains a random value
    private int randomY;//Int that will contain a random y value
    private float spriteChangeTime = 3f;//Float called spriteChangeTime which is set to 3f
    private float elapsedTime = 0f;//Float called elapsedTime which is set to 0f
    public Animator ball_animator; //The balls animator
    public static bool clearCheck;//Checks if the chests have been cleared
    AudioSource source;//AudioSource variable called source
    Collider2D soundTrigger; //Collider2D variable called soundtrigger
    public SpriteRenderer sprite; //A SpriteRenderer called sprite

    Vector2 direction; //Vector2 variable called direction, this will be used to manage the direction of the ball

    //On awake, the source is set to the balls audiosource and soundtrigger is set to the balls collider2D
    void Awake()
    {
        source = GetComponent<AudioSource>();
        soundTrigger = GetComponent<Collider2D>();
    }
    //On start the balls rigid body is obtained
    //Direction is set to a new vector2 of 1 for the x and 1 for the y
    //notPunchedSpeed is set to the value of speed  
    void Start()
    {
        ball_rb = GetComponent<Rigidbody2D>(); //Gets the balls rigid body and attaches it to the variable
        direction = new Vector2(1, 1); //Sets the direction of the ball to be (1,1) meaning the x direction = 1 and the y direction  = 1
        notPunchedSpeed = speed;
    }

    //On Update, If speed is not equal to notPunchedSpeed + 150f, then notPunchedSpeed is set to speed and its value is sent to the Debug.Log
    //If the GameManager scripts activeCurse variable is set to "clone", then the elapsedTime is set to itself + delta time
    //If elapsedTime is greater or equal to spriteChangeTime, the sprites color is set to white
    //If GameManager scripts activeCruse variable is not equal to "clone" then the object attached to this is destroyed
    void Update()
    {
        if (GameManager.activeCurse == "clone")
        {
            elapsedTime += Time.deltaTime;
        }
        if (elapsedTime >= spriteChangeTime)
        {
            sprite.color = new Color (1, 1, 1, 1);
        }
        if (GameManager.activeCurse != "clone")
        {
            Destroy(this.gameObject);
        }
        if (speed != notPunchedSpeed + 150f)
        {
            notPunchedSpeed = speed;
            Debug.Log(notPunchedSpeed);
        }
    }
    private void FixedUpdate()
    {
        ball_rb.linearVelocity = direction * speed * Time.deltaTime; //This controls the movement of the ball using its velocity, this is done by multiplying the speed float variable by the vector2 direction variable
    }
    

    void OnTriggerEnter2D(Collider2D other) //A funtion that checks to see if the ball enters trigger areas
    {
        if (other.gameObject.tag == "Hor_Wall") //If the ball enters a trigger area with the tag wall their direction will change
        {
            direction.y = -direction.y;
            source.Play();
        }
        else if (other.gameObject.tag == "Paddle")//If the object it detects has the tag Paddle, the x axis of the direction is set to its current value but negative
        {
            randomY = Random.Range(0, 2);//A randomY value is determined from a range of 0 to 1
            //If randomY is 0, then direction.y is set to the negative value of it
            //Else randomY stays the same
            if (randomY == 0)
            {
                direction.y = -direction.y;
            }
            else 
            {
                direction.y = direction.y;
            }
            direction.x = -direction.x;// the x axis of the direction is set to its current value but negative
            source.Play();//Plays the audiosource from the source variable
            if (slimed == true) //If the value slimed is true then speed is to speed + 150f, slimed is set to false, and notPunchedSpeed is set to speed
            {
                speed = speed + 150f;
                slimed = false;
                notPunchedSpeed = speed;
            }
        }
        //If the object has the tage Punch then direction.y is set to 0, direction x is set to a negative value of itself
        //speed is also set to notPunchedSpeed + 150f, this is done as a trade off for more precise ball movement from punching
        //The clone ball doesn't interact with the punch, but for the sake of something going wrong that I'm unaaware of this is kept in
        else if (other.gameObject.tag == "Punch")
        {
            direction.y = 0;
            direction.x = -direction.x;
            speed = notPunchedSpeed + 150f;
        }
        //Else if the objects tag is Slime then the speed is set to speed -150f and slimed is set to true, this slows down the balls speed
        else if (other.gameObject.tag == "Slime")
        {
            speed = speed - 150f;
            slimed = true;
        }
        //Else if the objects tag is Vert_Wall (short for vertical wall), then direction.x is set to its value but negative and the audio source is played 
        else if (other.gameObject.tag == "Vert_Wall")
        {
            direction.x = -direction.x;
            source.Play(); 
        }
        //If the other object has the weakSpot_Check tag, then clearCheck is set to true
        if (other.gameObject.tag == "weakSpot_Check")
        {
            clearCheck = true;
        }
        //If the other object is Score_Wall, 
        //RandomDirection function is called
        //direction.y is set to itself but negative
        //direction.x is set to itself but negative
        //The source variable is played 
        if (other.gameObject.tag == "Score_Wall")
        {
            RandomDirection();
            direction.y = -direction.y;
            direction.x = -direction.x;
            source.Play();
        }
    }
    //On exiting an object with the weakSpot_Check tag, the clearCheck is set to false

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "weakSpot_Check")
        {
            clearCheck = false;
        }

    }
    //Function called RandomDirection, handles the balls randomness

    private void RandomDirection()
    {
           randomness = Random.value; //A random value is obtained here
            
            direction.y = -direction.y; //The y axis of the direction is set to equal the same thing but negative (if it's already negative, it makes it positive)
            if (randomness > 0.5 && direction.y !> 4) //If the random value is greater than 0.5 and the direction.y isn't grreater than 4, an additional value is added to the direction to change its speed
            {
                direction.y = direction.y + 1;
            }
            else if (randomness < 0.5 && direction.y !< -4)//If the random value is less than 0.5 and the direction.y isn't less than 4, an additional value is subtracted to the direction to changed its speed
            {
                direction.y = direction.y - 1;
            }
    }

}
