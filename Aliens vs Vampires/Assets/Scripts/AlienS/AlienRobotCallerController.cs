using UnityEngine;

public class AlienRobotCallerController : MonoBehaviour
{
    public int maxHP = 40;
    int currentHP;

    public int lane;

    public float abilityCooldown = 15f;
    public int robotDamage = 50;
    public AudioClip robotAttackSound;
    AudioSource audioSource;

    float timer;

    void Start()
    {
        currentHP = maxHP;
        timer = abilityCooldown;

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            CallRobot();
            timer = abilityCooldown;
        }
    }

    void CallRobot()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();

        EnemyController strongest = null;
        int highestHP = 0;

        foreach (EnemyController e in enemies)
        {
            if (e.lane != lane)
                continue;

            if (e.maxHP > highestHP)
            {
                highestHP = e.maxHP;
                strongest = e;
            }
        }

        if (audioSource != null && robotAttackSound != null)
            audioSource.PlayOneShot(robotAttackSound);

        if (strongest != null)
            strongest.TakeDamage(robotDamage);
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            Destroy(gameObject);
    }
}