using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public HealthBar healthBar;
    public Transform[] path;
    public float speed = 2f;

    public int lane;

    float baseDamage;

    public int maxHP = 30;
    int currentHP;

    public int damage = 10;

    private int index = 0;

    void Start()
    {
        currentHP = maxHP;
        healthBar.SetHealth(currentHP, maxHP);
        baseDamage = damage;
    }

    public float attackRange = 1.2f;
    public float attackCooldown = 1.5f;

    float attackTimer = 0f;
    void Update()
    {
        if (path == null || path.Length == 0)
            return;

        attackTimer += Time.deltaTime;

        AlienBladeController alien = FindClosestAlien();

        if (alien != null)
        {
            float dist = Vector3.Distance(transform.position, alien.transform.position);

            if (dist <= attackRange)
            {
                if (attackTimer >= attackCooldown)
                {
                    float finalDamage = damage;

                    if (DayNightManager.instance.isNight)
                    {
                        finalDamage *= 1.5f;
                    }

                    alien.TakeDamage((int)finalDamage);
                    attackTimer = 0f;
                }

                return;
            }
        }
        AlienBladeController FindClosestAlien()
        {
            AlienBladeController[] aliens = FindObjectsOfType<AlienBladeController>();

            AlienBladeController closest = null;
            float minDist = Mathf.Infinity;

            foreach (AlienBladeController alien in aliens)
            {
                float dist = Vector3.Distance(transform.position, alien.transform.position);

                if (dist < minDist)
                {
                    minDist = dist;
                    closest = alien;
                }
            }

            return closest;
        }
        if (index >= path.Length)
        {
            ReachBase();
            return;
        }

        Transform target = path[index];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            index++;
        }

    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        healthBar.SetHealth(currentHP, maxHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        CoinManager.instance.AddCoins(10);
        Destroy(gameObject);
    }

    void ReachBase()
    {
        BaseController baseController = FindObjectOfType<BaseController>();

        if (baseController != null)
        {
            baseController.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}