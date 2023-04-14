using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, 0.5f);
    }

    public void SpawnEnemy()
    {
        var newScale = new Vector3(0.1f, 0.1f, 0f);
        var enemy = EnemyPooler.Instance.SpawnPooledEnemy(GetRandomPosition(newScale));
    }

    private Vector3 GetRandomPosition(Vector3 scale)
    {
        float randomY = GameManager.Instance.getRandomYInSceneBounds(scale);
        float maxX = GameManager.Instance.getSceneMaxX();
        return new Vector3(maxX, randomY, 0);
    }
}
