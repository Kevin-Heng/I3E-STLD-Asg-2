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
    public GameObject projectile;
    int damage = 10;
    public AudioClip playerHit;
    public AudioClip playerDie;
    Transform fpsCam;

  
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit");
            Destroy(projectile);
            GameManager.Instance.ReducePlayerHp(damage,playerHit,fpsCam);
            if(GameManager.Instance.playerHp == 0)
            {
                AudioSource.PlayClipAtPoint(playerDie, fpsCam.position, 1f);
            }
            Debug.Log(GameManager.Instance.playerHp);

        }
        else
        {
            Destroy(projectile, 1.25f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(fpsCam == null)
        {
            fpsCam = Camera.main.transform;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
