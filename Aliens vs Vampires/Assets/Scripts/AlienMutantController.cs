using UnityEngine;
using System.Collections;

public class AlienMutantController : AlienBaseController
{

    public float meleeRange = 1.5f;
    public int meleeDamage = 8;
    public float meleeCooldown = 1f;

    public float shootRange = 4f;
    public int shootDamage = 5;
    public float shootCooldown = 0.8f;
    public Transform firePoint;
    public LineRenderer laser;

    float meleeTimer;
    float shootTimer;

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        meleeTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;

        EnemyController target = FindTarget();

        if (target == null)
            return;

        float dist = Vector3.Distance(transform.position, target.transform.position);

        if (dist <= shootRange && shootTimer >= shootCooldown)
        {
            target.TakeDamage(shootDamage);

            if (laser != null && firePoint != null)
                StartCoroutine(Laser(target.transform));

            shootTimer = 0f;
        }

        if (dist <= meleeRange && meleeTimer >= meleeCooldown)
        {
            target.TakeDamage(meleeDamage);
            meleeTimer = 0f;
        }
    }
    IEnumerator Laser(Transform target)
    {
        laser.enabled = true;

        laser.SetPosition(0, firePoint.position);
        laser.SetPosition(1, target.position);

        yield return new WaitForSeconds(0.1f);

        laser.enabled = false;
    }

    EnemyController FindTarget()
    {
        EnemyController[] enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);

        EnemyController closest = null;
        float minDist = Mathf.Infinity;

        foreach (EnemyController e in enemies)
        {
            if (e.lane != lane)
                continue;

            float dist = Vector3.Distance(transform.position, e.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                closest = e;
            }
        }

        return closest;
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            Destroy(gameObject);
    }
}