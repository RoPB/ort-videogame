using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;

    private List<GameObject> pooledEnemies = new List<GameObject>();

    public void Init(int level)
    {
        ClearPool();
    }

    private void ClearPool()
    {
        for (int i = 0; i < pooledEnemies.Count; i++)
        {
            Destroy(pooledEnemies[i].gameObject);
        }
        pooledEnemies.Clear();
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
        var gameObject = (GameObject)Instantiate(enemyPrefabs[index]);
        var enemy = gameObject.GetComponent<Enemy>();
        enemy.SetOriginPool(this);
        return gameObject;
    }

    private GameObject GetPooledEnemy()
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

    public GameObject SpawnPooledEnemy(Vector3 scale, Vector3 position)
    {
        var obj = GetPooledEnemy();
        obj.transform.localScale = scale;
        obj.transform.position = position;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pooledEnemies.Remove(obj);
        pooledEnemies.Insert(Random.Range(0, pooledEnemies.Count), obj);
    }
}
