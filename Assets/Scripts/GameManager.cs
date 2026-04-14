using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public TMP_Text current_score;  //Establishing a text variable for the text list
   public GameObject ball;
   public int score;
   public GameObject hor_weakSpot;
   public GameObject vert_weakSpot;
   public GameObject weakSpot_Handler;
   public GameObject monkeyspaw;
   public GameObject monkeyspawManager;

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
        if (score == 3 && ball.GetComponent<Ball>().speed == 500)
        {
            ball.GetComponent<Ball>().speed = ball.GetComponent<Ball>().speed * 1.2f;
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
