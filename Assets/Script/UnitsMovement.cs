using UnityEngine;
using System.Collections.Generic;

public class UnitsMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float neighborDistance = 5.0f;
    public float separationDistance = 2.0f;
    public float leaderFollowWeight = 1.5f;  // Weight for following the leader

    private SwarmLeader leader;
    private Vector3 alignmentVector;
    private Vector3 cohesionVector;
    private Vector3 separationVector;
    private Vector3 leaderFollowVector;

    private List<UnitsMovement> neighbors = new List<UnitsMovement>();

    void Start()
    {
        // Reference the leader, which could be set in the manager
        leader = FindObjectOfType<SwarmLeader>();
    }

    void Update()
    {
        CalculateSwarmBehavior();

        // Combined movement vector
        Vector3 moveDirection = (alignmentVector + cohesionVector + separationVector + leaderFollowVector * leaderFollowWeight).normalized;

        // Move the agent
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void CalculateSwarmBehavior()
    {
        alignmentVector = Vector3.zero;
        cohesionVector = Vector3.zero;
        separationVector = Vector3.zero;
        leaderFollowVector = Vector3.zero;
        neighbors.Clear();

        // Find all nearby agents
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, neighborDistance);
        foreach (var hitCollider in hitColliders)
        {
            UnitsMovement agent = hitCollider.GetComponent<UnitsMovement>();
            if (agent != null && agent != this)
            {
                neighbors.Add(agent);
            }
        }

        // Apply behaviors if there are nearby agents
        if (neighbors.Count > 0)
        {
            foreach (var neighbor in neighbors)
            {
                // Alignment: Average the direction of neighbors
                alignmentVector += neighbor.transform.forward;

                // Cohesion: Move towards the average position of neighbors
                cohesionVector += neighbor.transform.position;

                // Separation: Keep a distance from nearby agents
                Vector3 toNeighbor = transform.position - neighbor.transform.position;
                if (toNeighbor.magnitude < separationDistance)
                {
                    separationVector += toNeighbor / toNeighbor.magnitude;
                }
            }

            alignmentVector = alignmentVector.normalized;
            cohesionVector = (cohesionVector / neighbors.Count - transform.position).normalized;
            separationVector = separationVector.normalized;
        }

        // Leader following: Move towards the leader
        if (leader != null)
        {
            leaderFollowVector = (leader.transform.position - transform.position).normalized;
        }
    }
}
