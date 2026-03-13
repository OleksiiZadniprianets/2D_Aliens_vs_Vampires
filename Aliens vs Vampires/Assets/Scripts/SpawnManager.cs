using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform spawnPoint;
    public Transform[] path;

    public float spawnInterval = 3f;

    public int lane; // 0 = top lane, 1 = bottom lane

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
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        EnemyController controller = enemy.GetComponent<EnemyController>();

        controller.path = path;

        controller.lane = lane; // передаємо lane ворогу
    }
}