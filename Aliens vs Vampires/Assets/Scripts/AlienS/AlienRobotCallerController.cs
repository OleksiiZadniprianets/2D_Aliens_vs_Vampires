using UnityEngine;

public class AlienRobotCallerController : MonoBehaviour
{
    public int maxHP = 40;
    int currentHP;

    public int lane;

    public float abilityCooldown = 15f;
    public int robotDamage = 50;

    float timer;

    void Start()
    {
        currentHP = maxHP;
        timer = abilityCooldown;
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

        if (strongest != null)
        {
            strongest.TakeDamage(robotDamage);
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            Destroy(gameObject);
    }
}