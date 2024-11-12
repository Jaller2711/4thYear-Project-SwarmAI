using UnityEngine;
using System.Collections.Generic;

public class SwarmManager : MonoBehaviour
{
    public GameObject agentPrefab;
    public GameObject leaderPrefab;
    public int agentCount = 20;
    public Vector3 spawnArea = new Vector3(10, 10, 10);
    public List<Transform> initialTargets; // List of initial targets for the leader

    private SwarmLeader leader;

    void Start()
    {
        // Spawn the leader
        Vector3 leaderSpawnPosition = transform.position + Random.insideUnitSphere * 2;
        GameObject leaderObj = Instantiate(leaderPrefab, leaderSpawnPosition, Quaternion.identity);
        leader = leaderObj.GetComponent<SwarmLeader>();

        // Assign initial targets to the leader
        if (leader != null && initialTargets != null && initialTargets.Count > 0)
        {
            leader.targets = new List<Transform>(initialTargets);
        }

        // Spawn the agents around the leader
        for (int i = 0; i < agentCount; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                Random.Range(-spawnArea.y / 2, spawnArea.y / 2),
                Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
            );

            Instantiate(agentPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Public method to change the leader's target dynamically
    public void ChangeLeaderTarget(Transform newTarget)
    {
        if (leader != null)
        {
            leader.SetTarget(newTarget);
        }
    }
}
