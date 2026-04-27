using UnityEngine;

public class Sword : MonoBehaviour
{
    public float swordThrowTime = 5f;
    public float elapsedTime = 0f;
	public float moveSpeed;
    public Rigidbody2D swordRB;       
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime >= swordThrowTime)
        {
			swordRB.AddForce(Vector2.right * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);  
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Paddle" || other.gameObject.tag == "Score_Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
