using UnityEngine;
public class Monkeys_Paw : MonoBehaviour
{
    //Animator called animations and a private bool called active (set to false) are estanlished
    public Animator animations;
    private bool active = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //On start, the idle animation is played
    void Start()
    {
        animations.Play("MonkeyPaw-Idle");
    }


    //If the monkeys paw enters an object with the tag ball and active is set to false
    //The monkeys paw activated animation is played, a message to the debug log is sent, and active is set to true
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball" && active == false)
        {
            animations.Play("MonkeysPaw-Activate");
            Debug.Log("THE PAW IS ACTIVE!!!!");
            active = true;
        }
    }
}
