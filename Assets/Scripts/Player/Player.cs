/*
 * Author: Kevin Heng
 * Date: 10/06/2024
 * Description: The Player class is used to handle player interactions
 */

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public Transform checkPoint;
    public bool checkPointSet;


    public TextMeshProUGUI interactText;
    public TextMeshProUGUI objective1;
    public TextMeshProUGUI objective2;

    public GameObject pauseScreen;
    public bool isPaused;

    public bool circuitBoard;
    public bool screw;
    public bool canister;
    public bool metalBoard;


    public void UpdateCircuitBoard(bool pickedUp)
    {
        circuitBoard = pickedUp;
    }

    public void UpdateScrew(bool pickedUp)
    {
        screw = pickedUp;
    }

    public void UpdateCanister(bool pickedUp)
    {
        canister = pickedUp;
        if(canister)
        {
            objective1.text = "<s>Collect empty energy canister in ship</s>";
        }
    }

    public void UpdateMetalBoard(bool pickedUp)
    {
        metalBoard = pickedUp;
    }


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
        currentGun.OutOfAmmo();
    }
    /// <summary>
    /// Player input to reload gun (R)
    /// </summary>
    void OnReload()
    {
        Debug.Log("reload");
        currentGun.Reloading();
        currentGun.OutOfAmmo();
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
                AudioManager.Instance.checkPointSound.Play();
                Debug.Log("check point set");
            }
            else
            {
                currentInteractable.Interaction();
            }
        }

    }


    void OnPause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("resume");
        }
        pauseScreen.SetActive(isPaused);
        
    }

    void OnResume()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("resume");
    }
    public void CheckPoint(GameObject newCheckPoint)
    {
        checkPoint.transform.position = newCheckPoint.transform.position;
        checkPointSet = true;
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
        this.transform.position = GameManager.Instance.startPoint.position;
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
                interactText.text = "Press E to interact";
            }
            else
            {
                interactText.text = null;
            }
        }
        else
        {
            interactText.text = null;
        }


    }
}
