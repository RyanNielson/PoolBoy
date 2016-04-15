using UnityEngine;
using System.Collections.Generic;

public class PoolBoy : MonoBehaviour
{
    public static PoolBoy Instance { get; protected set; }

    [SerializeField]
    private List<PrefabPool> prefabPools;

    private Dictionary<GameObject, PrefabPool> gameObjectToPrefabPool = new Dictionary<GameObject, PrefabPool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePrefabPools();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePrefabPools()
    {
        foreach (PrefabPool prefabPool in prefabPools)
        {
            prefabPool.Initialize(Instance);
            gameObjectToPrefabPool.Add(prefabPool.Prefab, prefabPool);
        }
    }

    public GameObject Spawn(GameObject go, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion))
    {
        if (gameObjectToPrefabPool.ContainsKey(go))
        {
            GameObject newGameObject = gameObjectToPrefabPool[go].Spawn();

            if (newGameObject != null)
            {
                newGameObject.transform.position = position;
                newGameObject.transform.rotation = rotation;
                newGameObject.SetActive(true);

                return newGameObject;
            }
        }

        return null;
    }

    public void Despawn(GameObject go)
    {
        PoolObject poolObject = go.GetComponent<PoolObject>();

        if (poolObject == null)
        {
            Destroy(go);
        }
        else
        {
            poolObject.Pool.Despawn(go);
        }
    }

    private void OnValidate()
    {
        foreach (PrefabPool prefabPool in prefabPools)
        {
            prefabPool.RefreshName();
        }
    }
}
