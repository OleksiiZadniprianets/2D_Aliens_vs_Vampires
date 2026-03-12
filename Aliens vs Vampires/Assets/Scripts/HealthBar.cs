using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform bar;

    float baseWidth;

    void Awake()
    {
        baseWidth = bar.localScale.x;
    }

    public void SetHealth(float current, float max)
    {
        float value = current / max;

        bar.localScale = new Vector3(
            baseWidth * value,
            bar.localScale.y,
            bar.localScale.z
        );
    }
}