using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class WinManager : LoseManager
{
	void Start () {
		Button resetButton = reset.GetComponent<Button>(); //Gets the component of the level1 button from the button itself
		resetButton.onClick.AddListener(gameStart); //Checks to see if the level1 button  has been clicked on, if so it runs the Level1_Start function 
		Button titleButton = title.GetComponent<Button>(); //Gets the component of the level2 button from the button itself
		titleButton.onClick.AddListener(titleStart); //Checks to see if the level2 button  has been clicked on,if so it runs the Level2_Start function 

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
    //A function that loads the game scene
    void gameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    //A function that loads the title scene
    void titleStart()
    {
        SceneManager.LoadScene("TitleScene");
    }
    
}
