using UnityEngine;
using System.Collections.Generic;

public class PoolBoy : MonoBehaviour
{
    public static PoolBoy Instance { get; protected set; }

    [SerializeField]
    private List<PrefabPool> prefabPools;

    private Dictionary<GameObject, PrefabPool> gameObjectToPrefabPool = new Dictionary<GameObject, PrefabPool>();
    private Dictionary<string, GameObject> nameToPrefab = new Dictionary<string, GameObject>();

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
            nameToPrefab.Add(prefabPool.Prefab.name, prefabPool.Prefab);
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
        string gameObjectName = go.name;

        if (nameToPrefab.ContainsKey(go.name))
        {
            gameObjectToPrefabPool[nameToPrefab[gameObjectName]].Despawn(go);
        }
    }
}
