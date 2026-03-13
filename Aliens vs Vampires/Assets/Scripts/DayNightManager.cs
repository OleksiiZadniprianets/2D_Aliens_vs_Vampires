using UnityEngine;
using TMPro;

public class DayNightManager : MonoBehaviour
{
    public static DayNightManager instance;

    public bool isNight = false;
    public float cycleLength = 60f;

    float timer;

    public TMP_Text dayNightText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= cycleLength)
        {
            timer = 0;
            ToggleDayNight();
        }
    }

    void ToggleDayNight()
    {
        isNight = !isNight;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (isNight)
            dayNightText.text = "NIGHT";
        else
            dayNightText.text = "DAY";
    }
}