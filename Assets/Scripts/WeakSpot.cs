using UnityEngine;

public class WeakSpot : Monkeys_Paw
{
    private float destroyTime = 0.8f; //Private float called destroyTime, set to 0.8f
    private bool hit = false; //private bool called hit, set to false
    void OnTriggerEnter2D(Collider2D other)
    {
        //If the other game object is Ball or Untagged, then the chests destroy animation is played and hit is set to true
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "Untagged") 
        {
            animations.Play("ChestDestroyanim");
            hit = true;
        }
    }

    public void Update()
    {
        //If hit is true then the destroyTime is equal to itself minus delta time (I'm not sure why I subtracted this time instead of added)
        if (hit == true)
        {
            destroyTime -= Time.deltaTime;
        }
        //If the destroyTime is less than 0f or == to 0f, the chest is destroyed
        if (destroyTime < 0f ||destroyTime == 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
