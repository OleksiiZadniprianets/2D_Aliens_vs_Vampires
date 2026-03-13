using UnityEngine;

public class AlienBaseController : MonoBehaviour
{
    public int lane;

    public int maxHP = 50;
    protected int currentHP;

    public HealthBar healthBar;

    void Start()
    {
        currentHP = maxHP;

        if (healthBar != null)
            healthBar.SetHealth(currentHP, maxHP);

        // ¬изначаЇмо lane по Y
        if (transform.position.y > 0)
            lane = 0;
        else
            lane = 1;
    }

    public virtual void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (healthBar != null)
            healthBar.SetHealth(currentHP, maxHP);

        if (currentHP <= 0)
            Destroy(gameObject);
    }
}