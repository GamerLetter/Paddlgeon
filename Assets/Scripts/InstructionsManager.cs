using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionsManager : MonoBehaviour
{
  public Button controls; //Establishes a button variable called restart 
  public Button story; //Establishes a button variable called menu 
  public Button gimmicks; //Establishes a text variable called total_score
  public Button title;
  public Camera cam; //Establisshed a camera variable called cam
  public TMP_Text controlsText; //Text variable called control_text
  public TMP_Text storyText;
  public TMP_Text gimmickText;
  public TMP_Text welcomeText;
  private bool clicked = false; //Bool called clicked, set to false at first
  //Keycodes for next and restart arre set
  private KeyCode next = KeyCode.N;
  private KeyCode restart = KeyCode.R;
  private string currentlyAt = "Welcome";
  List<string> welcome = new List<string>(); //List that will be used for the tutorial


	void Start () {
		Button controlsButton = controls.GetComponent<Button>(); //Gets the component of the controls button from the button itself
		controlsButton.onClick.AddListener(Controls_Start); //Checks to see if the controls button  has been clicked on, if so it runs the Controls_Start function 
		Button storyButton = story.GetComponent<Button>(); //Gets the component of the story button from the button itself
		storyButton.onClick.AddListener(Story_Start); //Checks to see if the story button  has been clicked on,if so it runs the Story_Start function 
        Button gimmicksButton = gimmicks.GetComponent<Button>(); //Gets the component of the gimmicks button from the button itself
		gimmicksButton.onClick.AddListener(Gimmicks_Start); //Checks to see if the gimmciks button has been clicked on, if so it runs the Gimmicks_Start function 
        Button titleButton = title.GetComponent<Button>(); //Gets the component of the title button from the button itself
		titleButton.onClick.AddListener(Title_Start); //Checks to see if the title button has been clicked on, if so it runs the Title_Start function 
        //Cam is set to the main camera, scores from both levels are set to 0
        cam = Camera.main;
        //Adding numerous elements to the welcome list to help form the text 
        //I know the use here is the same as my last game, but I did have the idea of using lists for keeping track of highscores
        //I shifted over to giving the player an actual goal and didn't have time to input an endless mode so sadly its cut for the time being
        welcome.Add("Welcome to Paddlgeon!     ");
        welcome.Add("Here you can learn the game.   ");
        welcome.Add("Click the title button to return to the title.   ");
        //Sets the welcome text to equal all of the elements in the welcome list combined
        welcomeText.text = welcome[0] + welcome[1] + welcome[2];
	}
    void Update()
    {
        //If restart key is pressed, then the scene is set to the title
        if (Input.GetKeyDown(restart))
            {
                SceneManager.LoadScene("TitleScene");
            }
        //If the next key is pressed, then the next scene is loaded
        if (Input.GetKeyDown(next))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
    }
    //This function is used for clicking on the title button, it loads the title scene
    void Title_Start()
    {
        SceneManager.LoadScene("TitleScene");
    }
    //This function handles what happens when the controls button is clicked
    void Controls_Start()
    {
        //If the cameras position isn't set to (30, 0, -10) and clicked is false and currentlyAt is set to Welcome
        //then foreach item in welcme, its set to the right by 10 units and the controls text is set to Go Back!
        //currentlyAt is also set to Controls
        if (cam.transform.position != new Vector3(30, 0, -10) && clicked == false && currentlyAt == "Welcome")
        {
            foreach (var welcome in welcome)
            {
                cam.transform.position = cam.transform.position + new Vector3(10, 0, 0);
            }
            controlsText.text = "Go Back!";
            currentlyAt = "Controls";
        }
        //If the cameras position is set to (30, 0, -10) and clicked is true and controlsText is equal to Go Back!
        // then its set to the left by 30 units and the story text is changed to Controls!
        //currentlyAt is also set to Welcome
        if (cam.transform.position == new Vector3(30, 0, -10) && clicked == true && controlsText.text == "Go Back!")
        {
            for (int i = 0; i < 30; i++)
            {
                cam.transform.position = cam.transform.position - new Vector3(1, 0, 0);
            }
            controlsText.text = "Controls!";
            currentlyAt = "Welcome";

        }
        //Clicked is set to not clicked
        clicked = !clicked;
    }
    //This function handles what happens when the story button is clicked
    void Story_Start()
    {
        //If the cameras position isn't set to (-30, 0, -10) and clicked is false and currentlyAt is Welcome
        //  then its set to the left by 30 units and the story text is changed to Go Back!
        //currentlyAt is also set to Story
        if (cam.transform.position != new Vector3(-30, 0, -10) && clicked == false && currentlyAt == "Welcome")
        {
            for (int i = 0; i < 30; i++)
            {
                cam.transform.position = cam.transform.position + new Vector3(-1, 0, 0);
            }
            storyText.text = "Go Back!";
            currentlyAt = "Story";
        }
        //If the cameras position is set to (-30, 0, -10) and clicked is true and storyText is equal to Go Back!
        // then its sent to the right  by 30 units and the storyText is changed to Story
        //currentlyAt is also changed to Welcome

        if (cam.transform.position == new Vector3(-30, 0, -10) && clicked == true && storyText.text == "Go Back!")
        {
            for (int i = 0; i < 30; i++)
            {
                cam.transform.position = cam.transform.position - new Vector3(-1, 0, 0);
            }
            storyText.text = "Story!";
            currentlyAt = "Welcome";

        }
        //Clicked is set to not clicked
        clicked = !clicked;
    }
    //This function handles what happens when the gimmicks button is clicked
    void Gimmicks_Start()
    {
      //If the cameras position isn't set to (0, 30, -10) and clicked is false and currentlyAt is Welcome
      //then its sent upwards by 30 units and the gimmicks text is changed to Go Back!
      //currentlyAt is also set to Gimmicks
        if (cam.transform.position != new Vector3(0, 30, -10) && clicked == false && currentlyAt == "Welcome")
        {
            for (int i = 0; i < 30; i++)
            {
                cam.transform.position = cam.transform.position + new Vector3(0, 1, 0);
            }
            gimmickText.text = "Go Back!";
            currentlyAt = "Gimmicks";

        }
      //If the cameras position is set to (0, 30, -10) and clicked is true and the gimmickText is Go Back!
      //then its sent downwards by 30 units and the gimmicks text is changed to Curses and Enemies!
      //currentlyAt is also set to Welcome
        if (cam.transform.position == new Vector3(0, 30, -10) && clicked == true && gimmickText.text == "Go Back!")
        {
            for (int i = 0; i < 30; i++)
            {
                cam.transform.position = cam.transform.position - new Vector3(0, 1, 0);
            }
            gimmickText.text = "Curses and Enemies!";
            currentlyAt = "Welcome";

        }
        //Clicked is set to not clicked
        clicked = !clicked;
    }
}