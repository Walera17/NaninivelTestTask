using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy[] enemies;

    public static readonly List<Enemy> enemiesList = new();

    public void SpawnEnemies(Transform spawnPoint)
    {
        Enemy newEnemy = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoint.position, spawnPoint.rotation, spawnPoint);
        enemiesList.Add(newEnemy);
    }

    public static void Remove(Enemy enemy)
    {
        enemiesList.Remove(enemy);
    }
}