/*
 * Author: Kevin Heng
 * Date: 29/06/2024
 * Description: The AudioManager class is used to handle all audio used in the game
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Collectibles")]
    public AudioSource placeObject;
    public AudioSource pickUp;
    public AudioSource pickUpMedKit;
    public AudioSource checkPointSound;

    [Header("Gun audio")]
    /// <summary>
    /// Audio when bullet is shot
    /// </summary>
    public AudioSource gunShot;

    /// <summary>
    /// Audio when rifle bullet is shot
    /// </summary>
    public AudioSource rifleShot;

    /// <summary>
    /// Audio when rocket launcher missile is shot
    /// </summary>
    public AudioSource rLShot;

    /// <summary>
    /// Audio when reloading gun
    /// </summary>
    public AudioSource gunReload;
    /// <summary>
    /// Audio when rifle is reloading
    /// </summary>
    public AudioSource rifleReload;
    /// <summary>
    /// Audio when rocket launcher is reloading
    /// </summary>
    public AudioSource rLReload;
    /// <summary>
    /// Audio when there is no ammo left in gun
    /// </summary>
    public AudioSource emptyMag;

    [Header("Player")]
    /// <summary>
    /// Audio played when enemy is hit by projectile
    /// </summary>
    public AudioClip playerHit;
    /// <summary>
    /// Audio played when player dies
    /// </summary>
    public AudioClip playerDie;

    public AudioSource deathMusic;
    public AudioSource mainMenu;
    public AudioSource winMusic;

    public AudioClip playerBurn;

    [Header("Audio mixer")]
    public Slider masterVol, musicVol, sfxVol;
    public AudioMixer mainAudioMixer;


    /// <summary>
    /// Function to ensure there is only one audio manager
    /// </summary>
    private void Awake()
    {
        if (Instance == null) //start: no audio manager
        {
            Instance = this; //set audio manager
            DontDestroyOnLoad(gameObject); //items in audio manager are not destroyed in the next scene
        }
        else if (Instance != null && Instance != this) //when enter new scene, new audio manager is created. if there is a audio manager and the audio manager is not the current one
        {
            Destroy(gameObject); //destroy the new one
        }

    }

    /// <summary>
    /// Function to adjust the master volume using a slider
    /// </summary>
    public void ChangeMasterVol()
    {

        mainAudioMixer.SetFloat("Master Volume", masterVol.value);

    }
    /// <summary>
    /// Function to adjust the music volume using a slider
    /// </summary>
    public void ChangeMusicVol()
    {
        mainAudioMixer.SetFloat("Music Volume", musicVol.value);
    }
    /// <summary>
    /// Function to adjust the sfx volume using a slider
    /// </summary>
    public void ChangeSFXVol()
    {
        mainAudioMixer.SetFloat("SFX Volume", sfxVol.value);
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
