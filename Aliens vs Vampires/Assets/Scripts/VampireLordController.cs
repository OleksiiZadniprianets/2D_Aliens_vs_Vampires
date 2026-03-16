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