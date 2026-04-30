using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    private float destroyTime = 0.8f;
    private bool hit = false;
    public Animator animations; 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "Untagged") 
        {
            animations.Play("ChestDestroyanim");
            hit = true;
        }
    }

    public void Update()
    {
        if (hit == true)
        {
            destroyTime -= Time.deltaTime;
        }
        if (destroyTime < 0f ||destroyTime == 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
