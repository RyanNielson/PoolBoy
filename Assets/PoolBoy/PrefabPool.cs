using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PrefabPool
{
    [SerializeField]
    private GameObject prefab;
    public GameObject Prefab
    {
        get { return prefab; }
    }

    [SerializeField]
    private int preloadQuantity = 5;
    public int PreloadQuantity
    {
        get { return preloadQuantity; }
    }

    private Stack<GameObject> gameObjectPool = new Stack<GameObject>();

    private PoolBoy poolBoy;

    private int spawnedCount = 0;

    public void Initialize(PoolBoy poolBoy)
    {
        this.poolBoy = poolBoy;
        gameObjectPool = new Stack<GameObject>(PreloadQuantity);
        InstantiateGameObjects(PreloadQuantity);
    }

    public GameObject Spawn()
    {
        if (gameObjectPool.Count > 0)
        {
            spawnedCount++;
            return gameObjectPool.Pop();
        }

        return null;
    }

    public void Despawn(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.SetParent(poolBoy.transform);

        gameObjectPool.Push(gameObject);

        spawnedCount--;
    }

    private void InstantiateGameObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject gameObject = GameObject.Instantiate(prefab);

            gameObject.name = prefab.name;
            gameObject.transform.SetParent(poolBoy.transform);
            gameObject.SetActive(false);
            gameObject.AddComponent<PoolObject>().Pool = this;

            gameObjectPool.Push(gameObject);
        }
    }
}
