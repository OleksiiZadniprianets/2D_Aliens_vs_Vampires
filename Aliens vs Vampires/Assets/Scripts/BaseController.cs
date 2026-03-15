using UnityEngine;

public class BaseController : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;
    protected SpriteRenderer spriteRenderer;
    private bool isGameOver = false;

    void Start()
    {
        currentHP = maxHP;
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

        if (currentHP <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;

        Debug.Log("GAME OVER");

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