using UnityEngine;

public class Paddle_Animation_Handler : MonoBehaviour
{
	public Rigidbody2D my_rb;                  
    public Animator animations;
    private int randomCurse;
        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            animations.Play("Paddle-Hit");
            animations.Play("Paddle-Not-Hit");
        }
        if (other.gameObject.tag == "Monkeys Paw")
        {
             randomCurse = Random.Range(0, 2);
             Debug.Log(randomCurse);
                if (randomCurse == 0)
                {
                    GameManager.activeCurse = "giant";
                }
                else if (randomCurse == 1)
                {
                    GameManager.activeCurse = "clone";
                }
                else if (randomCurse == 2)
                {
                    GameManager.activeCurse = "punch";
                }
        }
    }
}
