/*
 * Author: Kevin Heng
 * Date: 30/06/2024
 * Description: The Deathzone class is used to kill player when he falls off the map and can respawn
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathzone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.playerHp = 0;
        GameManager.Instance.DeathScreen();
        GameManager.Instance.playerUI.SetActive(false);
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
