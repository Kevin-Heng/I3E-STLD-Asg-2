using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Gun
{
    public float decreaseSpeed;
    Player player;
    public void ReduceSpeed(Player player)
    {      
        if (isEquipped)
        {
            float currentSpeed = player.GetComponent<FirstPersonController>().MoveSpeed;
            currentSpeed -= decreaseSpeed;
            player.GetComponent<FirstPersonController>().MoveSpeed = currentSpeed;
        }
        else
        {
            float currentSpeed = player.GetComponent<FirstPersonController>().MoveSpeed;
            currentSpeed += decreaseSpeed;
            player.GetComponent<FirstPersonController>().MoveSpeed = currentSpeed;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        isEquipped = false;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
        Reloading();
        OutOfAmmo();
    }
}
