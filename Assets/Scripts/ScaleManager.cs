using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    public GameObject paddle;
    public GameObject ball;
    public GameObject chest;

    public float paddleScaleSpeed;
    public float chestScaleSpeed;
    public float ballScaleSpeed;

    // Update is called once per frame
    void Update()
    {
        ball.transform.localScale = new Vector2(SinValue(), SinValue());
        chest.transform.localScale = new Vector2(CosValue(), CosValue());
        paddle.transform.localScale = new Vector2(SqrtValue(), SqrtValue());
    }

    public float SinValue()
    {
        return Mathf.Sin(Time.deltaTime * ballScaleSpeed);
    }
    public float CosValue()
    {
        return Mathf.Cos(Time.deltaTime * chestScaleSpeed);
    }
    public float SqrtValue()
    {
        return Mathf.Sqrt(Time.deltaTime * paddleScaleSpeed);
    }
}
