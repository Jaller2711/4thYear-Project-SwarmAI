using UnityEngine;
using UnityEngine.AI;

public class LeaderMovement : MonoBehaviour
{
    private NavMeshAgent leaderAgent;
    public Transform[] targets; // Array of targets to switch between
    private int currentTargetIndex = 0;

    //public Transform aiTarget;    // The target for autonomous movement
    public bool isPlayerControlled = false;  // Toggle between AI and player control
    private Vector3 targetPosition;
    public float movementSpeed = 10f; // Desired movement speed

    void Start()
    {
        leaderAgent = GetComponent<NavMeshAgent>();

        // Move to the first target initially
        if (targets.Length > 0)
        {
            leaderAgent.SetDestination(targets[currentTargetIndex].position);
        }

        // Initialize the target position to the current position initially
        targetPosition = transform.position;

        // Set the movement speed of the agent
        leaderAgent.speed = movementSpeed;
        leaderAgent.acceleration = 12f; // Increases how fast the agent accelerates
        leaderAgent.angularSpeed = 360f; // Increases how fast the agent can turn
    }

    void Update()
    {
        leaderAgent.speed = movementSpeed;//To change it in real-time from the Inspector

        if (leaderAgent != null && leaderAgent.isActiveAndEnabled && leaderAgent.isOnNavMesh)
        {
            leaderAgent.SetDestination(targetPosition);
        }
        else
        {
            Debug.LogWarning("NavMeshAgent is not active, enabled, or placed on a NavMesh.");
        }
        
        // Press 'C' to switch between AI and player control
        if (Input.GetKeyDown(KeyCode.C))
        {
            isPlayerControlled = !isPlayerControlled;
        }

        // Check if the agent is controlled by the player or AI
        if (isPlayerControlled)
        {
            HandlePlayerInput();  // Player input control
        }
        else
        {
            //MoveToAITarget();     // Autonomous movement
        }
        
        // Check for user input to switch targets
        if (Input.GetKeyDown(KeyCode.Space)) // Press space to switch target
        {
            SwitchTarget();
        }
        // Check if the leader has reached the current target
        if (!leaderAgent.pathPending && leaderAgent.remainingDistance <= leaderAgent.stoppingDistance)
        {
            // Switch to the next target automatically upon reaching the current one
            SwitchTarget();
        }
        

    }
    
    // Method to switch to the next target
    void SwitchTarget()
    {
        if (targets.Length == 0) return;

        // Increment the target index, looping back to 0 if it exceeds the number of targets
        currentTargetIndex = (currentTargetIndex + 1) % targets.Length;

        // Set the destination to the new target
        leaderAgent.SetDestination(targets[currentTargetIndex].position);

        /*if (targets.Length == 0) return;

        // Pick a random target
        currentTargetIndex = Random.Range(0, targets.Length);

        // Set the destination to the new target
        leaderAgent.SetDestination(targets[currentTargetIndex].position);
        */
    }
    /*
    void MoveToAITarget()
    {
        // Move the NavMeshAgent to the AI target position (e.g., an enemy, waypoint, etc.)
        if (aiTarget != null && leaderAgent.isOnNavMesh)
        {
            leaderAgent.SetDestination(aiTarget.position);
        }
    }*/

    void HandlePlayerInput()
    {
        // Example of moving the agent using the mouse click input
        if (Input.GetMouseButtonDown(0))  // Left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;  // Set target position to the clicked point

                if (leaderAgent.isOnNavMesh)
                {
                    leaderAgent.SetDestination(targetPosition);
                }
            }
        }

        // Optional: Movement with keyboard (WASD or Arrow keys)
        HandleKeyboardMovement();
    }

    void HandleKeyboardMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (moveX != 0 || moveZ != 0)
        {
            // Move the NavMeshAgent in the direction of the input
            Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

            // Calculate the target position based on the input direction and speed
            Vector3 moveTarget = transform.position + moveDirection * leaderAgent.speed * Time.deltaTime;

            if (leaderAgent.isOnNavMesh)
            {
                leaderAgent.SetDestination(moveTarget);
            }
        }
    }

}
