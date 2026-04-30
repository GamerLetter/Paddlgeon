using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Setting up an enum called curse, contains Giant, Clone, Punch, and None
public enum curse
{
    Giant,
    Clone,
    Punch,
    None
}
//Setting up an enum called enemy, contains Plunger, Slime, and Sword
public enum enemy
{
    Plunger,
    Slime,
    Sword
}

public class GameManager : MonoBehaviour
{
    //Establishing a LOT of variables that will be used, due to the amount I'll explain as they're used instead unlike the other scripts so its easier to follow
   public TMP_Text current_score; 
   public TMP_Text currentLives;
   public GameObject ball;
   public GameObject cloneBall;
   public GameObject plunger;
   public GameObject sword;
   public GameObject slime;
   public GameObject enemyHandler;
   public GameObject paddle;
   public GameObject curseFilter;
   public GameObject curseIcon;
   public int lives;
   private int score;
   private int next_paw_score;
   public int next_speed_score = 5;
   private int nextEnemyScore;
   public int max_speed;
   private int enemyDecider;
   public GameObject vert_weakSpot;
   public GameObject weakSpot_Handler;
   public GameObject cloneBallHandler;
   public GameObject monkeyspaw;
   public GameObject monkeyspaw_Handler;
   private bool monkeyspaw_Destroyed = true;
   private bool monkeyspaw_Spawned = false;
   private bool enemyDestroyed = true;
   private bool enemySpawned = false;
   private bool firstEnemySpawned = false;
   private bool cloneBallSpawned = false;
   public static string activeCurse;
   public static string activeEnemy;
   public static bool currentlyCursed;
   public static float curseTime = 10f;
   public static float elapsedTime;     
   private int positionDecider;
   AudioSource source;
   private int audio_check;
   public curse curse;
   public enemy enemy;
   private KeyCode restart = KeyCode.R;
   private KeyCode next = KeyCode.N;

    void Start()
    {
        current_score.text = score.ToString();
        currentLives.text = lives.ToString();
        score = 0;
        elapsedTime = 0f;
        next_paw_score = score + Random.Range(1, 5);
        nextEnemyScore = Random.Range(1, 5);
        curse = curse.None;
        curseSwitch();
        activeCurse = "None";
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(curse);
        score = ball.GetComponent<Ball>().score;
        current_score.text = score.ToString();
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
            ball.GetComponent<Ball>().speed = ball.GetComponent<Ball>().notPunchedSpeed;
            ball.GetComponent<Ball>().speed = ball.GetComponent<Ball>().speed * 1.1f;
            next_speed_score = next_speed_score + 5;
        }
        if (monkeyspaw_Handler.transform.childCount == 0 && monkeyspaw_Destroyed == true && currentlyCursed == false && activeCurse == "None")
            {
                next_paw_score = score + Random.Range(1, 4);
                monkeyspaw_Destroyed = false;
                monkeyspaw_Spawned = false;
                Debug.Log("You need this amount of points for another curse: " + next_paw_score);
            }
        if (next_paw_score == score && monkeyspaw_Spawned == false && currentlyCursed == false)
            {
                Instantiate(monkeyspaw, new Vector2(4.44f, 0f), Quaternion.identity, monkeyspaw_Handler.transform);
                monkeyspaw_Spawned = true;
                monkeyspaw_Destroyed = true;
            }

        if (activeCurse == "giant")
            {
                curse = curse.Giant;
                curseSwitch();
            }
        if (activeCurse == "clone" && cloneBallSpawned == false)
            {
                curse = curse.Clone;
                curseSwitch();
            }
        if (activeCurse == "punch")
            {
                curse = curse.Punch;
                curseSwitch();
            }          
        if (curse != curse.None)
            {
                elapsedTime += Time.deltaTime;
                curseFilter.SetActive(true);
            }
        if (elapsedTime >= curseTime)
            {

                curse = curse.None;
                activeCurse = "None";
                curseSwitch();
            }
        if (score >= 15)
        {
            startEnemySpawn();
        }
        if (score >= 30)
        {
            SceneManager.LoadScene("WinScene");//If the score is greater than or equal to 30,than the lose scene is loaded.

        }
        Restart();
        NextScene();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D  other) //This code is ran when the object its attached to enters another trigger object
    {
        if (other.gameObject.tag == "Ball" && lives > 0) //Checks to see if the object that it entered has the tag Ball
        {
            ball.transform.position = new Vector3(4.664f, 2.56f, -1);
            lives -= 1;
            currentLives.text = lives.ToString();
        }
        if (other.gameObject.tag == "Ball" && lives < 1)
        {
            SceneManager.LoadScene("LoseScene");//If the tag is Ball and lives is less than 1, it loads the lose scene.
        }
    }
    
    void startEnemySpawn()
    {
        if (firstEnemySpawned == false)
        {
            Instantiate(plunger, new Vector2(6f, 9.5f), Quaternion.identity, enemyHandler.transform);
            firstEnemySpawned = true;
        }
        if (enemyHandler.transform.childCount == 0 && enemyDestroyed == true)
            {
                nextEnemyScore = score + Random.Range(1, 3);
                enemyDestroyed = false;
                enemySpawned = false;
                Debug.Log("You need this amount of points for another enemy to spawn: " + nextEnemyScore);
            }
        if (nextEnemyScore == score && enemySpawned == false)
            {
                enemyDecider = Random.Range(0, 3);

                if (enemyDecider == 0)
                {
                    enemy = enemy.Plunger;
                    enemySwitch();
                }
              if (enemyDecider == 1)
                {
                    enemy = enemy.Slime;
                    enemySwitch();
                }
              if (enemyDecider == 2)
                {
                    enemy = enemy.Sword;
                    enemySwitch();
                }
                // enemyDestroyed = true;  
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
    void curseSwitch()
        {
            switch(curse)
            {
                case curse.Giant:
                Debug.Log("GIANT");
                paddle.GetComponent<Paddle>().giantCurse();
                currentlyCursed = true;
                curseIcon.GetComponent<Animator>().Play("giantCurse");
                break;
            }
            switch(curse)
            {
                case curse.Clone:
                Debug.Log("CLONE");
                Instantiate(cloneBall, new Vector3(8, 6, -1), Quaternion.identity, cloneBallHandler.transform);
                cloneBallSpawned = true;
                currentlyCursed = true;
                curseIcon.GetComponent<Animator>().Play("cloneCurse");
                break;
            }
            switch(curse)
            {
                case curse.Punch:
                Debug.Log("PUNCH");
                paddle.GetComponent<Paddle>().summonPunch();
                currentlyCursed = true;
                curseIcon.GetComponent<Animator>().Play("punchCurse");
                break;
            }
            switch(curse)
            {
                case curse.None:
                Debug.Log("no curse");
                currentlyCursed = false;
                if (cloneBallSpawned == true)
                    {
                    cloneBallSpawned = false;
                    }
                elapsedTime = 0f;
                paddle.GetComponent<Paddle>().normal();
                curseFilter.SetActive(false);
                curseIcon.GetComponent<Animator>().Play("noCurse");
                break;
            }
        }
    void enemySwitch()
    {
        switch(enemy)
        {
            case enemy.Plunger:
            Instantiate(plunger, new Vector2(6f, 9.5f), Quaternion.identity, enemyHandler.transform);
            enemySpawned = true;
            enemyDestroyed = true;
            break;
        }
    
        switch(enemy)
        {
            case enemy.Slime:
            Instantiate(slime, new Vector2(6f, 9.5f), Quaternion.identity, enemyHandler.transform);
            enemySpawned = true;
            enemyDestroyed = true;
            break;
        }
        switch(enemy)
        {
            case enemy.Sword:
            Instantiate(sword, new Vector2(-12f, 3.5f), Quaternion.identity, enemyHandler.transform);
            enemySpawned = true;
            enemyDestroyed = true;
            break;
        }
    }
    void Restart()
    {
        //If the restart key is hit, then the title scene is loaded
        if (Input.GetKeyDown(restart))
            {
                SceneManager.LoadScene("TitleScene");
            }
    }
    void NextScene()
    {
        //If the next key is hit, then the next scene is loaded
        if (Input.GetKeyDown(next))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
    }
}
