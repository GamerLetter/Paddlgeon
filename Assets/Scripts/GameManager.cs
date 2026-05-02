using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//NOTE I apologize if formatting is kind of wonky, sometimes I use underscores and other times I dont

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
    //Establishing a LOT of variables that will be used
    //Due to the amount I'll explain how they're used throughout the script, that way its easier to follow
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
   private bool monkeyspaw_Active = true;
   private bool monkeyspaw_Spawned = false;
   private bool enemyActive = true;
   private bool enemySpawned = false;
   private bool firstEnemySpawned = false;
   private bool cloneBallSpawned = false;
   public static string activeCurse;
   public static string activeEnemy;
   public static bool currentlyCursed;
   public static float curseTime = 10f;
   public static float elapsedTime;     
   private int positionDecider;
   public curse curse;
   public enemy enemy;
   private KeyCode restart = KeyCode.R;
   private KeyCode next = KeyCode.N;

    void Start()
    {
        //the current score text is set to the score but in string form, this displays the current score
        //the text of current lives is also set to lives but in string form, this is done to display the lives remaining
        current_score.text = score.ToString();
        currentLives.text = lives.ToString();
        //Score is set to 0 on start
        score = 0;
        //elapsedTime is set to 0, this is used for curses
        elapsedTime = 0f;
        //next paw score is set to current score plus a random number from 1 to 4, this is used to randomize when monkey paws spawn
        //next enemy score is set to current score plus a random number from 1 to 5, this is used to randomize when enemies spawn
        next_paw_score = score + Random.Range(1, 4);
        nextEnemyScore = Random.Range(1, 5);
        //curse is set to the variable none from the curse enum
        //curseSwitch is activated
        curse = curse.None;
        curseSwitch();
        //activeCurse is set to None
        activeCurse = "None";
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(curse);
        //The score is set to the score variable of the ball, this is done to keep track of the score
        //(When this was being prototyped in January it was easier to have the ball handle the score, I decided to keep it the same for times sake)
        score = ball.GetComponent<Ball>().score;
        //The current score text is set to the score but in string form
        current_score.text = score.ToString();
        //If the weak spot handler has no cildren and the clear check from the Ball script is true
        //Then the balls score is set to itself + 1
        //For as long as i is less than 2, the vertical weak spot spawner is ran
        if (weakSpot_Handler.transform.childCount == 0 && Ball.clearCheck == true)
        {
            ball.GetComponent<Ball>().score = ball.GetComponent<Ball>().score + 1;
           for (int i = 0; i < 2; i++)
            {
                    // Debug.Log(positionDecider);
                    //(The reason its specified as vertical is because I did want chests to spawn on the ceiling after 10 points
                    //but due to time, this feature had to be cut)
                    vertWeakSpotSpawner();
            }
        }
        //If the score is set to the next speed socre and the balls component isnt equal to the maximum allowed speed,
        //Then the balls speed is set to the notPunchedSpeed and the speed is multiplied by 1.1f
        //Next speed score is set to itself + 5
        //This allows the speed to increase every 5 points, making the game harder
        if (score == next_speed_score && ball.GetComponent<Ball>().speed != max_speed)
        {
            ball.GetComponent<Ball>().speed = ball.GetComponent<Ball>().notPunchedSpeed;
            ball.GetComponent<Ball>().speed = ball.GetComponent<Ball>().speed * 1.1f;
            next_speed_score = next_speed_score + 5;
        }
        //If the monkeys paw handle has no childrena and the monkeys paw active is true and currently cursed is false AND active curse is set to None
        //Then the next paw score is set to score + a random range of 1 to 4, monkeys paw active and monkeys paw spawned are both set to false
        if (monkeyspaw_Handler.transform.childCount == 0 && monkeyspaw_Active == true && currentlyCursed == false && activeCurse == "None")
            {
                next_paw_score = score + Random.Range(1, 4);
                monkeyspaw_Spawned = false;
                monkeyspaw_Active = false;
                // Debug.Log("You need this amount of points for another curse: " + next_paw_score);
            }
        //If the next paw score is equal to score and the monkeys paw spawned varable is false and currently cursed is set to false
        //Then a monkeys paw is spawned and set to be in the monkeys paw handlerr
        //Monkeys paw active and monkeys paw spawned are both set to true
        if (next_paw_score == score && monkeyspaw_Spawned == false && currentlyCursed == false)
            {
                Instantiate(monkeyspaw, new Vector2(4.44f, 0f), Quaternion.identity, monkeyspaw_Handler.transform);
                monkeyspaw_Spawned = true;
                monkeyspaw_Active = true;
            }
        //If the active curse is giant, then curse is set to the Giant variable of the curse enum
        //The curseSwitch function is ran
        if (activeCurse == "giant")
            {
                curse = curse.Giant;
                curseSwitch();
            }
        //If the active curse is clone and the clone ball spawned is false, then curse is set to the Clone variable of the curse enum
        //The curseSwitch function is ran
        if (activeCurse == "clone" && cloneBallSpawned == false)
            {
                curse = curse.Clone;
                curseSwitch();
            }
        //If the active curse is punch, then curse is set to the Punch variable of the curse enum
        //The curseSwitch function is ran
        if (activeCurse == "punch")
            {
                curse = curse.Punch;
                curseSwitch();
            } 
        //If the active curse is not none, then elapsed time is set to itself + delta time to keep track of how long the player has been cursed
        //The curseFilter is also set to be active, this is what creates the purple affect when cursed
        if (curse != curse.None)
            {
                elapsedTime += Time.deltaTime;
                curseFilter.SetActive(true);
            }
        //If the elapsed time is greater than or equal to the curse time (the maximum time one can be cursed for)
        //Then curse is set to the None variable of the curse enum and the curseSwitch functinon is ran
        if (elapsedTime >= curseTime)
            {

                curse = curse.None;
                activeCurse = "None";
                curseSwitch();
            }
        //If the score is greater than or equal to 15, the startEnemySpawn function is ran
        if (score >= 15)
        {
            startEnemySpawn();
        }
        //If score is greater than or equal to 30, then the win screen is loaded
        if (score >= 30)
        {
            SceneManager.LoadScene("WinScene");

        }
        //The restart and nextscene functions are constantly ran to allow for moving to the next scene or resetting the game
        Restart();
        NextScene();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D  other) //This code is ran when the object its attached to enters another trigger object
    {
        if (other.gameObject.tag == "Ball" && lives > 0) //Checks to see if the object that it entered has the tag Ball and lives is greater than 0
        {
            //If its true then the ball is set back to be on screen and lives is set to iself - 1 and the current lives text is set to lives but in string form
            ball.transform.position = new Vector3(4.664f, 2.56f, -1);
            lives -= 1;
            currentLives.text = lives.ToString();
        }
        //If the other object is Bal and lives is less than 1, then the lose scene is loaded
        if (other.gameObject.tag == "Ball" && lives < 1)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
    
    //Starts the enemy spawning event midway through the game
    void startEnemySpawn()
    {
        //If the first enemy spawned variable is false, then a plunger is spawned in the enemy handler and first enemy spawned is set to true
        //This makes it so the first enemy is always a plunger, the enemy that affects the least amount
        if (firstEnemySpawned == false)
        {
            Instantiate(plunger, new Vector2(6f, 9.5f), Quaternion.identity, enemyHandler.transform);
            firstEnemySpawned = true;
        }
        //If the enemy handler has no children and enemyActive is true
        //Then he next enemy score is set to scorre + a random number from 1 to 3
        //Enemy active and enemy spawned is set to false to prevent the enemy from spawning
        if (enemyHandler.transform.childCount == 0 && enemyActive == true)
            {
                nextEnemyScore = score + Random.Range(1, 3);
                enemyActive = false;
                enemySpawned = false;
                // Debug.Log("You need this amount of points for another enemy to spawn: " + nextEnemyScore);
            }
        //If the next enemy score is equal to score and enemy spawned i false
        //Then a random enemy is decided by choosing a random number from 0 to 3
        if (nextEnemyScore == score && enemySpawned == false)
            {
                enemyDecider = Random.Range(0, 3);
            //If the number is 0, then the enemy is set to Plunger and enemySwitch function is ran
                if (enemyDecider == 0)
                {
                    enemy = enemy.Plunger;
                    enemySwitch();
                }
            //If the number is 1, then the enemy is set to Slime and enemySwitch function is ran
              if (enemyDecider == 1)
                {
                    enemy = enemy.Slime;
                    enemySwitch();
                }
            //If the number is 2, then the enemy is set to Sword and enemySwitch function is ran
              if (enemyDecider == 2)
                {
                    enemy = enemy.Sword;
                    enemySwitch();
                }
                // enemyDestroyed = true;  
            }
    }

    //This handles spawning the chests, originally chests were going to be weak spots on the wall that when hit would destroy the wall 
    //This changed mid-development, but weak spots worked well as a code name so I kept it in which also helped to save time
    void vertWeakSpotSpawner()
            {
                //The position decider is set to a random number from 0 to 5, this helps to decide where the weak spots (chests) will spawn
                positionDecider = Random.Range(0, 5);
                // Debug.Log(positionDecider);
                //A float called fixed horizontal placement is set to 11.11f, this makes it so they always spawn on the wall
                float fixedHorizontalPlacement = 11.11f;
                //For each instance here it checks if the position decier is set to a number from 0 up to 4
                //If its 0 then vertical position will be 1
                //If its 1 then vertical position will be 3
                //If its 2 then vertical position will be 5
                //If its 3 then vertical position will be 7
                //If its 4 then vertical position will be 9
                //For all of these its set to have a rotation of 90 in the z axis, the horizontal position is set to the fixed horizontal placement, and they're spawned in the weak spot handler
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
    //This handles switching the curses depending on what case curse is set to
    void curseSwitch()
        {
            //This code checks if the case is Giant and if so the paddles giant function is started
            //currentlyCursed is set to true so no more monkey paws spawn, the curse icon is set to the animation giantCurse, and the code ends
            //The curse icon variable is what handles what current curse you're shown to have
            switch(curse)
            {
                case curse.Giant:
                // Debug.Log("GIANT");
                paddle.GetComponent<Paddle>().giantCurse();
                currentlyCursed = true;
                curseIcon.GetComponent<Animator>().Play("giantCurse");
                break;
            }
            //This code checks if the case is Clone and if so it spawns in a clone ball inthe cloneBallHandler
            //cloneBallSpawened and currentlyCursed is set to true so no more monkey paws spawn or balls spawn
            // the curse icon is set to the animation giantCurse and the code ends
            switch(curse)
            {
                case curse.Clone:
                // Debug.Log("CLONE");
                Instantiate(cloneBall, new Vector3(8, 6, -1), Quaternion.identity, cloneBallHandler.transform);
                cloneBallSpawned = true;
                currentlyCursed = true;
                curseIcon.GetComponent<Animator>().Play("cloneCurse");
                break;
            }
            //This code checks if the case is Punch and if so the paddles summonPunch function is played
            //currentlyCursed is set to true so no more monkey paws spawn, the curse icon is set to the animation punchCurse, and the code ends
            switch(curse)
            {
                case curse.Punch:
                // Debug.Log("PUNCH");
                paddle.GetComponent<Paddle>().summonPunch();
                currentlyCursed = true;
                curseIcon.GetComponent<Animator>().Play("punchCurse");
                break;
            }
            //Switching  here checks if the case is None and if so then a lot of variables are set back to their previous state
            //currentlyCursed, cloneBallSpawned (if set to true), and he curse filter are all set to false
            //Elapsed time is set to 0f so the timer has been reset for the next curse
            //The filter is set false so the game looks normal again and the curseIcon plays the noCurse animation, then the code ends
            switch(curse)
            {
                case curse.None:
                // Debug.Log("no curse");
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
        //This function handles spawning in enemies depending on the case
    void enemySwitch()
    {
        //This code checks if the case is Plunger and if so a plunger enemy is spawned in the enemyHandler
        //The variables enemySpawned and enemyActive are both set to true and the code ends
        switch(enemy)
        {
            case enemy.Plunger:
            Instantiate(plunger, new Vector2(6f, 9.5f), Quaternion.identity, enemyHandler.transform);
            enemySpawned = true;
            enemyActive = true;
            break;
        }
        //This code checks if the case is Slime and if so a slime enemy is spawned in the enemyHandler
        //The variables enemySpawned and enemyActive are both set to true and the code ends
        switch(enemy)
        {
            case enemy.Slime:
            Instantiate(slime, new Vector2(6f, 9.5f), Quaternion.identity, enemyHandler.transform);
            enemySpawned = true;
            enemyActive = true;
            break;
        }
        //This code checks if the case is Sword and if so a sword enemy is spawned in the enemyHandler
        //The variables enemySpawened and enemyActive are both set to true and the code ends
        switch(enemy)
        {
            case enemy.Sword:
            Instantiate(sword, new Vector2(-12f, 3.5f), Quaternion.identity, enemyHandler.transform);
            enemySpawned = true;
            enemyActive = true;
            break;
        }
    }
    
    //This function handles restarting the game
    void Restart()
    {
        //If the restart key is hit, then the title scene is loaded
        if (Input.GetKeyDown(restart))
            {
                SceneManager.LoadScene("TitleScene");
            }
    }
    //This function handles going to the next scene of a game
    void NextScene()
    {
        //If the next key is hit, then the next scene is loaded
        if (Input.GetKeyDown(next))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
    }
}
