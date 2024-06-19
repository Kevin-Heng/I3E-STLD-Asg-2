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
    public int healHpAmt;
    public AudioClip pickUp;
    [SerializeField] Transform fpsCam;

    public override void InteractObject()
    {
        if (GameManager.Instance.playerHp < 100)
        {
            HealPlayer();
            base.InteractObject();
        }
    }
    public void HealPlayer()
    {
        int hpDiff = 100 - GameManager.Instance.playerHp; 
        if(hpDiff >= healHpAmt)
        {
            GameManager.Instance.playerHp += healHpAmt;
        }
        else
        {
            healHpAmt -= hpDiff;
            GameManager.Instance.playerHp += healHpAmt;
            
        }
        AudioSource.PlayClipAtPoint(pickUp, fpsCam.position, 0.7f);
        Debug.Log(GameManager.Instance.playerHp);

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
