using UnityEngine;
using UnityEngine.SceneManagement;
public class Game_over : MonoBehaviour

{

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D  other) //This code is ran when the object its attached to enters another trigger object
    {
        if (other.gameObject.tag == "Ball") //Checks to see if the object that it entered has the tag Ball
        {
        SceneManager.LoadScene("SampleScene");//If the tag is Ball, it reloads the game scene.
        }
    }
}
