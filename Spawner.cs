using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs to spawn and pool")]
    public List<GameObject> objects = new List<GameObject>();

    [Header("Pooling settings")]
    [SerializeField] private int copiesPerObject = 10;

    void Start()
    {
        // Pre-instantiate and pool each object
        foreach (var obj in objects)
        {
            for (int i = 0; i < copiesPerObject; i++)
            {
                GameObject instance = Instantiate(obj, Vector2.zero, Quaternion.identity);
                instance.SetActive(false); // Disable before adding to pool
                ObjectPooler.Instance.AddToPool(obj, instance);
            }
        }
    }

    void Update()
    {
        // Spawn an object at the clicked/tapped screen position
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0f; // Ensure it's on the 2D plane

            GameObject prefabToSpawn = objects[Random.Range(0, objects.Count)];
            ObjectPooler.Instance.Spawn(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
