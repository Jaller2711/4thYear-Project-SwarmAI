using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoidAgent : MonoBehaviour
{
    public Transform leader; // Reference to the leader
    public float separationDistance = 2.0f; // Minimum separation between agents
    public float alignmentWeight = 1.0f; // Weight for alignment behavior
    public float cohesionWeight = 1.0f; // Weight for cohesion behavior
    public float separationWeight = 1.5f; // Weight for separation behavior
    public float flockRadius = 5.0f; // Radius to check for nearby agents

    private NavMeshAgent agent;
    private List<BoidAgent> neighbors = new List<BoidAgent>();

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Get nearby agents
        neighbors = GetNearbyAgents();

        // Calculate the final velocity based on Boid rules
        Vector3 separation = CalculateSeparation() * separationWeight;
        Vector3 alignment = CalculateAlignment() * alignmentWeight;
        Vector3 cohesion = CalculateCohesion() * cohesionWeight;

        // Calculate the flocking direction
        Vector3 flockingDirection = separation + alignment + cohesion;

        // Set the agent destination as the leader position plus the flocking adjustment
        Vector3 destination = leader.position + flockingDirection;
        agent.SetDestination(destination);
    }

    // Find nearby BoidAgents within the flock radius
    private List<BoidAgent> GetNearbyAgents()
    {
        List<BoidAgent> nearbyAgents = new List<BoidAgent>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, flockRadius);
        foreach (Collider col in colliders)
        {
            BoidAgent boid = col.GetComponent<BoidAgent>();
            if (boid != null && boid != this)
            {
                nearbyAgents.Add(boid);
            }
        }
        return nearbyAgents;
    }

    // Separation: Avoid crowding nearby agents
    private Vector3 CalculateSeparation()
    {
        Vector3 separationForce = Vector3.zero;
        int count = 0;

        foreach (BoidAgent neighbor in neighbors)
        {
            float distance = Vector3.Distance(transform.position, neighbor.transform.position);
            if (distance < separationDistance)
            {
                Vector3 direction = transform.position - neighbor.transform.position;
                separationForce += direction.normalized / distance;
                count++;
            }
        }

        if (count > 0)
        {
            separationForce /= count;
        }

        return separationForce;
    }

    // Alignment: Move in the same direction as the average direction of nearby agents
    private Vector3 CalculateAlignment()
    {
        Vector3 averageDirection = Vector3.zero;
        int count = 0;

        foreach (BoidAgent neighbor in neighbors)
        {
            averageDirection += neighbor.agent.velocity;
            count++;
        }

        if (count > 0)
        {
            averageDirection /= count;
        }

        return averageDirection.normalized;
    }

    // Cohesion: Move towards the average position of nearby agents
    private Vector3 CalculateCohesion()
    {
        Vector3 averagePosition = Vector3.zero;
        int count = 0;

        foreach (BoidAgent neighbor in neighbors)
        {
            averagePosition += neighbor.transform.position;
            count++;
        }

        if (count > 0)
        {
            averagePosition /= count;
            return (averagePosition - transform.position).normalized;
        }

        return Vector3.zero;
    }

    // A public method to assign a leader dynamically
    public void SetLeader(Transform newLeader)
    {
        leader = newLeader;
    }
}

