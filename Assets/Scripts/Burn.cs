/*
 * Author: Kevin Heng
 * Date: 26/06/2024
 * Description: The Burn class is used to damage the player when he touches something that burns enemy
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
    /// <summary>
    /// Projectile damage if it hits player
    /// </summary>
    public int burnDamage;
    
    /// <summary>
    /// Time taken to burn player once
    /// </summary>
    public float burnTime;
    /// <summary>
    /// Time when player will get burned again
    /// </summary>
    public static float nextTimeToBurn = 0;

    /// <summary>
    /// Duration of burn DOT
    /// </summary>
    public float burnDuration;

    /// <summary>
    /// Time between burn DOT
    /// </summary>
    public float burnInterval;

    /// <summary>
    /// Player stays within burn trigger
    /// </summary>
    /// <param name="other"> player </param>
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!GameManager.Instance.burning && GameManager.Instance.playerHp > 0 && GameManager.Instance.playerHp != GameManager.Instance.originalPlayerHp) //check if player is not currently burning and hp more than 0
            {
                BurnDamage();
            }
        }
    }

    /// <summary>
    /// Burn DOT when player exits trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (!GameManager.Instance.burning && GameManager.Instance.playerHp > 0) //check if player is not currently burning and hp more than 0
            {
                StartCoroutine(BurnDOT());
            }
        }
    }

    /// <summary>
    /// Function to burn player
    /// </summary>
    public void BurnDamage()
    {
        if(Time.time >= nextTimeToBurn && GameManager.Instance.playerHp > 0) //player gets burned if current game time is more than or equal to nextTimeToBurn and hp more than 0
        {
            GameManager.Instance.burning = true; //player is burning
            GameManager.Instance.burningFrame.SetActive(true);
            nextTimeToBurn = Time.time + 1/burnTime; //time when will player get burned next

            GameManager.Instance.ReducePlayerHp(burnDamage, AudioManager.Instance.playerBurn, AudioManager.Instance.playerDie, GameManager.Instance.fpsCam);
            GameManager.Instance.playerHpText.text = GameManager.Instance.playerHp.ToString(); //update player hp
            GameManager.Instance.burning = false;
        }
        
        
    }

    /// <summary>
    /// Function for Burn DOT
    /// </summary>
    /// <returns></returns>
    IEnumerator BurnDOT()
    {
        float endTime = Time.time + burnDuration; //time when DOT ends
        GameManager.Instance.burning = true;
        while (Time.time < endTime && GameManager.Instance.playerHp > 0) //when current game time is less than end time and player hp more than 0
        {
            GameManager.Instance.burningFrame.SetActive(true); //player is burning
            GameManager.Instance.ReducePlayerHp(burnDamage, AudioManager.Instance.playerBurn, AudioManager.Instance.playerDie, GameManager.Instance.fpsCam);
            GameManager.Instance.playerHpText.text = GameManager.Instance.playerHp.ToString(); //update player hp
            yield return new WaitForSeconds(burnInterval);
        }
        //player stops burning and turn off UI
        GameManager.Instance.burning = false;
        GameManager.Instance.burningFrame.SetActive(false);
    }
    


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
