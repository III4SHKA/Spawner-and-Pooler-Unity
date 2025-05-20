using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // List of prefabs to spawn and pool
    public List<GameObject> objects = new List<GameObject>();

    // Time interval between spawns
    [SerializeField] float TimeOfSpawn;

    // Timer helper to show time remaining until next spawn
    public float currentTimeSpeed;

    // Time at which the next object should spawn
    public float NextSpawn = 0.0f;

    // Number of copies of each object to pre-instantiate for pooling
    [SerializeField] float copyOfObj;

    void Start()
    {
        // Initialize spawn timer
        NextSpawn -= 3;

        // Pre-instantiate and pool each object
        foreach (var obj in objects)
        {
            for (int i = 0; i < copyOfObj; i++)
            {
                GameObject objInstance = Instantiate(obj, Vector2.zero, Quaternion.identity);
                objInstance.SetActive(false); // Disable before pooling
                ObjectPooler.Instance.AddToPool(obj, objInstance);
            }
        }
    }

    void Update()
    {
        // Track time remaining until next spawn
        currentTimeSpeed = NextSpawn - Time.time;

        // Time to spawn a new object
        if (Time.time > NextSpawn)
        {
            NextSpawn = Time.time + TimeOfSpawn;

            Vector2 spawnPosition = new Vector2(0, 3);
            Quaternion rotation = Quaternion.identity;

            // Spawn the first prefab from the list
            GameObject prefab = objects[0];
            ObjectPooler.Instance.Spawn(prefab, spawnPosition, rotation);
        }
    }
}
