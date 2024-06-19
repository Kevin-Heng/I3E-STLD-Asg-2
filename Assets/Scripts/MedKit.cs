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

    public override void InteractObject()
    {
        HealPlayer();
        base.InteractObject();
    }
    public void HealPlayer()
    {
        if(GameManager.Instance.playerHp < 100)
        {
            GameManager.Instance.playerHp += healHpAmt;
        }
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
