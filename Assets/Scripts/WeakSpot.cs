using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) //A funtion that checks to see if the ball enters trigger areas
    {
        if (other.gameObject.tag == "Ball") //If the ball enters a trigger area with the tag wall their direction will change
        {
            Destroy(this.gameObject);
        }
    }
}
