using UnityEngine;

public class VampireBufferController : EnemyController
{
    public float buffCooldown = 5f;
    public float buffMultiplier = 1.25f;

    float timer;
    bool stopped = false;

    protected override void Update()
    {
        if (!stopped)
        {
            base.Update();

            if (transform.position.x < 7f)
            {
                stopped = true;
                speed = 0;
            }

            return;
        }

        timer += Time.deltaTime;

        if (timer >= buffCooldown)
        {
            Buff();
            timer = 0f;
        }
    }

    void Buff()
    {
        EnemyController[] enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);

        foreach (EnemyController e in enemies)
        {
            if (e == this) continue;

            if (Vector3.Distance(transform.position, e.transform.position) < 6f)
            {
                e.damage += 5;
            }
        }
    }
}