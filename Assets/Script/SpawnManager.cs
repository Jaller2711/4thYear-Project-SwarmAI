using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject unitsPrefab;
    private float spawnRange = 9.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(unitsPrefab, transform.position, unitsPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(unitsPrefab, transform.position, unitsPrefab.transform.rotation);
        }*/
    }
    /*
    private Vector3 GenerateSpawnPosition()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            float spawnPosX = Random.Range(-spawnRange, spawnRange);
            float spawnPosZ = Random.Range(-spawnRange, spawnRange);

            Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
            return randomPos;
        }        
        
    }*/
}
