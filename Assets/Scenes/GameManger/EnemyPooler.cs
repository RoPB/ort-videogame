using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;

    private List<GameObject> pooledEnemies = new List<GameObject>();

    private int _currentLevel;
    private float _lastIncreasedScale = 0;

    public void Init(int level)
    {
        _currentLevel = level;
        _lastIncreasedScale = 0;
    }

    public void LevelChanged(int level)
    {
        if(_lastIncreasedScale==0)
            _lastIncreasedScale = GetSmallestSpawnedEnemyScale().x;

        _currentLevel = level;
        _lastIncreasedScale = level % 3 != 0 ? _lastIncreasedScale + 0.03f : _lastIncreasedScale;
        ScaleEnemies(1, new Vector3(_lastIncreasedScale, _lastIncreasedScale, 0));
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
        return gameObject;
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

    public GameObject SpawnPooledEnemy(GameObject obj, Vector3 position)
    {
        obj.SetActive(true);
        obj.transform.position = position;
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pooledEnemies.Remove(obj);
        pooledEnemies.Insert(Random.Range(0, pooledEnemies.Count), obj);
    }

    private void ScaleEnemies(int count, Vector3 scale)
    {
        var minScale = GetSmallestSpawnedEnemyScale();

        List<GameObject> smallerEnemiesToScale = pooledEnemies.Where(e => e.transform.localScale.Equals(minScale)).ToList();

        var scaledCount = 0; 

        while(scaledCount < count && scaledCount < smallerEnemiesToScale.Count)
        {
            smallerEnemiesToScale[scaledCount].transform.localScale = scale;
            scaledCount++;
        }

        while (scaledCount < count && scaledCount < pooledEnemies.Count)
        {
            scaledCount++;
            pooledEnemies[scaledCount].transform.localScale = scale;
        }
    }

    private Vector3 GetSmallestSpawnedEnemyScale()
    {
        if (pooledEnemies.Count == 0)
            return new Vector3(0, 0, 0);

        var minScale = new Vector3(int.MaxValue, int.MaxValue, 0);
        for (int i = 0; i < pooledEnemies.Count; i++)
        {
            if (pooledEnemies[i].transform.localScale.x <= minScale.x)
                minScale = pooledEnemies[i].transform.localScale;
        }
        return minScale;
    }
}
