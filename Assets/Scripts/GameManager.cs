/*
 * Author: Kevin Heng
 * Date: 11/06/2024
 * Description: The GameManager class is used to store values that are to be carried across different scenes
 */

using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    /// <summary>
    /// To set only one game manager at all times
    /// </summary>
    public static GameManager Instance;

    /// <summary>
    /// player camera
    /// </summary>
    public Transform fpsCam;

    [Header("Player")]
    /// <summary>
    /// To reference Player class on Player Capsule
    /// </summary>
    public Player player;

    /// <summary>
    /// Player hp
    /// </summary>
    public int playerHp;
    /// <summary>
    /// Original player hp
    /// </summary>
    public int originalPlayerHp = 100;
    /// <summary>
    /// To show player hp
    /// </summary>
    public TextMeshProUGUI playerHpText;

    [Header("UI")]
    /// <summary>
    /// death screen panel
    /// </summary>
    public GameObject deathScreen;
    /// <summary>
    /// player ui panel
    /// </summary>
    public GameObject playerUI;
    /// <summary>
    /// burning UI 
    /// </summary>
    public GameObject burningFrame;
    /// <summary>
    /// check if player is currently burning
    /// </summary>
    public bool burning;
    /// <summary>
    /// bleeding UI
    /// </summary>
    public GameObject bleedingFrame;
    /// <summary>
    /// Image for warning text
    /// </summary>
    public GameObject warningTextBox;
    /// <summary>
    /// wraning text when player cannot access something
    /// </summary>
    public TextMeshProUGUI warningText;
    /// <summary>
    /// Panel when player completes game
    /// </summary>
    public GameObject winPanel;

    [Header("start")]
    /// <summary>
    /// Start point in scene index 1
    /// </summary>
    public Transform startPoint;

    [Header("Gun")]
    /// <summary>
    /// UI text for current ammo in the rifle's magazine
    /// </summary>
    public TextMeshProUGUI currentRifleAmmoText;
    /// <summary>
    /// UI text for total ammo left to use for the rifle
    /// </summary>
    public TextMeshProUGUI totalRifleAmmoText;
    /// <summary>
    /// UI text for current ammo in the rocket launcher's magazine
    /// </summary>
    public TextMeshProUGUI currentRLAmmoText;
    /// <summary>
    /// UI text for total ammo left to use for the rocket launcherr
    /// </summary>
    public TextMeshProUGUI totalRLAmmoText;

    /// <summary>
    /// To reference the Rifle class in rifle game object 
    /// </summary>
    public Rifle rifle;
    /// <summary>
    /// To refernce RocketLauncher class in rocket launcher game object
    /// </summary>
    public RocketLauncher rL;

    /// <summary>
    /// Function to ensure there is only one game manager
    /// </summary>
    private void Awake()
    {
        if (Instance == null) //start: no game manager
        {
            Instance = this; //set game manager
            DontDestroyOnLoad(gameObject); //items in game manager are not destroyed in the next scene
        }
        else if (Instance != null && Instance != this) //when enter new scene, new game manager is created. if there is a game manager and the game manager is not the current one
        {
            Destroy(gameObject); //destroy the new one
        }

    }

    /// <summary>
    /// Function to reduce player hp when hit by enemy attacks
    /// </summary>
    /// <param name="damage"> Damage done by enemy </param> 
    /// <param name="playerHit"> Audio to be played when hit by enemy attack </param>
    /// <param name="fpsCam"> Main camera on player capsule </param>
    public void ReducePlayerHp(int damage, AudioClip playerHit, AudioClip playerDie, Transform fpsCam)
    {
        playerHp -= damage; //reduce player hp by specified damage done by enemy
        AudioSource.PlayClipAtPoint(playerHit, fpsCam.position, 1f); //audio clip to be played when hurt
        if(playerHp <= 30)
        {
            playerHpText.color = Color.red;
        }
        else
        {
            playerHpText.color = Color.white;
        }
        if (playerHp <= 0) //when player hp reaches 0 and below
        {
            AudioSource.PlayClipAtPoint(playerDie, fpsCam.position, 1f); //play death audio
            playerUI.SetActive(false);
            DeathScreen();
        }

    }

    /// <summary>
    /// Death screen panel to check if player wants to restart or return to main menu
    /// </summary>
    public void DeathScreen()
    {
        AudioManager.Instance.deathMusic.Play(); //play death music
        AudioManager.Instance.mainMenu.Stop(); //stop main menu music

        //remove damage effcts
        burningFrame.SetActive(false);
        burning = false;
        bleedingFrame.SetActive(false);
        //turn on deaeth screen panel
        deathScreen.gameObject.SetActive(true);
        //cursor is visible
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //reset player hp
        playerHp = originalPlayerHp;
        //pause game time
        Time.timeScale = 0;
        //turn off any warnings
        warningText.text = null;
        warningTextBox.SetActive(false);
    }

    //----------------------------------- Gun ----------------------------------------// 
    /// <summary>
    /// Function to reduce ammo when player shoots gun
    /// </summary>
    /// <param name="currentAmmo"> Current ammo inside gun magazine </param>
    public void ReduceAmmo(ref int currentAmmo) //function to reduce ammo when shooting
    {
        currentAmmo--; //ammo reduce by 1 when 1 bullet is shot
        if (rifle.isEquipped) //rifle is equipped
        {
            currentRifleAmmoText.text = currentAmmo.ToString(); //edit current ammo UI text for rifle
            if(currentAmmo <= 10)
            {
                currentRifleAmmoText.color = Color.red;
            }
        }
        else //rocket launcher is equipped
        {
            currentRLAmmoText.text = rL.currentAmmo.ToString(); //edit current ammo UI text for rocket launcher
            if(currentAmmo <= 2)
            {
                currentRLAmmoText.color = Color.red;
            }
        }
    }

    /// <summary>
    /// Function to reload ammo in gun
    /// </summary>
    /// <param name="currentAmmo"> Current ammo inside gun magazine </param>
    /// <param name="magazineAmmo"> Total ammo inside one magazine of the gun </param>
    /// <param name="totalAmmo"> Total ammo left to use in gun </param>
    public void ReloadGun(ref int currentAmmo, ref int magazineAmmo, ref int totalAmmo)
    {
        int difference = magazineAmmo - currentAmmo; //calculate how much ammo is needed to get current ammo back to 30
        if (difference > totalAmmo) //check if there is enough ammo to increase current ammo back to 30, e.g. current ammo = 20, difference = 10 and total ammo = 5
        {
            currentAmmo += totalAmmo; //increase current ammo by the remainder ammo
            totalAmmo -= totalAmmo; //total ammo is now 0
        }
        else
        {
            totalAmmo -= difference; //reduce total ammo by the required amount to increase current ammo back to 30
            currentAmmo += difference; //current ammo increase back to 30
        }
        if (rifle.isEquipped) //rifle is equipped
        {
            currentRifleAmmoText.text = currentAmmo.ToString(); //update current rifle ammo
            totalRifleAmmoText.text = totalAmmo.ToString(); //update total rifle ammo
            currentRifleAmmoText.color = Color.white;
        }
        else //rocket launcher is equipped
        {
            currentRLAmmoText.text = currentAmmo.ToString(); //update current rocket launcher ammo
            totalRLAmmoText.text = totalAmmo.ToString(); //update total rocket launcher ammo
            currentRLAmmoText.color = Color.white;
        }
        
    }

    //--------------------------------------------------------------------------------//
    /// <summary>
    /// Function to swap to rifle
    /// </summary>
    public void EquipWeapon1()
    {
        if (!rifle.isEquipped && rL.isEquipped && !rL.isReloading) //check if rifle is not equipped, rocket launcher is equipped and player presses 1
        {
            rifle.isEquipped = true; //rifle is equipped
            rL.isEquipped = false; //rocket launcher is unequipped

            rifle.gameObject.SetActive(true); //show rifle
            rL.gameObject.SetActive(false); //hide rocket launcher

            //show rifle UI
            currentRifleAmmoText.enabled = true;
            totalRifleAmmoText.enabled = true;
            rifle.ammoIcon.SetActive(true);

            //hide rocket launcher UI
            currentRLAmmoText.enabled = false;
            totalRLAmmoText.enabled = false;
            rL.ammoIcon.SetActive(false);

            rL.ChangeSpeed(player); //change player speed
        }
    }
    /// <summary>
    /// Function to equip rocket launcher
    /// </summary>
    public void EquipWeapon2()
    {
        if (rifle.isEquipped && !rL.isEquipped && !rifle.isReloading)
        {
            rifle.isEquipped = false; //rifle is unequipped
            rL.isEquipped = true; //rocket launcher is equipped

            rifle.gameObject.SetActive(false); //hide rifle
            rL.gameObject.SetActive(true); //show rocket launcher

            //hide rifle UI
            currentRifleAmmoText.enabled = false;
            totalRifleAmmoText.enabled = false;
            rifle.ammoIcon.SetActive(false);

            //show rocket launcher UI
            currentRLAmmoText.enabled = true;
            totalRLAmmoText.enabled = true;
            rL.ammoIcon.SetActive(true);

            rL.ChangeSpeed(player); //change player speed
        }
    }



    // Start is called before the first frame update
    void Start()
    {

        playerHp = originalPlayerHp;
        playerHpText.text = playerHp.ToString();

    }

    // Update is called once per frame
    void Update()
    {

    }
}