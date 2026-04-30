using UnityEngine;

public class Paddle_Collision_Handler : MonoBehaviour
{
	public Rigidbody2D my_rb; //Public Rigidbody2D variable called my_rb (I sure love reusing that name huh)
    public Animator animations; //Public Animator variable called animations (I sure love also reusing that name huh)
    private bool hit = false; //Private bool called hit, set to false 
    public static bool stunned = false; //Private static bool called stunned, set to false
    private float elapsedHitTime = 0f; //Private float called elapsedHitTime, set to 0f
    public KeyCode punch; //Pubblic keycode called punch, will be used to... punch!
    private int randomCurse; //private int called randomCurse
    public AudioSource[] set_source = new AudioSource[2]; //Creating an AudioSource array called set_source, giving it 2 possible slots
    public static AudioSource[] source = new AudioSource[2]; //Creating an AudioSource array called source, giving it 2 possible slots

    [SerializeField] AudioSource audioSource;

    void Awake()
    //While the game loads, each array number in source is set to the set_sources equivalent
    {
        source[0] = set_source[0];
        source[1] = set_source[1];
    }

    void Update()
    {
        //If the GameManagers active curse is punch and the paddle scripts alreadyPunching variabe is false AND the punch button is being pressed
        //The Paddle-Punch animation is played
        if (GameManager.activeCurse == "punch" && Paddle.alreadyPunching == false && Input.GetKeyDown(punch))
        {
            animations.Play("Paddle-Punch");
        }
        //If hit is set to true, the elapsedHitTime is equal to itself plus delta time
        if (hit == true)
        {
            elapsedHitTime += Time.deltaTime;
        }
        //If the elapsedPunchTime is greater or equal to punchTime (both from the paddle script) OR elapsedHitTime is greater or equal to punchTime
        //The Paddle-Not-Hit aniamtion is played, hit is set to false, and elapsedHitTime is set to 0f
        //Even though punchTime wasn't intended for elapsedHitTime, it ended up working perfectly so I just reused the time from that
        if (Paddle.elapsedPunchTime >=  Paddle.punchTime || elapsedHitTime >= Paddle.punchTime)
        {
            animations.Play("Paddle-Not-Hit");
            hit = false;
            elapsedHitTime = 0f;
        }
        
    }
        void OnTriggerEnter2D(Collider2D other)
    {
        //When entering a trigger object with the tag "Ball", the Paddle-Hit animation is played, the first audio clip in the array is played, and hit is set to true

        if (other.gameObject.tag == "Ball")
        {
            animations.Play("Paddle-Hit");
            source[0].Play();
            hit = true;
        }
        //When entering a trigger object with the tag "Sword", the second audio clip in the array is played and stunned is set to true
        if (other.gameObject.tag == "Sword")
        {
            source[1].Play();
            stunned = true;
        }
        //When entering a trigger object with the tag "Monkeys Paw", randomCurse is set to a random number from 0 to 3 (though with how C# is 3 isn't a possible number so really its 0 to 2!)
        if (other.gameObject.tag == "Monkeys Paw")
        {
             randomCurse = Random.Range(0, 3);
            //If randomCurse is 0 then the activeCurse in GameManager is set to giant
            //Else if its 1 then activeCurse is set to clone
            //Else if its 2 then activeCurse is set to punch
                if (randomCurse == 0)
                {
                    GameManager.activeCurse = "giant";
                }
                else if (randomCurse == 1)
                {
                    GameManager.activeCurse = "clone";
                }
                else if (randomCurse == 2)
                {
                    GameManager.activeCurse = "punch";
                }
        }
    }
}
