using UnityEngine;

public class Paddle_Collision_Handler : MonoBehaviour
{
	public Rigidbody2D my_rb;                  
    public Animator animations;
    private bool hit = false;
    public static bool stunned = false;
    private float elapsedHitTime = 0f;
    public KeyCode punch;
    private int randomCurse;
    public AudioSource[] set_source = new AudioSource[2];
    public static AudioSource[] source = new AudioSource[2];

    [SerializeField] AudioSource audioSource;

    void Awake()
    //While the game loads, each array number is set to the set_sources equivalent
    {
        source[0] = set_source[0];
        source[1] = set_source[1];
    }

    void Update()
    {
        if (GameManager.activeCurse == "punch" && Paddle.alreadyPunching == false && Input.GetKeyDown(punch))
        {
            animations.Play("Paddle-Punch");
        }
        if (hit == true)
        {
            elapsedHitTime += Time.deltaTime;
        }
        if (Paddle.elapsedPunchTime >=  Paddle.punchTime || elapsedHitTime >= Paddle.punchTime)
        {
            animations.Play("Paddle-Not-Hit");
            hit = false;
            elapsedHitTime = 0f;
        }
        
    }
        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            animations.Play("Paddle-Hit");
            source[0].Play();
            hit = true;
        }
        if (other.gameObject.tag == "Sword")
        {
            source[1].Play();
            stunned = true;
        }
        if (other.gameObject.tag == "Monkeys Paw")
        {
             randomCurse = Random.Range(0, 3);
            //  Debug.Log(randomCurse);
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
