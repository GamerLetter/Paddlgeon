using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public TMP_Text current_score;  //Establishing a text variable for the text list
   public GameObject ball;
   private int score;
   private int next_paw_score;
   private int next_speed_score = 5;
   public int max_speed;
   public static string activeCurse;
   public GameObject hor_weakSpot;
   public GameObject vert_weakSpot;
   public GameObject weakSpot_Handler;
   public GameObject monkeyspaw;
   public GameObject monkeyspaw_Handler;
   private bool monkeyspaw_Destroyed = true;
   private bool monkeyspaw_Spawned = false;


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
        
    }

    // Update is called once per frame
    void Update()
    {
        score = ball.GetComponent<Ball>().score;
        current_score.text = "Score: " + score.ToString();
        // if (score % 10 == 0 && audio_check == 0)
        // {
        //     source.Play();
        //     audio_check = 1;
        // }
        // if (score % 10 != 0)
        // {
        //     audio_check = 0;
        // }
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
        if (monkeyspaw_Handler.transform.childCount == 0 && monkeyspaw_Destroyed == true && Paddle.currentlyCursed == false)
            {
                next_paw_score = score + Random.Range(2, 4);
                monkeyspaw_Destroyed = false;
                monkeyspaw_Spawned = false;
                Debug.Log(next_paw_score);
            }
        if (next_paw_score == score && monkeyspaw_Spawned == false && Paddle.currentlyCursed == false)
            {
                Instantiate(monkeyspaw, new Vector2(4.44f, 0f), Quaternion.identity, monkeyspaw_Handler.transform);
                monkeyspaw_Spawned = true;
                monkeyspaw_Destroyed = true;
            }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D  other) //This code is ran when the object its attached to enters another trigger object
    {
        if (other.gameObject.tag == "Ball") //Checks to see if the object that it entered has the tag Ball
        {
        SceneManager.LoadScene("SampleScene");//If the tag is Ball, it reloads the game scene.
        }
    }

    void vertWeakSpotSpawner()
            {
                positionDecider = Random.Range(0, 5);
                Debug.Log(positionDecider);
                float fixedHorizontalPlacement = 11.75f;
                if (positionDecider == 0)
                {
                    Instantiate(vert_weakSpot, new Vector2(fixedHorizontalPlacement, 1), Quaternion.identity, weakSpot_Handler.transform);
                }
                else if (positionDecider == 1)
                {
                    Instantiate(vert_weakSpot, new Vector2(fixedHorizontalPlacement, 3), Quaternion.identity, weakSpot_Handler.transform);
                }
                else if (positionDecider == 2)
                {
                    Instantiate(vert_weakSpot, new Vector2(fixedHorizontalPlacement, 5), Quaternion.identity, weakSpot_Handler.transform);
                }
                else if (positionDecider == 3)
                {
                    Instantiate(vert_weakSpot, new Vector2(fixedHorizontalPlacement, 7), Quaternion.identity, weakSpot_Handler.transform);
                }
                else if (positionDecider == 4)
                {
                    Instantiate(vert_weakSpot, new Vector2(fixedHorizontalPlacement, 9), Quaternion.identity, weakSpot_Handler.transform);
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
