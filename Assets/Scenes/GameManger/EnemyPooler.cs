using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;

    public List<GameObject> pooledEnemies;

    public int poolSize;

    public static EnemyPooler Instance;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledEnemies = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            PoolNewEnemy();
        }
    }

    private GameObject PoolNewEnemy()
    {
        GameObject obj = CreateRandomEnemy();
        obj.SetActive(false);
        pooledEnemies.Add(obj);
        return obj;
    }

    private GameObject CreateRandomEnemy()
    {
        var index = Random.Range(0, enemyPrefabs.Count);
        return (GameObject)Instantiate(enemyPrefabs[index]);
    }

    public GameObject GetPooledEnemy()
    {
        for (int i = 0; i < pooledEnemies.Count; i++)
        {
            if (!pooledEnemies[i].activeInHierarchy)
            {
                return pooledEnemies[i];
            }
        }

        return PoolNewEnemy();
    }
}
