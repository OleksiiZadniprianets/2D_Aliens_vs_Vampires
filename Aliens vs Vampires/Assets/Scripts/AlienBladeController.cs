using UnityEngine;

public class AlienBladeController : MonoBehaviour
{
    public int maxHP = 50;
    int currentHP;
    public HealthBar healthBar;

    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    public int damage = 10;

    public int lane;

    float timer = 0f;

    void Start()
    {
        currentHP = maxHP;
        healthBar.SetHealth(currentHP, maxHP);
    }

    void Update()
    {
        timer += Time.deltaTime;

        EnemyController target = FindClosestEnemy();

        if (target == null)
            return;

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance <= attackRange)
        {
            if (timer >= attackCooldown)
            {
                Attack(target);
                timer = 0f;
            }
        }
    }

    EnemyController FindClosestEnemy()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();

        EnemyController closest = null;
        float minDist = Mathf.Infinity;

        foreach (EnemyController enemy in enemies)
        {
            if (enemy.lane != lane)
                continue;

            float dist = Vector3.Distance(transform.position, enemy.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy;
            }
        }

        return closest;
    }

    void Attack(EnemyController enemy)
    {
        enemy.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        healthBar.SetHealth(currentHP, maxHP);

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}