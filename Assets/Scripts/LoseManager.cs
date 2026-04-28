using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class LoseManager : MonoBehaviour
{
  public Button reset; //Establishes a button variable called restart 
  public Button title; //Establishes a button variable called menu 
  public TMP_Text scoreText;
  //Keycodes for next and restart are set
  private KeyCode next = KeyCode.N;
  private KeyCode restart = KeyCode.R;

	void Start () {
		Button resetButton = reset.GetComponent<Button>(); //Gets the component of the level1 button from the button itself
		resetButton.onClick.AddListener(gameStart); //Checks to see if the level1 button  has been clicked on, if so it runs the Level1_Start function 
		Button titleButton = title.GetComponent<Button>(); //Gets the component of the level2 button from the button itself
		titleButton.onClick.AddListener(titleStart); //Checks to see if the level2 button  has been clicked on,if so it runs the Level2_Start function 

	}
    void Update()
    {
        scoreText.text = "You only returned treasure " + Ball.setScoreText.ToString() + "/30 times.";
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
    void titleStart()
    {
        SceneManager.LoadScene("TitleScene");
    }
    
}
