using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // Singleton instance for global access
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Internal Pool class to manage a group of inactive objects
    class Pool
    {
        public List<GameObject> inactive = new List<GameObject>();
        private GameObject prefab;
        private int initialSize;

        public Pool(GameObject prefab, int initialSize)
        {
            this.prefab = prefab;
            this.initialSize = initialSize;
        }

        // Spawn an object from the pool or create a new one if empty
        public GameObject Spawn(Vector2 pos, Quaternion rot)
        {
            GameObject obj;

            if (inactive.Count == 0)
            {
                // No inactive objects, instantiate a new one
                obj = Instantiate(prefab, pos, rot);
                obj.name = prefab.name;
                obj.transform.SetParent(Instance.transform);
            }
            else
            {
                // Reuse the last inactive object
                obj = inactive[inactive.Count - 1];
                inactive.RemoveAt(inactive.Count - 1);
            }

            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.SetActive(true);
            return obj;
        }

        // Return an object back to the pool
        public void Despawn(GameObject obj)
        {
            obj.SetActive(false);
            inactive.Add(obj);
        }

        // Add an already instantiated object to the pool
        public void AddToPool(GameObject obj)
        {
            obj.SetActive(false);
            inactive.Add(obj);
        }
    }

    // Dictionary mapping prefab names to their corresponding pools
    private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

    // Initialize a pool for a given prefab if it doesn't exist
    void Init(GameObject prefab, int initialSize)
    {
        if (prefab != null && !pools.ContainsKey(prefab.name))
        {
            pools[prefab.name] = new Pool(prefab, initialSize);
        }
    }

    // Public method to spawn an object from the pool
    public GameObject Spawn(GameObject prefab, Vector2 pos, Quaternion rot)
    {
        Init(prefab, 1); // Initialize if needed
        GameObject obj = pools[prefab.name].Spawn(pos, rot);
        obj.name = prefab.name; // Clean name (remove "(Clone)")
        return obj;
    }

    // Public method to return an object to the pool
    public void Despawn(GameObject obj)
    {
        // Remove "(Clone)" from the object name
        string cleanName = obj.name.Replace("(Clone)", "").Trim();

        if (pools.ContainsKey(cleanName))
        {
            pools[cleanName].Despawn(obj);
        }
        else
        {
            Destroy(obj); // If no matching pool, destroy it
        }
    }

    // Add a prefab instance to the appropriate pool
    public void AddToPool(GameObject prefab, GameObject obj)
    {
        Init(prefab, 1);
        pools[prefab.name].AddToPool(obj);
    }
}
