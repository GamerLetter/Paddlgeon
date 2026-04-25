using UnityEngine;

public class PunchPosition : MonoBehaviour
{
    public GameObject paddle;

    void Update()
    {
        this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x , paddle.transform.position.y);

    }
}
