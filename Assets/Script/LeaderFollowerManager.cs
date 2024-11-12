using UnityEngine;

public class LeaderFollowerManager : MonoBehaviour
{
    public GameObject leaderPrefab;  // The leader prefab reference
    public GameObject UnitsPrefab; // Prefab of the follower
    public int numberOfFollowers = 5; // Number of followers to spawn
    public float spacingBetweenFollowers = 2f; // How much distance between each follower
    //public Transform[] targets; // Array of targets to switch between
    //public LeaderMovement leaderMovement;

    private GameObject leaderInstance;

    void Start()
    {
        // Spawn the leader and store the instance
        leaderInstance = Instantiate(leaderPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        // Spawn followers and assign the leader to them
        SpawnFollowers();

        //leaderMovement.SwitchTarget(targets);
    }

    // Function to spawn followers dynamically
    void SpawnFollowers()
    {
        for (int i = 0; i < numberOfFollowers; i++)
        {
            // Calculate the position for each follower behind the leader
            Vector3 spawnPosition = leaderInstance.transform.position - leaderInstance.transform.forward * (spacingBetweenFollowers * (i + 1));

            // Instantiate the follower prefab
            GameObject newFollower = Instantiate(UnitsPrefab, spawnPosition, Quaternion.identity);

            // Assign the leader to the follower
            BoidAgent followerScript = newFollower.GetComponent<BoidAgent>();
            followerScript.SetLeader(leaderInstance.transform);  // Pass the leader's transform
        }
    }
}
