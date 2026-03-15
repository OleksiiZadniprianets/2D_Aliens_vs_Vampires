using UnityEngine;
using TMPro;
using System.Collections;

public class DayNightManager : MonoBehaviour
{
    public static DayNightManager instance;

    public bool isNight = false;
    public float cycleLength = 60f;
    public SpriteRenderer grassRenderer;

    public SpriteRenderer roadTopRenderer;
    public SpriteRenderer roadBottomRenderer;

    public Sprite grassDay;
    public Sprite grassNight;

    public Sprite roadDay;
    public Sprite roadNight;

    public GameObject sun;
    public GameObject moon;

    public float transitionDuration = 2f;
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
    public void SwitchToNight()
    {
        Debug.Log("Switching to NIGHT");
        StartCoroutine(DayNightTransition(true));
    }

    public void SwitchToDay()
    {
        Debug.Log("Switching to DAY");
        StartCoroutine(DayNightTransition(false));
    }
    IEnumerator DayNightTransition(bool night)
    {
        float duration = 1.5f;
        float timer = 0f;

        Color startColor = Color.white;
        Color darkColor = new Color(0.45f, 0.45f, 0.65f);

        // затемнення
        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;

            Color c = Color.Lerp(startColor, darkColor, t);

            grassRenderer.color = c;
            roadTopRenderer.color = c;
            roadBottomRenderer.color = c;

            yield return null;
        }

        // зміна спрайтів
        if (night)
        {
            grassRenderer.sprite = grassNight;

            roadTopRenderer.sprite = roadNight;
            roadBottomRenderer.sprite = roadNight;

            sun.SetActive(false);
            moon.SetActive(true);
        }
        else
        {
            grassRenderer.sprite = grassDay;

            roadTopRenderer.sprite = roadDay;
            roadBottomRenderer.sprite = roadDay;

            sun.SetActive(true);
            moon.SetActive(false);
        }

        // освітлення назад
        timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;

            Color c = Color.Lerp(darkColor, startColor, t);

            grassRenderer.color = c;
            roadTopRenderer.color = c;
            roadBottomRenderer.color = c;

            yield return null;
        }

        grassRenderer.color = Color.white;
        roadTopRenderer.color = Color.white;
        roadBottomRenderer.color = Color.white;
        EnemyController[] enemies =
    FindObjectsByType<EnemyController>(FindObjectsSortMode.None);

        foreach (EnemyController enemy in enemies)
        {
            enemy.SetNightMode(night);
        }
    }
    public bool IsNight()
    {
        return isNight;
    }
}