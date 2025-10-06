using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyPathfind : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;
    private bool isPatrolling;

    public float currentCooldown;
    public GameObject projectile;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void OnTriggerStay(Collider other)
    {
        // Check if the colliding GameObject has the specified tag
        if (other.gameObject.CompareTag("Player"))
        {
            if(projectile == null && currentCooldown <=0f)
            {
                Debug.Log("Dealing " + GetComponent<EnemyActor>().enemyType.damage + " Damage to: " + other.GetComponent<PlayerHealthManager>() + " Object");
                other.GetComponent<PlayerHealthManager>().dealDamage(-GetComponent<EnemyActor>().enemyType.damage);
                currentCooldown = GetComponent<EnemyActor>().enemyType.attackSpeed;
            }
        }
        return;
    }

    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;

    }

    private void Update()
    {
        currentCooldown -= Time.deltaTime;
        Collider[] sightHits = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer);
        playerInSightRange = false;

        foreach (Collider hit in sightHits)
        {
            Vector3 directionToTarget = (hit.transform.position - transform.position).normalized;

            // Check if target is within forward-facing semisphere (90° for half-sphere)
            float angle = Vector3.Angle(transform.forward, directionToTarget);
            if (angle < 90f / 2f) // adjust 90f to whatever forward field-of-view you want
            {
                playerInSightRange = true;
                break;
            }
        }
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        
        if (!playerInSightRange && !playerInAttackRange && !isPatrolling) StartCoroutine(PatrolRoutine());
        if(playerInSightRange &&  !playerInAttackRange) chasePlayer();
        if(playerInAttackRange && playerInSightRange) attackPlayer();
    }
    private IEnumerator PatrolRoutine()
    {
        isPatrolling = true;

        // Wait a random interval before moving
        float waitTime = Random.Range(1f, 5f);
        yield return new WaitForSeconds(waitTime);

        Patroling();

        isPatrolling = false; // allows next patrol scheduling
    }
    private void Patroling()
    {
        if (!walkPointSet) searchWalkPoint();
        
        if(walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }
    private void searchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX,transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
    }
    private void chasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void attackPlayer()
    {
        agent.SetDestination(transform.position);

        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPos);

        if (currentCooldown <= 0f)
        {

            //Attack Code Here
            if (projectile != null)
            {
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 8f, ForceMode.Impulse);
                currentCooldown = GetComponent<EnemyActor>().enemyType.attackSpeed;
            }
        }
    }
}
