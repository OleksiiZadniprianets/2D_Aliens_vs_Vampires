using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;

    float gameTime;

    void Update()
    {
        gameTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}