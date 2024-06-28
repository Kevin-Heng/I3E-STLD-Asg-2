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
            Debug.Log("total heal: " + hpDiff);
            GameManager.Instance.playerHp += healHpAmt;
        }
        else //98 only need +2
        {
            Debug.Log(hpDiff);
            GameManager.Instance.playerHp += hpDiff;
            
        }
        AudioSource.PlayClipAtPoint(pickUp, fpsCam.position, 0.4f);
        if(GameManager.Instance.playerHp > 30)
        {
            GameManager.Instance.playerHpText.color = Color.white;
        }
        GameManager.Instance.playerHpText.text = GameManager.Instance.playerHp.ToString();

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
