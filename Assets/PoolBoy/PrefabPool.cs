using UnityEngine;

using System;
using System.Collections.Generic;

[Serializable]
public class PrefabPool
{
    [SerializeField, HideInInspector]
    private string name = "Prefab Pool";

    [SerializeField]
    private GameObject prefab;
    public GameObject Prefab
    {
        get { return prefab; }
    }

    [SerializeField]
    private int quantityToPreload = 5;

    [SerializeField]
    private int quantityToLoadIfEmpty = 5;

    public int SpawnedCount
    {
        get { return spawnedGameObjects.Count; }
    }

    private Queue<GameObject> availableGameObjects = new Queue<GameObject>();

    private HashSet<GameObject> spawnedGameObjects = new HashSet<GameObject>();

    private PoolBoy poolBoy;

    public void Initialize(PoolBoy poolBoy)
    {
        this.poolBoy = poolBoy;
        InstantiateGameObjects(quantityToPreload);
    }

    public GameObject Spawn()
    {
        if (availableGameObjects.Count <= 0)
        {
            InstantiateGameObjects(quantityToLoadIfEmpty);
        }

        if (availableGameObjects.Count > 0)
        {
            GameObject gameObjectToSpawn = availableGameObjects.Dequeue();

            spawnedGameObjects.Add(gameObjectToSpawn);

            return gameObjectToSpawn;
        }

        return null;
    }

    public void Despawn(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.SetParent(poolBoy.transform);
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;

        spawnedGameObjects.Remove(gameObject);
        availableGameObjects.Enqueue(gameObject);
    }

    public void RefreshName()
    {
        name = Prefab ? Prefab.name : "Prefab Pool";
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

            availableGameObjects.Enqueue(gameObject);
        }
    }
}
