using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public GameObject batPrefab;
    public GameObject bloodDrinkerPrefab;
    public GameObject bufferPrefab;
    public GameObject lordPrefab;

    public Transform spawnPoint;
    public Transform[] path;

    public float spawnInterval = 3f;

    public int lane;

    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        float time = Time.timeSinceLevelLoad;

        GameObject[] pool;

        if (time > 90f)
        {
            pool = new GameObject[]
            {
            enemyPrefab,
            batPrefab,
            bloodDrinkerPrefab,
            bufferPrefab,
            lordPrefab
            };
        }
        else if (time > 60f)
        {
            pool = new GameObject[]
            {
            enemyPrefab,
            batPrefab,
            bloodDrinkerPrefab,
            bufferPrefab
            };
        }
        else if (time > 30f)
        {
            pool = new GameObject[]
            {
            enemyPrefab,
            batPrefab,
            bloodDrinkerPrefab
            };
        }
        else
        {
            pool = new GameObject[]
            {
            enemyPrefab
            };
        }

        GameObject enemyToSpawn = pool[Random.Range(0, pool.Length)];

        GameObject enemy = Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);

        EnemyController controller = enemy.GetComponent<EnemyController>();

        controller.path = path;
        controller.lane = lane;
    }
}