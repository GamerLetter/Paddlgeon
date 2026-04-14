using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Paddle : MonoBehaviour
{
	public Rigidbody2D my_rb;                  
	public float moveSpeed;                   
	public KeyCode UpKey;                 
    public KeyCode DownKey;
    public KeyCode RightKey;
    public KeyCode LeftKey;
    public int stopSpeed;
    public GameObject triggerPad;
    public GameObject collisionPad;
    public Animator animations;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		my_rb = GetComponent<Rigidbody2D>(); //Gets the rigid body 2D component of the parent object  								

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
        letGo();
        //This peice of code makes it so the position of the pad with trigger collision is the exact same as the position of the pad with normal colission in both the x and y direction.
        triggerPad.transform.position = new Vector2(collisionPad.transform.position.x, collisionPad.transform.position.y);
    }

    void moveUp()
    {
        if (Input.GetKey(UpKey))
        {
			my_rb.AddForce(Vector2.up * moveSpeed * Time.deltaTime, ForceMode2D.Impulse );  
        }
    }

   void moveDown()
    {
        if (Input.GetKey(DownKey))
        {
			my_rb.AddForce(Vector2.down * moveSpeed * Time.deltaTime, ForceMode2D.Impulse );  
        }
    }

    
    void letGo() //A function to competely stop the player if the down or up key is released
    {
        if (Input.GetKeyUp(DownKey) || Input.GetKeyUp(UpKey)) //If the down key is let go, then the linear velocity of the rigid body is set to 0 in both the x and y axis.
        {
            my_rb.linearVelocity = new Vector2(0, 0);
        }
        // if (Input.GetKeyUp(UpKey))//If the up key is let go, then the linear velocity of the rigid body is set to 0 in both the x and y axis.
        // {
        //     my_rb.linearVelocity = new Vector2(0, 0);
        // }
    }
    //  void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.tag == "Ball")
    //     {
    //         animator.Play("Paddle-Hit");
    //     }
    // }
}
