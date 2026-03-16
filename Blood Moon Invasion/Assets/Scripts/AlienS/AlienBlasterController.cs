using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AlienBlasterController : MonoBehaviour
{
    public int maxHP = 40;
    int currentHP;

    public float attackRange;
    public float attackCooldown;
    public int damage;

    public Transform firePoint;
    public LineRenderer laser;

    public int lane;
    public AudioClip shootSound;
    AudioSource audioSource;
    float cooldown;

    void Start()
    {
        currentHP = maxHP;
        cooldown = 0f;

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;

        EnemyController target = FindTarget();

        if (target == null)
            return;

        float dist = Vector3.Distance(transform.position, target.transform.position);

        if (dist <= attackRange && cooldown <= 0f)
        {
            Shoot(target);
            cooldown = attackCooldown;
        }
    }

    EnemyController FindTarget()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();

        EnemyController closest = null;
        float minDist = Mathf.Infinity;

        foreach (EnemyController e in enemies)
        {
            // тільки своя лінія
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

    void Shoot(EnemyController enemy)
    {
        if (enemy == null)
            return;

        Debug.Log("Blaster shot");
        enemy.TakeDamage(damage);

        if (laser != null && firePoint != null)
            StartCoroutine(Laser(enemy.transform));

        if (audioSource != null && shootSound != null)
            audioSource.PlayOneShot(shootSound);
    }

    IEnumerator Laser(Transform target)
    {
        laser.enabled = true;

        laser.SetPosition(0, firePoint.position);
        laser.SetPosition(1, target.position);

        yield return new WaitForSeconds(0.1f);

        laser.enabled = false;
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            Destroy(gameObject);
    }
}