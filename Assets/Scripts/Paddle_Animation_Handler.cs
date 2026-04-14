using UnityEngine;

public class Paddle_Animation_Handler : MonoBehaviour
{
	public Rigidbody2D my_rb;                  
    public Animator animations;

        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            animations.Play("Paddle-Hit");
            animations.Play("Paddle-Not-Hit");
        }
    }
}
