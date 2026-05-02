using UnityEngine;

public class Sword : MonoBehaviour
{
    public float swordThrowTime = 5f; //A public float called swordThrowTime, this is for when the sword starts moving
    public float elapsedTime = 0f; //A public float called elapsedTime
	public float moveSpeed; //A public float called moveSpeed, handles how fast the sword moves
    public Rigidbody2D swordRB; //A public Rigidbody2D called swordRB  
    // Update is called once per frame
    void Update()
    {
        //The elapsed time is set to itself + delta time every frame
        elapsedTime += Time.deltaTime;
        //If the elapsed time is greater than or equal to the sword throw time then the sword is thrown across the screen by adding force towards the right
        if (elapsedTime >= swordThrowTime)
        {
			swordRB.AddForce(Vector2.right * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);  
        }
    }
    //If the sword enters a trigger area with the tag Paddle or Score_Wall, the sword is then destroyed
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Paddle" || other.gameObject.tag == "Score_Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
