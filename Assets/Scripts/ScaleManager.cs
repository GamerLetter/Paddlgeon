using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    //NOTE: Wasn't really sure how to use the math functions so I decided to make basic scaling effects for the win screen.
    //NOTE (continued): I had to get help with making this but I do understand how everything works!
    //Setting up numerous game object variables for the paddle, ball, and chest
    public GameObject paddle;
    public GameObject ball;
    public GameObject chest;

    //Setting public float scale speeds for each object
    public float paddleScaleSpeed;
    public float chestScaleSpeed;
    public float ballScaleSpeed;

    // Update is called once per frame
    void Update()
    {
        //balls scale is set to a Vector2 of SinValue and SinValue
        ball.transform.localScale = new Vector2(SinValue(), SinValue());
        //chests scale is set to a Vector2 of CosValue and CosValue
        chest.transform.localScale = new Vector2(CosValue(), CosValue());
        //paddles scale is set to a Vector2 of SqrtValue and SqrtValue
        paddle.transform.localScale = new Vector2(SqrtValue(), SqrtValue());
    }

    //Public float value called SinValue(), which returns the the sine of delta time multiplied by the ballScaleSpeed
    public float SinValue()
    {
        return Mathf.Sin(Time.deltaTime * ballScaleSpeed);
    }
    //Public float value called CosValue(), which returns the the cosine of delta time multiplied by the chestScaleSpeed
    public float CosValue()
    {
        return Mathf.Cos(Time.deltaTime * chestScaleSpeed);
    }
    //Public float value called SqrtValue(), which returns the the square root of delta time multiplied by the paddleScaleSpeed
    public float SqrtValue()
    {
        return Mathf.Sqrt(Time.deltaTime * paddleScaleSpeed);
    }
}
