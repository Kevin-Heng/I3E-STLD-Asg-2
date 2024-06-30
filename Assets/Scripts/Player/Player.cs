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
    [Header("References")]
    /// <summary>
    /// Reference the current gun player is using
    /// </summary>
    Gun currentGun;
    /// <summary>
    /// Reference the current interactable players wants to interact with
    /// </summary>
    Interact currentInteractable;

    [Header("Raycast")]
    /// <summary>
    /// Player camera
    /// </summary>
    [SerializeField] Transform fpsCam;

    /// <summary>
    /// Max raycast distance
    /// </summary>
    public float interactionDist;

    [Header("Gun shooting")]
    /// <summary>
    /// boolean to check if player is shooting gun
    /// </summary>
    public bool shooting;

    [Header("Check point")]
    /// <summary>
    /// Position of check point
    /// </summary>
    public Transform checkPoint;
    /// <summary>
    /// boolean to check if check point is set 
    /// </summary>
    public bool checkPointSet;

    [Header("UI text")]
    /// <summary>
    /// Text to tell player what he can interact with
    /// </summary>
    public TextMeshProUGUI interactText;
    /// <summary>
    /// Text to see if player completes objective 1
    /// </summary>
    public TextMeshProUGUI objective1;
    /// <summary>
    /// Text to see if player completes objective 2
    /// </summary>
    public TextMeshProUGUI objective2;

    [Header("Pause")]
    /// <summary>
    /// Pause screen panel
    /// </summary>
    public GameObject pauseScreen;
    /// <summary>
    /// boolean to check if game is paused
    /// </summary>
    public bool isPaused;

    [Header("Collectibles")]
    /// <summary>
    /// check if circuit board is picked up
    /// </summary>
    public bool circuitBoard;
    /// <summary>
    /// check if screw is picked up
    /// </summary>
    public bool screw;
    /// <summary>
    /// check if canister is picked up
    /// </summary>
    public bool canister;
    /// <summary>
    /// check if metal board is picked up;
    /// </summary>
    public bool metalBoard;

    /// <summary>
    /// Update if circuit board is picked up
    /// </summary>
    /// <param name="pickedUp">boolean from circuit board script</param>
    public void UpdateCircuitBoard(bool pickedUp)
    {
        circuitBoard = pickedUp;
    }
    /// <summary>
    /// Update if screw is picked up
    /// </summary>
    /// <param name="pickedUp">boolean from screw script</param>
    public void UpdateScrew(bool pickedUp)
    {
        screw = pickedUp;
    }
    /// <summary>
    /// Update if canister is picked up
    /// </summary>
    /// <param name="pickedUp">boolean from canister script</param>
    public void UpdateCanister(bool pickedUp)
    {
        canister = pickedUp;
        if(canister)
        {
            objective1.text = "<s>Collect empty energy canister in ship</s>"; //strike through text
        }
    }
    /// <summary>
    /// Update if metal board is picked up
    /// </summary>
    /// <param name="pickedUp">boolean from metal board script</param>
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
    /// <summary>
    /// Player input to shoot (Left click)
    /// </summary>
    void OnShoot()
    {
        shooting = !shooting; //player is shooting
    }
    /// <summary>
    /// Player input to reload gun (R)
    /// </summary>
    void OnReload()
    {
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
                AudioManager.Instance.checkPointSound.Play(); //audio plays when check point is set
            }
        }

    }

    /// <summary>
    /// Player input to pause game (Tab)
    /// </summary>
    void OnPause()
    {
        isPaused = !isPaused; //isPaused = true
        if (isPaused)
        {
            Time.timeScale = 0f; //stop game time
            //cursor is visible and can move
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f; //resume game time
            //hide cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        pauseScreen.SetActive(isPaused); //show or hide pause screen panel
        
    }
    /// <summary>
    /// Function to set check point position
    /// </summary>
    /// <param name="newCheckPoint"> check point game object</param>
    public void CheckPoint(GameObject newCheckPoint)
    {
        checkPoint.transform.position = newCheckPoint.transform.position; //store new check point position values
        checkPointSet = true; //check point is set
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
        this.transform.position = GameManager.Instance.startPoint.position; //set player to start point
    }

    // Update is called once per frame
    void Update()
    {
        if(shooting) //check if player is shooting
        {
            if (GameManager.Instance.rifle.isEquipped)
            {
                GameManager.Instance.rifle.Shooting(); //rifle shoots if equipped
            }
            else
            {
                GameManager.Instance.rL.Shooting(); //rocket launcher shoots if equipped
            }
            
        }
        //if raycast hits an object with Interact component
        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.position, fpsCam.forward, out hitInfo, interactionDist))
        {
            if (hitInfo.transform.TryGetComponent<Interact>(out currentInteractable))
            {
                interactText.text = "Press E to interact"; //text appears
            }
            else
            {
                interactText.text = null; //text is empty
            }
        }
        else
        {
            interactText.text = null; //text is empty
        }


    }
}
