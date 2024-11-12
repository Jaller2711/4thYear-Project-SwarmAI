using UnityEngine;
using System.Collections.Generic;

public class SwarmLeader : MonoBehaviour
{
    public List<Transform> targets;    // List of targets to switch between
    public float speed = 5.0f;
    public float targetSwitchDistance = 1.5f;  // Distance at which to switch to the next target

    private int currentTargetIndex = 0;

    void Update()
    {
        if (targets == null || targets.Count == 0) return;

        // Move towards the current target
        Transform currentTarget = targets[currentTargetIndex];
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Check if the leader is close enough to switch to the next target
        if (Vector3.Distance(transform.position, currentTarget.position) < targetSwitchDistance)
        {
            SwitchToNextTarget();
        }
    }

    // Switch to the next target in the list
    private void SwitchToNextTarget()
    {
        currentTargetIndex = (currentTargetIndex + 1) % targets.Count;
    }

    // Public method to manually set a specific target
    public void SetTarget(Transform newTarget)
    {
        targets.Clear();
        targets.Add(newTarget);
        currentTargetIndex = 0;
    }
}
