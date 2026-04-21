using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public TMP_Text current_score;  //Establishing a text variable for the text list
   public TMP_Text currentLives;
   public GameObject ball;
   public GameObject cloneBall;
   public GameObject paddle;
   public int lives;
   private int score;
   private int next_paw_score;
   public int next_speed_score = 5;
   public int max_speed;
   public GameObject hor_weakSpot;
   public GameObject vert_weakSpot;
   public GameObject weakSpot_Handler;
   public GameObject cloneBallHandler;
   public GameObject monkeyspaw;
   public GameObject monkeyspaw_Handler;
   private bool monkeyspaw_Destroyed = true;
   private bool monkeyspaw_Spawned = false;
   private bool cloneBallSpawned = false;
   public static string activeCurse;
   public static bool currentlyCursed;
   public static float curseTime = 10f;
   public static float elapsedTime;     
   private int positionDecider;
   AudioSource source;
   private int audio_check;

    // [SerializeField] AudioSource audioSource;

    // void Awake()
    // {
    //     source = GetComponent<AudioSource>();

    // }
    void Start()
    {
        current_score.text = "Score: 0";
        currentLives.text = "Lives: " + lives;
        score = 0;
        elapsedTime = 0f;
        next_paw_score = score + Random.Range(2, 5);
        activeCurse = "N/A";
    }

    // Update is called once per frame
    void Update()
    {
        score = ball.GetComponent<Ball>().score;
        current_score.text = "Score: " + score.ToString();
        if (weakSpot_Handler.transform.childCount == 0 && Ball.clearCheck == true)
        {
            ball.GetComponent<Ball>().score = ball.GetComponent<Ball>().score + 1;
           for (int i = 0; i < 2; i++)
            {
                    Debug.Log(positionDecider);
                    vertWeakSpotSpawner();
            }
        }
        if (score == next_speed_score && ball.GetComponent<Ball>().speed != max_speed)
        {
            ball.GetComponent<Ball>().speed = ball.GetComponent<Ball>().speed * 1.1f;
            next_speed_score = next_speed_score + 5;
        }
        if (monkeyspaw_Handler.transform.childCount == 0 && monkeyspaw_Destroyed == true && currentlyCursed == false)
            {
                next_paw_score = score + Random.Range(2, 5);
                monkeyspaw_Destroyed = false;
                monkeyspaw_Spawned = false;
                Debug.Log(next_paw_score);
            }
        if (next_paw_score == score && monkeyspaw_Spawned == false && currentlyCursed == false)
            {
                Instantiate(monkeyspaw, new Vector2(4.44f, 0f), Quaternion.identity, monkeyspaw_Handler.transform);
                monkeyspaw_Spawned = true;
                monkeyspaw_Destroyed = true;
            }
        if (activeCurse == "giant")
            {
                paddle.GetComponent<Paddle>().giantCurse();
                currentlyCursed = true;
            }
        if (activeCurse == "clone" && cloneBallSpawned == false)
            {
                Instantiate(cloneBall, new Vector2(8, 6), Quaternion.identity, cloneBallHandler.transform);
                cloneBallSpawned = true;
                currentlyCursed = true;
            }
        if (activeCurse != "N/A")
            {
                elapsedTime += Time.deltaTime;
            }
        if (elapsedTime >= curseTime)
            {
                activeCurse = "N/A";
                currentlyCursed = false;
                if (cloneBallSpawned == true)
                    {
                    cloneBallSpawned = false;
                    }
                elapsedTime = 0f;
                paddle.GetComponent<Paddle>().normal();
            }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D  other) //This code is ran when the object its attached to enters another trigger object
    {
        if (other.gameObject.tag == "Ball" && lives > 0) //Checks to see if the object that it entered has the tag Ball
        {
            ball.transform.position = new Vector2(4.664f, 2.56f);
            lives -= 1;
            currentLives.text = "Lives: " + lives;
        }
        if (other.gameObject.tag == "Ball" && lives < 1)
        {
            SceneManager.LoadScene("GameScene");//If the tag is Ball, it reloads the game scene.
        }
    }

    void vertWeakSpotSpawner()
            {
                positionDecider = Random.Range(0, 5);
                // Debug.Log(positionDecider);
                float fixedHorizontalPlacement = 11.11f;
                if (positionDecider == 0)
                {
                    Instantiate(vert_weakSpot, new Vector2(fixedHorizontalPlacement, 1), Quaternion.Euler(0, 0, 90), weakSpot_Handler.transform);
                }
                else if (positionDecider == 1)
                {
                    Instantiate(vert_weakSpot, new Vector2(fixedHorizontalPlacement, 3), Quaternion.Euler(0, 0, 90), weakSpot_Handler.transform);
                }
                else if (positionDecider == 2)
                {
                    Instantiate(vert_weakSpot, new Vector2(fixedHorizontalPlacement, 5), Quaternion.Euler(0, 0, 90), weakSpot_Handler.transform);
                }
                else if (positionDecider == 3)
                {
                    Instantiate(vert_weakSpot, new Vector2(fixedHorizontalPlacement, 7), Quaternion.Euler(0, 0, 90), weakSpot_Handler.transform);
                }
                else if (positionDecider == 4)
                {
                    Instantiate(vert_weakSpot, new Vector2(fixedHorizontalPlacement, 9), Quaternion.Euler(0, 0, 90), weakSpot_Handler.transform);
                }
            }
    
    void horWeakSpotSpawner()
    {
            positionDecider = Random.Range(1, 4);
            if (positionDecider == 1)
            {
                Instantiate(hor_weakSpot, new Vector2(1, 9), Quaternion.identity, weakSpot_Handler.transform);
            }
            else if (positionDecider == 2)
            {
                Instantiate(vert_weakSpot, new Vector2(5, 9), Quaternion.identity, weakSpot_Handler.transform);
            }
            else if (positionDecider == 3)
            {
                Instantiate(vert_weakSpot, new Vector2(7, 9), Quaternion.identity, weakSpot_Handler.transform);
            }
    }

}
