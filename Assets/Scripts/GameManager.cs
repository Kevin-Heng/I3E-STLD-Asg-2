using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //----------------------------------- Gun ----------------------------------------// 

    //Gun ammo
    public static int totalAmmo = 120; //total ammo that is stored at the start of the game
    public static int currentAmmo; //set to 0 at the start
    public static int magazineAmmo = 30; //total of 30 bullets for one magazine, max amount for currentAmmo
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
    //----------------------------------- Gun ----------------------------------------// 
    //Function to shoot gun
    public void ReduceAmmo() //function to reduce ammo when shooting
    {
        currentAmmo--; //ammo reduce by 1 when 1 bullet is shot     
    }

    //function to reload gun
    public void ReloadGun()
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
    }

    

    //function for when there is no ammo at all
    public void NoAmmo(AudioClip emptyMag, Transform fpsCam)
    {
        if (totalAmmo == 0 && currentAmmo == 0)
        {
            AudioSource.PlayClipAtPoint(emptyMag, fpsCam.position, 0.5f);
        }
    }

    

    //--------------------------------------------------------------------------------//

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo += magazineAmmo;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
