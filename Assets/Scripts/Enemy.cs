/*
 * Author: Main code taken from here: https://youtu.be/UjkSFoLxesw?si=l8DT8gSOf4ERmiWB, Kevin Heng
 * Date: 13/06/2024
 * Description: The Enemy class is used to handle enemy movement and attacks against the player
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("AI")]
    /// <summary>
    /// Enemy Model
    /// </summary>
    public NavMeshAgent agent;
    /// <summary>
    /// Player capsule transform component
    /// </summary>
    public Transform player;
    /// <summary>
    /// Mask to only attack enemy
    /// </summary>
    public LayerMask Player;

    [Header("")]
    /// <summary>
    /// player camera
    /// </summary>
    [SerializeField] Transform fpsCam;

    [Header("Walkpoint")]
    /// <summary>
    /// Enemy walking destination
    /// </summary>
    public Vector3 walkPoint;
    /// <summary>
    /// Boolean to check if enemy walk point is set
    /// </summary>
    bool walkPointSet;
    /// <summary>
    /// Enemy walk point range
    /// </summary>
    public float walkPointRange;

    [Header("Attack")]
    /// <summary>
    /// Attack cooldown
    /// </summary>
    public float timeBetweenAttacks;
    /// <summary>
    /// Boolean to check if enemy is attacking
    /// </summary>
    bool attack;

    [Header("Sight range")]
    /// <summary>
    /// Distance at which enemy spots player
    /// </summary>
    public float sightRange;
    /// <summary>
    /// Distance at which enemy can attack player
    /// </summary>
    public float attackRange;
    /// <summary>
    /// Boolean to check if player is in enemy sight range
    /// </summary>
    public bool playerInSightRange;
    /// <summary>
    /// Boolean to check if player is in enemy attack range
    /// </summary>
    public bool playerInAttackRange;

    [Header("Projectile")]
    /// <summary>
    /// damage done by projectile to player
    /// </summary>
    public int projectileDamage;
    /// <summary>
    /// Projectile when enemy attacks
    /// </summary>
    public GameObject projectile;
    /// <summary>
    /// Position where projectile is to be spawned
    /// </summary>
    public Transform projectileSpawn;
    /// <summary>
    /// Variable to store instantiated projectile info
    /// </summary>
    GameObject spawnProjectile;
    /// <summary>
    /// Forward force to launch projectile
    /// </summary>
    public int forwardForce;
    /// <summary>
    /// Upward force to launch projectile
    /// </summary>
    public int upwardForce;

    [Header("HP")]
    /// <summary>
    /// Enemy hp
    /// </summary>
    public int enemyHp;

    /// <summary>
    /// Enemy original hp
    /// </summary>
    public int originalEnemyHp;
    /// <summary>
    /// text to update enemy hp
    /// </summary>
    public TextMeshProUGUI enemyHpText;


    /// <summary>
    /// To set player and agent variables
    /// </summary>
    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    /// <summary>
    /// Function for when enemy is walking around
    /// </summary>
    void Patrol()
    {
        if (!walkPointSet) //enemy looks for walk point if it is not set
        {
            SearchWalkPoint();
        }
        if (walkPointSet) //enemy moves to destination after walk point is set
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint; //difference in distance between enemy and destination

        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false; //reset walk point if difference less than 1
        }
    }

    /// <summary>
    /// Function for enemy to look for destination
    /// </summary>
    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange); //get random z value to add to walkPoint
        float randomX = Random.Range(-walkPointRange, walkPointRange); //get random x value to add to walkPoint

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); //enemy destination
        if(Physics.Raycast(walkPoint, -transform.up, 2f)) //check if end destination is on the ground
        {
            walkPointSet = true; 
        }

    }
    /// <summary>
    /// Function for enemy to walk towards player
    /// </summary>
    void Chase()
    {
        agent.SetDestination(player.position);  //enemy moves towards player's position
    }

    /// <summary>
    /// Function for enemy to attack player
    /// </summary>
    void Attack()
    {
        agent.SetDestination(transform.position); //enemy moves towards player's position

        agent.transform.LookAt(player); //front faces player

        if (!attack) //enemy attack
        {
            spawnProjectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation); //projectile spawns
            spawnProjectile.GetComponent<Projectile>().damage = projectileDamage; //set damage

            Rigidbody projectileRb = spawnProjectile.GetComponent<Rigidbody>(); //access rigidbody component of projectile

            projectileRb.AddForce(transform.forward * forwardForce, ForceMode.Impulse); //launch projectile forward
            projectileRb.AddForce(transform.up * upwardForce, ForceMode.Impulse); //launch projectile upward

            attack = true;
            StartCoroutine(ResetAttack());
            
        }
    }

    /// <summary>
    /// Attack cooldown
    /// </summary>
    /// <returns></returns>
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        GameManager.Instance.bleedingFrame.SetActive(false); //bleeding UI turns off
        attack = false;
    }




    // Start is called before the first frame update
    void Start()
    {
        enemyHp = originalEnemyHp; //set enemy hp
        enemyHpText.text = enemyHp.ToString() + "/" + originalEnemyHp.ToString(); //update enemy hp text
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player); //check if player is within the radius of sight range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player); //check if player is within the radius of attack range
        if(!playerInSightRange && !playerInAttackRange) //player is not within sight and attack range
        {
            Patrol();
        }
        if(playerInSightRange && !playerInAttackRange) //player is within sight range but not attack range
        {
            Chase();
        }
        if(playerInSightRange &&  playerInAttackRange) //player is within sight range and attack range
        {
            Attack();
        }
    }
}
