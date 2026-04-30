using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Titlemanager : MonoBehaviour
{
  public Button start; //Establishes a button variable called start 
  public Button instructions; //Establishes a button variable called instructions 
  //Keycodes for next and restart are set
  private KeyCode next = KeyCode.N;
  private KeyCode restart = KeyCode.R;

	void Start () {
		Button startButton = start.GetComponent<Button>(); //Gets the component of the startButton from the button itself
		startButton.onClick.AddListener(gameStart); //Checks to see if the startButton button  has been clicked on, if so it runs the gameStart function 
		Button instructionsButton = instructions.GetComponent<Button>(); //Gets the component of the instructionsButton button from the button itself
		instructionsButton.onClick.AddListener(tutorialStart); //Checks to see if the instructionsButton button has been clicked on,if so it runs the tutorialStart function 
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
    //Loads the game scene
    void gameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    //Loads the tutorial scene
    void tutorialStart()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    
}
