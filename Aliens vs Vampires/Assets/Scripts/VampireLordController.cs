using UnityEngine;

public class VampireLordController : EnemyController
{
    public GameObject vampirePrefab;
    public float summonCooldown = 7f;

    float timer;

    protected override void Update()
    {
        base.Update();

        timer += Time.deltaTime;

        if (timer >= summonCooldown)
        {
            Summon();
            timer = 0f;
        }
    }

    void Summon()
    {
        Vector3 spawnPos = transform.position + Vector3.left * 2f;

        GameObject v = Instantiate(vampirePrefab, spawnPos, Quaternion.identity);

        EnemyController e = v.GetComponent<EnemyController>();

        e.path = path;
        e.lane = lane;
    }
}