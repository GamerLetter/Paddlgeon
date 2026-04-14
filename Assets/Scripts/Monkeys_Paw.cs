using UnityEngine;
public class Monkeys_Paw : MonoBehaviour
{
    public Animator animations;
    private bool active = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animations.Play("MonkeyPaw-Idle");
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball" && active == false)
        {
            animations.Play("MonkeysPaw-Activate");
            Debug.Log("AAAAAAAAAA");
            active = true;
        }
    }
}
