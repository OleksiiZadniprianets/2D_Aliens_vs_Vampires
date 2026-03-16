using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;

    public float dayDuration = 30f;

    float gameTime;
    float cycleTimer;

    bool isNight = false;

    void Update()
    {
        gameTime += Time.deltaTime;
        cycleTimer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

        if (!isNight && cycleTimer >= dayDuration)
        {
            isNight = true;
            DayNightManager.instance.SwitchToNight();
        }

        if (isNight && cycleTimer >= dayDuration * 2)
        {
            isNight = false;
            cycleTimer = 0;

            DayNightManager.instance.SwitchToDay();
        }
    }
}