using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ball : MonoBehaviour

//NOTE I am not too familar with coding randomized movement besides basic enemy left and right movement.
//So I had to look up a tutorial to help me with the ball movement.
//I do understand how all of this code works and made sure to comment everything, hopefully that is okay!
{
    [SerializeField] AudioSource audioSource;
    public float speed; //Public float variable calleed speed, this will be used to control the speed of the ball
    private float notPunchedSpeed;
    private bool slimed = false;
    public Rigidbody2D ball_rb; //Public rigidbody2d variable called ball_rb, this will be used to get the balls rigid body
    public int score;
    public static int setScoreText;
    private float randomness;
    private int randomY;
    public Animator ball_animator;
    // public Animator bg_animator;
    public static bool clearCheck;
    AudioSource source;
    Collider2D soundTrigger;

    Vector2 direction; //Vector2 variable called direction, this will be used to manage the direction of the ball

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Awake()
    {
        source = GetComponent<AudioSource>();
        soundTrigger = GetComponent<Collider2D>();
    }
    
    void Start()
    {
        ball_rb = GetComponent<Rigidbody2D>(); //Gets the balls rigid body and attaches it to the variable
        direction = new Vector2(1, 1); //Sets the direction of the ball to be (1,1) meaning the x direction = 1 and the y direction  = 1
        notPunchedSpeed = speed;
    }

    void Update()
    {
        setScoreText = score;
        if (speed != notPunchedSpeed + 150f)
        {
            notPunchedSpeed = speed;
            Debug.Log(notPunchedSpeed);
        }
    }

    private void FixedUpdate()
    {
        ball_rb.linearVelocity = direction * speed * Time.deltaTime; //This controls the movement of the ball using its velocity, this is done by multiplying the speed float variable by the vector2 direction variable
        // Debug.Log(direction); //Sends a statement ot the debug log of the current direction of the ball

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
            randomY = Random.Range(0, 2);
            speed = notPunchedSpeed;
            if (randomY == 0)
            {
                direction.y = -direction.y;
            }
            else 
            {
                direction.y = direction.y;
            }
            direction.x = -direction.x;
            source.Play();
            if (slimed == true)
            {
                speed = speed + 150f;
                slimed = false;
                notPunchedSpeed = speed;
            }
        }
        else if (other.gameObject.tag == "Punch")
        {
            direction.y = 0;
            direction.x = -direction.x;
            speed = notPunchedSpeed + 150f;
        }
        else if (other.gameObject.tag == "Slime")
        {
            speed = speed - 150f;
            slimed = true;
        }
        else if (other.gameObject.tag == "Vert_Wall")
        {
            direction.x = -direction.x;
            source.Play(); 
        }

        if (other.gameObject.tag == "weakSpot_Check")
        {
            clearCheck = true;
        }
        if (other.gameObject.tag == "Score_Wall") //If the object it detects has the tag Score_Wall, the score variable is added by 1 AND it is sent to the debug log
        {
            while (direction.y == 0)
            {
                randomY = Random.Range(0, 2);
                if (randomY == 0)
                    {
                        direction.y = 1;
                    }
                else 
                    {
                        direction.y = -1;
                    }
            }
            RandomDirection();
            direction.y = -direction.y;
            direction.x = -direction.x;
            source.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "weakSpot_Check")
        {
            clearCheck = false;
        }

    }

    private void RandomDirection()
    {
           randomness = Random.value; //A random value is obtained here
            
            direction.y = -direction.y; //The y axis of the direction is set to equal the same thing but negative (if it's already negative, it makes it positive)
            if (randomness > 0.5 && direction.y !> 4) //If the random value is greater than 0.5 and the direction.y isn't grreater than 4, an additional value is added to the direction to increase its speed
            {
                direction.y = direction.y + 1;
            }
            else if (randomness < 0.5 && direction.y !< -4)//If the random value is less than 0.5 and the direction.y isn't less than 4, an additional value is added to the direction to increase its speed
            {
                direction.y = direction.y - 1;
            }
    }

}
