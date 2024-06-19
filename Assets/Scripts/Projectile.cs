/*
 * Author: Kevin Heng
 * Date: 13/06/2024
 * Description: The Projectile class is used to handle the projectile properties when it is launched by the enemy attack
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Projectile to be launched when enemy attacks
    /// </summary>
    public GameObject projectile;
    /// <summary>
    /// Projectile damage if it hits player
    /// </summary>
    public int damage;
    /// <summary>
    /// Audio played when enemy is hit by projectile
    /// </summary>
    public AudioClip playerHit;
    /// <summary>
    /// Audio played when player dies
    /// </summary>
    public AudioClip playerDie;
    /// <summary>
    /// Player camera
    /// </summary>
    Transform fpsCam;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player")) //check if projectile hits player
        {
            Destroy(projectile);
            GameManager.Instance.ReducePlayerHp(damage, playerHit,  playerDie, fpsCam);
            Debug.Log(GameManager.Instance.playerHp);

        }
        else //projectile miss player
        {
            Destroy(projectile, 1.25f); //remove projectile from game
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(fpsCam == null) //since projectile game object is not in scene, this is needed to set the fpsCam
        {
            fpsCam = Camera.main.transform; //set player camera
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
