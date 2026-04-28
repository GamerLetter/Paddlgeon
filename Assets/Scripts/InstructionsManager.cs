using UnityEngine;
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
  private bool clicked = false; //Bool called clicked, set to false at first
  //Keycodes for next and restart arre set
  private KeyCode next = KeyCode.N;
  private KeyCode restart = KeyCode.R;

	void Start () {
		Button controlsButton = controls.GetComponent<Button>(); //Gets the component of the level1 button from the button itself
		controlsButton.onClick.AddListener(Controls_Start); //Checks to see if the level1 button  has been clicked on, if so it runs the Level1_Start function 
		Button storyButton = story.GetComponent<Button>(); //Gets the component of the level2 button from the button itself
		storyButton.onClick.AddListener(Story_Start); //Checks to see if the level2 button  has been clicked on,if so it runs the Level2_Start function 
        Button gimmicksButton = gimmicks.GetComponent<Button>(); //Gets the component of the controls button from the button itself
		gimmicksButton.onClick.AddListener(Gimmicks_Start); //Checks to see if the controls button has been clicked on, if so it runs the Controls function 
        Button titleButton = title.GetComponent<Button>(); //Gets the component of the controls button from the button itself
		titleButton.onClick.AddListener(Title_Start); //Checks to see if the controls button has been clicked on, if so it runs the Controls function 
        //Cam is set to the main camera, scores from both levels are set to 0
        cam = Camera.main;

	}
    void Update()
    {
        //If restart key is pressed, then the scene is set to the title
        if (Input.GetKeyDown(restart))
            {
                SceneManager.LoadScene("Title");
            }
        //If the next key is pressed, then the next scene is loaded
        if (Input.GetKeyDown(next))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
    }
    //Loads the Level_1 scene
    void Title_Start()
    {
        SceneManager.LoadScene("TitleScene");
    }
    
    void Controls_Start()
    {
        //If the cameras position isn't set to (30, 0, -10) and clicked is false, then its set to the right by 30 units and the control_text is changed
        if (cam.transform.position != new Vector3(30, 0, -10) && clicked == false)
        {
            for (int i = 0; i < 30; i++)
            {
                cam.transform.position = cam.transform.position + new Vector3(1, 0, 0);
            }
            controlsText.text = "Go Back!";

        }
        //If the cameras position is set to (30, 0, -10) and clicked is true, then its set to the left  by 30 units and the control_text is changed

        if (cam.transform.position == new Vector3(30, 0, -10) && clicked == true && controlsText.text == "Go Back!")
        {
            for (int i = 0; i < 30; i++)
            {
                cam.transform.position = cam.transform.position - new Vector3(1, 0, 0);
            }
            controlsText.text = "Controls!";

        }
        //Clicked is set to not clicked
        clicked = !clicked;
    }
    void Story_Start()
    {
        //If the cameras position isn't set to (30, 0, -10) and clicked is false, then its set to the right by 30 units and the control_text is changed
        if (cam.transform.position != new Vector3(-30, 0, -10) && clicked == false)
        {
            for (int i = 0; i < 30; i++)
            {
                cam.transform.position = cam.transform.position + new Vector3(-1, 0, 0);
            }
            storyText.text = "Go Back!";
        }
        //If the cameras position is set to (30, 0, -10) and clicked is true, then its set to the left  by 30 units and the control_text is changed

        if (cam.transform.position == new Vector3(-30, 0, -10) && clicked == true && storyText.text == "Go Back!")
        {
            for (int i = 0; i < 30; i++)
            {
                cam.transform.position = cam.transform.position - new Vector3(-1, 0, 0);
            }
            storyText.text = "Story!";

        }
        //Clicked is set to not clicked
        clicked = !clicked;
    }
void Gimmicks_Start()
    {
        //If the cameras position isn't set to (30, 0, -10) and clicked is false, then its set to the right by 30 units and the control_text is changed
        if (cam.transform.position != new Vector3(0, 30, -10) && clicked == false)
        {
            for (int i = 0; i < 30; i++)
            {
                cam.transform.position = cam.transform.position + new Vector3(0, 1, 0);
            }
            gimmickText.text = "Go Back!";

        }
        //If the cameras position is set to (30, 0, -10) and clicked is true, then its set to the left  by 30 units and the control_text is changed

        if (cam.transform.position == new Vector3(0, 30, -10) && clicked == true && gimmickText.text == "Go Back!")
        {
            for (int i = 0; i < 30; i++)
            {
                cam.transform.position = cam.transform.position - new Vector3(0, 1, 0);
            }
            gimmickText.text = "Curses and Enemies!";

        }
        //Clicked is set to not clicked
        clicked = !clicked;
    }
}