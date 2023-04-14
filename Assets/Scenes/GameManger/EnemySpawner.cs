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
        var enemy = EnemyPooler.Instance.InstantiatePooledEnemy(GetRandomPosition());
    }

    private Vector3 GetRandomPosition()
    {
        float randomY = GameManager.Instance.getRandomYInSceneBounds();
        float maxX = GameManager.Instance.getSceneMaxX();
        return new Vector3(maxX, randomY, 0);
    }
}
