/*
 * Author: Kevin Heng
 * Date: 10/06/2024
 * Description: The Player class is used to handle player interactions
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Reference Gun parent class
    /// </summary>
    Gun currentGun;

    Interact currentInteractable;
    [SerializeField] Transform fpsCam;

    public float interactionDist;

    public bool shooting;

    public Vector3 checkPoint;
    public Transform startPoint;
    

    /// <summary>
    /// Function to update which gun is currently equipped
    /// </summary>
    /// <param name="gun"> To input weapons with Gun class </param>
    public void UpdateGun(Gun gun)
    {
       currentGun = gun;
    }
    void OnShoot()
    {
        shooting = !shooting;
    }
    /// <summary>
    /// Player input to reload gun (R)
    /// </summary>
    void OnReload()
    {
        Debug.Log("reload");
        currentGun.Reloading();
    }

    /// <summary>
    /// Player input to interact with items (E)
    /// </summary>
    void OnInteract()
    {
        if(currentInteractable != null)
        {
            if (currentInteractable.CompareTag("Collectible"))
            {
                currentInteractable.InteractObject();
            }
            else if (currentInteractable.CompareTag("Door"))
            {
                currentInteractable.ChangeScene();
            }
            else if (currentInteractable.CompareTag("RespawnPoint"))
            {
                currentInteractable.SetSpawnPoint();
                Debug.Log("Set spawn");
            }


            
        }

    }

    public void CheckPoint(GameObject newCheckPoint)
    {
        checkPoint = newCheckPoint.transform.position;
    }

    /// <summary>
    /// Playeer input to equip first weapon/rifle (1)
    /// </summary>
    void OnWeapon1()
    {
        GameManager.Instance.EquipWeapon1();
    }

    /// <summary>
    /// Player input to equip second weapon/rocket launcher (2)
    /// </summary>
    void OnWeapon2()
    {
        GameManager.Instance.EquipWeapon2();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = startPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(shooting)
        {
            if (GameManager.Instance.rifle.isEquipped)
            {
                GameManager.Instance.rifle.Shooting();
            }
            else
            {
                GameManager.Instance.rL.Shooting();
            }
            
        }
        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hitInfo, interactionDist))
        {
            if (hitInfo.transform.TryGetComponent<Interact>(out currentInteractable))
            {
                Debug.Log(hitInfo.transform.name);
            }
        }

    }
}
