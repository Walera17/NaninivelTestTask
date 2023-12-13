using UnityEngine;

public class ShootingPoint : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float cameraCorrect;
    [SerializeField] private bool finish;
    [SerializeField] private Transform[] spawnPoints;

    public float CameraCorrect => cameraCorrect;

    public bool Finish => finish;

    public void SpawnEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            enemySpawner.SpawnEnemies(spawnPoint);
        }
    }
}