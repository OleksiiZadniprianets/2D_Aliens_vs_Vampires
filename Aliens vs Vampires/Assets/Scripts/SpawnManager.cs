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

        GameObject enemyToSpawn = enemyPrefab;

        if (time > 90f)
        {
            int r = Random.Range(0, 5);

            if (r == 0) enemyToSpawn = lordPrefab;
            else if (r == 1) enemyToSpawn = bufferPrefab;
            else if (r == 2) enemyToSpawn = bloodDrinkerPrefab;
            else if (r == 3) enemyToSpawn = batPrefab;
            else enemyToSpawn = enemyPrefab;
        }
        else if (time > 60f)
        {
            int r = Random.Range(0, 4);

            if (r == 0) enemyToSpawn = bufferPrefab;
            else if (r == 1) enemyToSpawn = bloodDrinkerPrefab;
            else if (r == 2) enemyToSpawn = batPrefab;
            else enemyToSpawn = enemyPrefab;
        }
        else if (time > 30f)
        {
            enemyToSpawn = Random.value > 0.5f ? batPrefab : bloodDrinkerPrefab;
        }

        GameObject enemy = Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);

        EnemyController controller = enemy.GetComponent<EnemyController>();

        controller.path = path;
        controller.lane = lane;
    }
}