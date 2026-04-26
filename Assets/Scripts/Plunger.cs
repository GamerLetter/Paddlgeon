using UnityEngine;

public class Plunger : MonoBehaviour
{
    public Rigidbody2D rb;
    private bool hit = false;
    private bool grounded = false;
    public Animator animations;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
   void OnTriggerEnter2D(Collider2D other)
   {
        if (other.gameObject.tag == "Ball" && hit == false)
        {
            rb.gravityScale = 2;
            animations.Play("Plunger-Swap");
        }
        if (other.gameObject.tag == "Ball" && grounded == true)
        {
           Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "PlungerBlocker")
        {
            rb.bodyType = RigidbodyType2D.Static;
            Debug.Log("PLUNGER DETECTED!");
            animations.Play("Plunger-Idle-Down");
            grounded = true;
        }
   }
}
