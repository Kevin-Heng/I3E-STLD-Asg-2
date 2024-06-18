using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance;
    public Player player;
    //----------------------------------- Player ----------------------------------------// 
    public int playerHp = 100;

    //----------------------------------- Gun ----------------------------------------// 
    public TextMeshProUGUI currentRifleAmmoText;
    public TextMeshProUGUI totalRifleAmmoText;
    public TextMeshProUGUI currentRLAmmoText;
    public TextMeshProUGUI totalRLAmmoText;

    public Rifle rifle;
    public RocketLauncher rL;


    //--------------------------------------------------------------------------------// 

    //function to ensure there is only one game manager
    private void Awake()
    {
        if (Instance == null) //start: no game manager
        {
            Instance = this; //set game manager
            DontDestroyOnLoad(gameObject); //items in game manager are not destroyed in the next scene
        }
        else if (Instance != null && Instance != this) //when enter new scene, new game manager is created, means there is a game manager and the game manager is not the current one
        {
            Destroy(gameObject); //destroy the new one
        }

    }

    public void ReducePlayerHp(int damage, AudioClip playerHit, Transform fpsCam)
    {
        playerHp -= damage;
        AudioSource.PlayClipAtPoint(playerHit, fpsCam.position, 0.6f);
    }
    //----------------------------------- Gun ----------------------------------------// 


    //Function to shoot gun
    public void ReduceAmmo(ref int currentAmmo) //function to reduce ammo when shooting
    {
        currentAmmo--; //ammo reduce by 1 when 1 bullet is shot
        if (rifle.isEquipped)
        {
            currentRifleAmmoText.text = currentAmmo.ToString();
        }
        else
        {
            currentRLAmmoText.text = rL.currentAmmo.ToString();
        }
    }

    //function to reload gun
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
        if (rifle.isEquipped)
        {
            currentRifleAmmoText.text = currentAmmo.ToString();
            totalRifleAmmoText.text = totalAmmo.ToString();
        }
        else
        {
            currentRLAmmoText.text = currentAmmo.ToString();
            totalRLAmmoText.text = totalAmmo.ToString();
        }
        
    }



    //function for when there is no ammo at all
    public void NoAmmo(ref int currentAmmo, ref int totalAmmo, AudioClip emptyMag, Transform fpsCam)
    {
        if (totalAmmo == 0 && currentAmmo == 0)
        {
            AudioSource.PlayClipAtPoint(emptyMag, fpsCam.position, 0.5f);
        }
    }
    //--------------------------------------------------------------------------------//

    public void SwapWeapons()
    {
        if (!rifle.isEquipped && rL.isEquipped && !rL.isReloading && Input.GetKeyDown(KeyCode.Alpha1))
        {
            rifle.isEquipped = true;
            rL.isEquipped = false;

            rifle.gameObject.SetActive(true);
            rL.gameObject.SetActive(false);

            currentRifleAmmoText.enabled = true;
            totalRifleAmmoText.enabled = true;

            currentRLAmmoText.enabled = false;
            totalRLAmmoText.enabled = false;
            rL.ReduceSpeed(player);
        }
        else if(rifle.isEquipped && !rL.isEquipped && !rifle.isReloading && Input.GetKeyDown(KeyCode.Alpha2))
        {
            rifle.isEquipped = false;
            rL.isEquipped = true;
            rifle.gameObject.SetActive(false);
            rL.gameObject.SetActive(true);
            currentRifleAmmoText.enabled = false;
            totalRifleAmmoText.enabled = false;
            currentRLAmmoText.enabled = true;
            totalRLAmmoText.enabled = true;
            rL.ReduceSpeed(player);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        currentRifleAmmoText.text = rifle.currentAmmo.ToString();
        totalRifleAmmoText.text = rifle.totalAmmo.ToString();

        currentRLAmmoText.text = rL.currentAmmo.ToString();
        totalRLAmmoText.text += rL.totalAmmo.ToString();

        currentRLAmmoText.enabled = false;
        totalRLAmmoText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        SwapWeapons();
        
    }
}
