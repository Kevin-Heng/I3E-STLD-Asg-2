/*
 * Author: Kevin Heng
 * Date: 10/06/2024
 * Description: The SceneChanger class is used to change the current scene when player needs to enter a different area
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    /// <summary>
    /// To set scene number for next area
    /// </summary>
    public int sceneIndex;
    /// <summary>
    /// Number is set 1 to bring player from start menu to first scene
    /// </summary>
    int startSceneIndex = 1;

    /// <summary>
    /// Function to change scenes
    /// </summary>
    public virtual void ChangeScene()
    {
        if(sceneIndex == 0) //if go back to home page
        {
            //show cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //Turn off all UIs, player and damage
            GameManager.Instance.playerUI.SetActive(false);

            GameManager.Instance.deathScreen.SetActive(false);

            GameManager.Instance.burningFrame.SetActive(false);

            GameManager.Instance.player.pauseScreen.SetActive(false);

            //turn off death music
            AudioManager.Instance.deathMusic.Stop();

            AudioManager.Instance.mainMenu.Stop();
            AudioManager.Instance.mainMenu.Play();


        }
        else
        {
            //hide cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            GameManager.Instance.deathScreen.SetActive(false);
            GameManager.Instance.burningFrame.SetActive(false);
                     
        }

        SceneManager.LoadScene(sceneIndex);

    }

    /// <summary>
    /// Function for when player starts game to enter first scene(Inside spaceship)
    /// </summary>
    public void StartScene()
    {
        Debug.Log("Starting");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if(GameManager.Instance != null)
        {
            Time.timeScale = 1.0f;
            GameManager.Instance.playerUI.SetActive(true);
            GameManager.Instance.deathScreen.SetActive(false);
            GameManager.Instance.player.pauseScreen.SetActive(false);

            GameManager.Instance.player.gameObject.transform.position = GameManager.Instance.startPoint.transform.position;
            GameManager.Instance.player.gameObject.transform.eulerAngles = GameManager.Instance.startPoint.transform.eulerAngles;
            Physics.SyncTransforms();

            //--------------------------------------------- Reset player info --------------------------------------------------------------------------------
            GameManager.Instance.playerHp = GameManager.Instance.originalPlayerHp;
            GameManager.Instance.playerHpText.text = GameManager.Instance.playerHp.ToString();
            GameManager.Instance.playerHpText.color = Color.white;

            GameManager.Instance.rifle.currentAmmo = GameManager.Instance.rifle.magazineAmmo;
            GameManager.Instance.currentRifleAmmoText.text = GameManager.Instance.rifle.currentAmmo.ToString(); //update rifle ammo 
            GameManager.Instance.currentRifleAmmoText.color = Color.white;

            GameManager.Instance.rifle.totalAmmo = GameManager.Instance.rifle.originalTotalAmmo;
            GameManager.Instance.totalRifleAmmoText.text = GameManager.Instance.rifle.totalAmmo.ToString(); //update rifle total ammo
            GameManager.Instance.totalRifleAmmoText.color = Color.white;


            GameManager.Instance.rL.currentAmmo = GameManager.Instance.rL.magazineAmmo;
            GameManager.Instance.currentRLAmmoText.text = GameManager.Instance.rL.currentAmmo.ToString(); //update rocket launcher ammo
            GameManager.Instance.currentRLAmmoText.color = Color.white;

            //turn off death music
            AudioManager.Instance.deathMusic.Stop();

            
        }
        SceneManager.LoadScene(startSceneIndex);

    }

    /// <summary>
    /// Function for player to restart the area when he dies
    /// </summary>
    public void Restart()
    {
        Time.timeScale = 1.0f; //resume game time

        //hide mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        //turn off damage UIs and turn on player UI
        GameManager.Instance.burningFrame.SetActive(false);
        GameManager.Instance.bleedingFrame.SetActive(false);
        GameManager.Instance.playerUI.SetActive(true);

        //reset player hp to full
        GameManager.Instance.playerHp = GameManager.Instance.originalPlayerHp;
        GameManager.Instance.playerHpText.color = Color.white;
        GameManager.Instance.playerHpText.text = GameManager.Instance.playerHp.ToString();

        //reset current rifle ammo to max
        GameManager.Instance.rifle.currentAmmo = GameManager.Instance.rifle.magazineAmmo;
        GameManager.Instance.currentRifleAmmoText.text = GameManager.Instance.rifle.currentAmmo.ToString(); //update rifle ammo 
        GameManager.Instance.currentRifleAmmoText.color = Color.white;

        //reset current rocket launcher ammo to max
        GameManager.Instance.rL.currentAmmo = GameManager.Instance.rL.magazineAmmo;
        GameManager.Instance.currentRLAmmoText.text = GameManager.Instance.rL.currentAmmo.ToString(); //update rocket launcher ammo
        GameManager.Instance.currentRLAmmoText.color = Color.white;

        //turn off death screen in previous scene
        GameManager.Instance.deathScreen.SetActive(false);

        //turn off death music
        AudioManager.Instance.deathMusic.Stop();



        if (SceneManager.GetActiveScene().buildIndex == 1) //if player dies inside spaceship, check point is automatically set to start point
        {
            GameManager.Instance.player.gameObject.transform.position = GameManager.Instance.startPoint.transform.position;
            GameManager.Instance.player.gameObject.transform.eulerAngles = GameManager.Instance.startPoint.transform.eulerAngles;
            Physics.SyncTransforms();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else //if player dies in any other scenes
        {
            if (!GameManager.Instance.player.checkPointSet) //if player has no check point set, , player spawns back at start point inside spaceship
            {
                GameManager.Instance.player.gameObject.transform.position = GameManager.Instance.startPoint.transform.position;
                GameManager.Instance.player.gameObject.transform.eulerAngles = GameManager.Instance.startPoint.transform.eulerAngles;
                Physics.SyncTransforms();
                SceneManager.LoadScene(1);
            }
            else //if player sets check point, player spawns at check point
            {
                GameManager.Instance.player.gameObject.transform.position = GameManager.Instance.player.checkPoint.position;
                GameManager.Instance.player.gameObject.transform.eulerAngles = GameManager.Instance.player.checkPoint.eulerAngles;
                Physics.SyncTransforms();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

}
