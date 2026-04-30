using UnityEngine;

public class Plunger : MonoBehaviour
{
    public Rigidbody2D rb; //Establishes a Rigidbody2D variable called rb
    private bool hit = false; //A private bool called hit, set to false
    private bool grounded = false; //A private bool called grounded, set to false
    public Animator animations; //A public animator called animations
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
   void OnTriggerEnter2D(Collider2D other)
   //If the object has a ball tag and hit is set to false, then the gravity is set to 2 and the plunger swap animation is played
   {
        if (other.gameObject.tag == "Ball" && hit == false)
        {
            rb.gravityScale = 2;
            animations.Play("Plunger-Swap");
        }
        //If the objects tag is ball and grounded is true, the plunger is destroyed
        if (other.gameObject.tag == "Ball" && grounded == true)
        {
           Destroy(this.gameObject);
        }
        //If the objects tag is PlungerBlocker, the boddy type is switched to Static, a message is sent to the debug log, the Plunger Idle Down animation is played, and grounded is set to true
        if (other.gameObject.tag == "PlungerBlocker")
        {
            rb.bodyType = RigidbodyType2D.Static;
            Debug.Log("PLUNGER DETECTED!");
            animations.Play("Plunger-Idle-Down");
            grounded = true;
        }
   }
}
