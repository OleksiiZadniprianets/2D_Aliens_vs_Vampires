using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public HealthBar healthBar;
    public Transform[] path;
    public float speed = 2f;

    public int lane;

    public int maxHP = 30;
    protected int currentHP;

    public int damage = 5;

    private int index = 0;

    public float attackRange = 1.2f;
    public float attackCooldown = 1.5f;

    protected float attackTimer = 0f;


    void Start()
    {
        currentHP = maxHP;
        healthBar.SetHealth(currentHP, maxHP);
    }

    protected virtual void Update()
    {
        if (path == null || path.Length == 0)
            return;

        attackTimer += Time.deltaTime;

        AlienBaseController alien = FindClosestAlien();
        Debug.Log("Enemy lane = " + lane);
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
                        finalDamage *= 1.25f;
                    }
                    alien.TakeDamage((int)finalDamage);

                    attackTimer = 0f;
                }

                return;
            }
            Debug.Log("Alien found: " + alien);
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

    protected AlienBaseController FindClosestAlien()
    {
        AlienBaseController[] aliens = FindObjectsOfType<AlienBaseController>();

        AlienBaseController closest = null;
        float minDist = Mathf.Infinity;

        foreach (AlienBaseController alien in aliens)
        {
            // перевіряємо тільки доріжку
            if (alien.lane != lane)
                continue;

            float dist = Vector3.Distance(transform.position, alien.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                closest = alien;
            }
        }

        return closest;
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
        CoinManager.instance.AddCoins(15);
        Destroy(gameObject);
    }

    void ReachBase()
    {
        BaseController baseController = FindObjectOfType<BaseController>();

        if (baseController != null)
            baseController.TakeDamage(damage);

        Destroy(gameObject);
    }

    protected void MoveAlongPath()
    {
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

}