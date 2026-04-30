using UnityEngine;
using Pathfinding;
public class Monkeys_Paw_Movement : MonoBehaviour
{
    //IMPORTANT!!!!!
    //This code is reused from the AI and pathfinding lecture
    //I do not fully understand how this works but I did my best to understand it

    //Setting up numerous variables that will be used in the code
    public Transform target;

    public float speed = 20.0f;
    public float nextWaypointDistance = 1f;

    private Path path;
    private int currentWayPoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;
    public Animator animations;
    private bool active = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //On start, the seeker is set to the seeker component
    //rb is set to the rigidbody2D component
    //The seekers starting path is set to the rigid body's position, the targets position, and the OnPathComplelte functions result
    //InvokeRepeating is called, set to UpdatePath, 0f, and 0.5f
    //The Monkeys Paw idle aniamtion is played
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        seeker.StartPath(rb.position, target.position, OnPathComplete);
        InvokeRepeating ("UpdatePath", 0f, .5f);
        animations.Play("MonkeyPaw-Idle");
    }
//If the seeker has reached its spot, then a new path is created using the same method in the start function
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    //Whn the path is complete, if the path is not an error, then path is set to p and currentWayPoint is set to 0
    private void OnPathComplete(Path p)
    {
        if ( !p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
    //On FixedUpdate, if path is null then the update is returned
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }
    //If currentWayPoint is greater or equal to the paths vectorPath count, then reachedEndOfPath is true and the update is returned
        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
    //Else, reachedEndOfPath is set to false
        else
        {
            reachedEndOfPath = false;
        }
    //I am going to be honest, I am not sure what is happening with the Vector2's direction here
    //A Vector2 force is set to the direction multiplied by speed and delta time
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
    //This force is added to the rigid body
        rb.AddForce(force);
    //I am also not sure what is happening in this line of code, but it seems we're setting a float called distance to a distance using the rb's position and paths vectorPath?
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
    //If distance is less than the nextWaypointDistance, then currentWayPoint is increased by 1
        if (distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }
    }
    //When entering an object tagged with ball and active is false, the monkeys paw moving animation is played
    //Active is set to true and the speed is set to 10000f to make the paw move all around the screen (makes it almost like a wild ghost)
    //If the object is Paddle, then the paw is destroyed
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball" && active == false)
        {
            animations.Play("MonkeysPaw-Move");
            // Debug.Log("AAAAAAAAAA");
            active = true;
            speed = 10000f;
        }
        if (other.gameObject.tag == "Paddle")
        {
            Destroy(this.gameObject);
        }
    }
}
