using UnityEngine;

public class BloodDrinkerController : EnemyController
{
    public int healOnHit = 5;

    protected override void Update()
    {
        if (path == null || path.Length == 0)
            return;

        attackTimer += Time.deltaTime;

        AlienBaseController alien = FindClosestAlien();

        if (alien != null)
        {
            float dist = Vector3.Distance(transform.position, alien.transform.position);

            if (dist <= attackRange)
            {
                if (attackTimer >= attackCooldown)
                {
                    float finalDamage = damage;

                    if (DayNightManager.instance.isNight)
                        finalDamage *= 1.25f;

                    alien.TakeDamage((int)finalDamage);

                    Heal();

                    attackTimer = 0f;
                }

                return;
            }
        }

        MoveAlongPath();
    }

    void Heal()
    {
        currentHP += healOnHit;

        if (currentHP > maxHP)
            currentHP = maxHP;

        if (healthBar != null)
            healthBar.SetHealth(currentHP, maxHP);
    }
}
