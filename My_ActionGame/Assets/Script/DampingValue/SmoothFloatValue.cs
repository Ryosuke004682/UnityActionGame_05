using UnityEngine;

/*値の補完を楽にするための値補間インターフェイス*/
public class SmoothFloatValue
{
    private float calcuratedValue;
    private float velocity;
    private float smoothTime;
    private float maxSpeed;

    public SmoothFloatValue(float smoothTime = 1.0f, float maxSpeed = 1.0f)
    {
        this.smoothTime = smoothTime;
        this.maxSpeed = maxSpeed;
    }

    public float Calcuration(float currentValue)
    {
        calcuratedValue = Mathf.SmoothDamp(calcuratedValue , currentValue , ref velocity , smoothTime , maxSpeed);
        return calcuratedValue;
    }

}
