using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // objeto de enemigo que será spawnado
    public Transform spawnPoint;  // punto donde se spawneará el enemigo
    public float spawnRate = 2f;  // tiempo en segundos para spawnear otro enemigo
    public float minSpawnRate = 3f;  // tiempo mínimo en segundos para spawnear otro enemigo
    public float maxSpawnRate = 20f; // tiempo máximo en segundos para spawnear otro enemigo
    private float nextSpawnTime = 0f; // tiempo de espera hasta el próximo spawn

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy(); // llama a la función para spawnear un enemigo
            nextSpawnTime = Time.time + Random.Range(minSpawnRate, maxSpawnRate); // actualiza el tiempo de espera para el próximo spawn
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); // crea una instancia del enemigo en la posición y rotación del spawnPoint
    }
}
