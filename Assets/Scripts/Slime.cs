using UnityEngine;

public class Slime : MonoBehaviour
{

void OnTriggerEnter2D(Collider2D other) 
    {
        //If the object enters a object with the tag Ball, then this object the script is attached to is destroyed
        if (other.gameObject.tag == "Ball")
        {
            Destroy(this.gameObject);
        }
}
}