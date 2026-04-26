using UnityEngine;

public class Slime : MonoBehaviour
{

void OnTriggerEnter2D(Collider2D other) //A funtion that checks to see if the ball enters trigger areas
    {
        if (other.gameObject.tag == "Ball")
        {
            Destroy(this.gameObject);
        }
}
}