using UnityEngine;

public class BaseController : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;

    private bool isGameOver = false;

    void Start()
    {
        currentHP = maxHP;
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
}