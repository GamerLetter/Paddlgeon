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
        if (other.gameObject.tag == "Score_Wall")
        {
            int randomCurse = 0;
                if (randomCurse == 0)
                {
                    Paddle.activeCurse = "giant";
                }
                else if (randomCurse == 1)
                {
                    Paddle.activeCurse = "punch";
                }
                else if (randomCurse == 2)
                {
                    Paddle.activeCurse = "double";
                }
        }
    }
}
