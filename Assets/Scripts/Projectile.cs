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
    /// Projectile prefab to be launched when enemy attacks
    /// </summary>
    public GameObject projectile;

    /// <summary>
    /// Projectile damage if it hits player
    /// </summary>
    public int damage;

    /// <summary>
    /// Player camera
    /// </summary>
    Transform fpsCam;



    /// <summary>
    /// Function to damage player when projectile hits player
    /// </summary>
    /// <param name="collision"> player </param>
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player")) //check if projectile hits player
        {
            Destroy(projectile); 
            GameManager.Instance.ReducePlayerHp(damage, AudioManager.Instance.playerHit, AudioManager.Instance.playerDie, fpsCam);
            GameManager.Instance.playerHpText.text = GameManager.Instance.playerHp.ToString(); //update player hp
            GameManager.Instance.bleedingFrame.SetActive(true); //turn on bleeding UI
            if(GameManager.Instance.playerHp <= 30) //hp colour set to red when hp is equal or below 30
            {
                GameManager.Instance.playerHpText.color = Color.red; 
            }
            else //hp higher than 30 has white text
            {
                GameManager.Instance.playerHpText.color = Color.white;
            }
            if(GameManager.Instance.playerHp <= 0) //if player dies, death screen turns on
            {
                GameManager.Instance.DeathScreen();
            }

        }
        else //projectile miss player
        {
            Destroy(projectile, 1.25f); //remove projectile from game after 1.25s
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
