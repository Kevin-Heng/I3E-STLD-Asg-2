/*
 * Author: Kevin Heng
 * Date: 29/06/2024
 * Description: The AudioManager class is used to handle all audio used in the game
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip placeObject;
    public AudioClip pickUp;
    public AudioClip pickUpMedKit;
    public AudioClip checkPointSound;

    //Gun audio
    /// <summary>
    /// Audio when bullet is shot
    /// </summary>
    public AudioClip gunShot;
    /// <summary>
    /// Audio when rifle bullet is shot
    /// </summary>
    public AudioClip rifleShot;
    /// <summary>
    /// Audio when rocket launcher missile is shot
    /// </summary>
    public AudioClip rLShot;
    /// <summary>
    /// Audio when reloading gun
    /// </summary>
    public AudioClip gunReload;
    /// <summary>
    /// Audio when rifle is reloading
    /// </summary>
    public AudioClip rifleReload;
    /// <summary>
    /// Audio when rocket launcher is reloading
    /// </summary>
    public AudioClip rLReload;
    /// <summary>
    /// Audio when there is no ammo left in gun
    /// </summary>
    public AudioClip emptyMag;


    /// <summary>
    /// Audio played when enemy is hit by projectile
    /// </summary>
    public AudioClip playerHit;
    /// <summary>
    /// Audio played when player dies
    /// </summary>
    public AudioClip playerDie;

    public AudioClip playerBurn;


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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
