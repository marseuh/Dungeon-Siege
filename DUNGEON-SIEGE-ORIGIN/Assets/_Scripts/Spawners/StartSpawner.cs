using UnityEngine;

public class StartSpawner : BaseSpawner
{
    [SerializeField] private GameObject prefabToSpawn;

    void Start()
    {
        Spawn(prefabToSpawn);
    }
}