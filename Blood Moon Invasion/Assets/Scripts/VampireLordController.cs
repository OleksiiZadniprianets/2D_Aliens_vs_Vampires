using UnityEngine;

public class VampireLordController : EnemyController
{
    public GameObject vampirePrefab;
    public float summonCooldown = 3f;

    float summonTimer;

    protected override void Update()
    {
        base.Update();

        summonTimer += Time.deltaTime;

        if (summonTimer >= summonCooldown)
        {
            Summon();
            summonTimer = 0f;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        healthBar.SetHealth(currentHP, maxHP);

        if (currentHP <= 0)
            Die();
    }
    void Die()
    {
        CoinManager.instance.AddCoins(coinReward);
        Destroy(gameObject);
    }
    void Summon()
    {
        Vector3 spawnPos = transform.position + Vector3.left * 2f;

        GameObject v = Instantiate(vampirePrefab, spawnPos, Quaternion.identity);

        EnemyController newEnemy = v.GetComponent<EnemyController>();
        EnemyController lord = GetComponent<EnemyController>();

        if (newEnemy != null && lord != null)
        {
            newEnemy.path = lord.path;
            newEnemy.lane = lord.lane;
            newEnemy.index = lord.index;   // ⭐ ключовий рядок
        }
    }
}