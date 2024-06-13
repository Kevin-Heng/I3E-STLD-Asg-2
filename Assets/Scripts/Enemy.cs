using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask Player;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool attack;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public GameObject projectile;
    public Transform projectileSpawn;
    GameObject spawnProjectile;

    public int enemyHp = 50;

    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    void Patrol()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

     void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(walkPoint, -transform.up, 2f))
        {
            walkPointSet = true;
        }

    }
    void Chase()
    {
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        agent.SetDestination(transform.position);

        agent.transform.LookAt(player);

        if (!attack)
        {
            spawnProjectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
            Rigidbody projectileRb = spawnProjectile.GetComponent<Rigidbody>();

            projectileRb.AddForce(transform.forward * 18, ForceMode.Impulse);
            projectileRb.AddForce(transform.up * 4, ForceMode.Impulse);

            attack = true;
            StartCoroutine(ResetAttack());
            
        }
    }


    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        attack = false;
    }





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);
        if(!playerInSightRange && !playerInAttackRange)
        {
            Patrol();
        }
        if(playerInSightRange && !playerInAttackRange)
        {
            Chase();
        }
        if(playerInSightRange &&  playerInAttackRange)
        {
            Attack();
        }
    }
}
