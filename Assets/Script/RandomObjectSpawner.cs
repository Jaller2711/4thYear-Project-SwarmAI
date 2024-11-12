using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The prefab to spawn
    public int numberOfObjects = 10; // How many objects to spawn
    public Vector3 spawnAreaMin; // Minimum bounds for random spawning
    public Vector3 spawnAreaMax; // Maximum bounds for random spawning
    /*
    IEnumerator SpawnObjectsOverTime()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(1f); // Wait 1 second between spawns
        }
    }*/

    void Start()
    {
        //StartCoroutine(SpawnObjectsOverTime());
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Generate a random position within the specified bounds
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

            // Instantiate the object at the random position
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        }
    }
}