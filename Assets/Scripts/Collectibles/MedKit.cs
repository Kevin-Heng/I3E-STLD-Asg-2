/*
 * Author: Kevin Heng
 * Date: 19/06/2024
 * Description: The MedKit class is used to increase player health when pick up med kit
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : Interact
{
    /// <summary>
    /// how much hp does a med kit heal
    /// </summary>
    public int healHpAmt;

    /// <summary>
    /// player camera
    /// </summary>
    [SerializeField] Transform fpsCam;

    /// <summary>
    /// Function to pick up med kit
    /// </summary>
    public override void InteractObject()
    {
        if (GameManager.Instance.playerHp < GameManager.Instance.originalPlayerHp) //check if current player hp is lower than original player hp
        {
            HealPlayer();
            base.InteractObject();
        }
    }
    /// <summary>
    /// Function to calculate how much hp to heal player
    /// </summary>
    public void HealPlayer()
    {
        int hpDiff = GameManager.Instance.originalPlayerHp - GameManager.Instance.playerHp; //difference in original hp and current hp 
        if(hpDiff >= healHpAmt) //difference is more than what one med kit can heal
        {
            GameManager.Instance.playerHp += healHpAmt; //increase player hp by max heal of a med kit
        }
        else //difference is less than what one med kit can heal
        {
            GameManager.Instance.playerHp += hpDiff; //increase player hp by the difference
            
        }
        AudioManager.Instance.pickUpMedKit.Play(); //audio source for when med kit is picked up
        if(GameManager.Instance.playerHp > 30) //change text colour to white if player is not in danger
        {
            GameManager.Instance.playerHpText.color = Color.white;
        }
        GameManager.Instance.playerHpText.text = GameManager.Instance.playerHp.ToString(); //update player hp text 

    }

    // Start is called before the first frame update
    void Start()
    {
        if (fpsCam == null) //since projectile game object is not in scene, this is needed to set the fpsCam
        {
            fpsCam = Camera.main.transform; //set player camera
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
