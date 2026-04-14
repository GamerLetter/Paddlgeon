using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
   public TMP_Text current_score;  //Establishing a text variable for the text list
   public GameObject ball_score;
   AudioSource source;
   private int audio_check;

    [SerializeField] AudioSource audioSource;

    void Awake()
    {
        source = GetComponent<AudioSource>();

    }
    void Start()
    {

        current_score.text = "Score: 0";
        
    }

    // Update is called once per frame
    void Update()
    {
        current_score.text = "Score: " + ball_score.GetComponent<Ball>().score.ToString();
        if (ball_score.GetComponent<Ball>().score % 10 == 0 && audio_check == 0)
        {
            source.Play();
            audio_check = 1;
        }
        if (ball_score.GetComponent<Ball>().score % 10 != 0)
        {
            audio_check = 0;
        }
    }

}
