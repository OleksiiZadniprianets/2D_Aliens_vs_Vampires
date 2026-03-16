using UnityEngine;
using TMPro;

public class BaseController : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;

    protected SpriteRenderer spriteRenderer;

    private bool isGameOver = false;

    // --- UI HP ---
    public TextMeshProUGUI hpText;

    // --- Game Over ---
    public GameObject gameOverImage;

    // --- Sound ---
    public AudioClip gameOverSound;
    AudioSource audioSource;

    void Start()
    {
        currentHP = maxHP;

        // Знайдено AudioSource
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        UpdateHP();
    }

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        if (isGameOver)
            return;

        currentHP -= damage;

        if (currentHP < 0)
            currentHP = 0;

        Debug.Log("Base HP: " + currentHP);

        UpdateHP();

        if (currentHP <= 0)
        {
            GameOver();
        }
    }

    void UpdateHP()
    {
        if (hpText != null)
            hpText.text = "HP: " + currentHP;
    }

    void GameOver()
    {
        isGameOver = true;

        Debug.Log("GAME OVER");

        if (gameOverImage != null)
            gameOverImage.SetActive(true);

        if (audioSource != null && gameOverSound != null)
            audioSource.PlayOneShot(gameOverSound);

        Time.timeScale = 0f;
    }

    public virtual void SetNightMode(bool night)
    {
        Debug.Log(gameObject.name + " night mode: " + night);

        if (spriteRenderer == null)
            return;

        if (night)
            spriteRenderer.color = new Color(1f, 0.6f, 0.6f);
        else
            spriteRenderer.color = Color.white;
    }
}