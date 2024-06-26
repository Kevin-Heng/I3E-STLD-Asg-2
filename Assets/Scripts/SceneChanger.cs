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
    public int sceneIndex;
    public int startSceneIndex;

    public void ChangeScene()
    {
        if(sceneIndex == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            GameManager.Instance.playerUI.SetActive(false);

            GameManager.Instance.deathScreen.SetActive(false);
        }
        else
        {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameManager.Instance.player.gameObject.SetActive(true);
            GameManager.Instance.playerUI.SetActive(true);
            GameManager.Instance.deathScreen.SetActive(false);
                     
        }
        SceneManager.LoadScene(sceneIndex);

    }

    public void StartScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if(GameManager.Instance != null)
        {
            GameManager.Instance.burning = false;

            GameManager.Instance.player.gameObject.SetActive(true);
            GameManager.Instance.playerUI.SetActive(true);
            GameManager.Instance.deathScreen.SetActive(false);

            GameManager.Instance.player.gameObject.transform.position = GameManager.Instance.player.startPoint.transform.position;
            Physics.SyncTransforms();

            GameManager.Instance.playerHp = GameManager.Instance.originalPlayerHp;
            GameManager.Instance.playerHpText.text = GameManager.Instance.playerHp.ToString();

            GameManager.Instance.rifle.currentAmmo = GameManager.Instance.rifle.magazineAmmo;
            GameManager.Instance.currentRifleAmmoText.text = GameManager.Instance.rifle.currentAmmo.ToString(); //update rifle ammo 

            GameManager.Instance.rifle.totalAmmo = GameManager.Instance.rifle.originalTotalAmmo;
            GameManager.Instance.totalRifleAmmoText.text = GameManager.Instance.rifle.totalAmmo.ToString(); //update rifle total ammo

            GameManager.Instance.rL.currentAmmo = GameManager.Instance.rL.magazineAmmo;
            GameManager.Instance.currentRLAmmoText.text = GameManager.Instance.rL.currentAmmo.ToString(); //update rocket launcher ammo

            GameManager.Instance.rL.totalAmmo = GameManager.Instance.rL.originalTotalAmmo;
            GameManager.Instance.totalRLAmmoText.text += GameManager.Instance.rL.totalAmmo.ToString(); //update rocket launcher total ammo

            SceneManager.LoadScene(startSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(startSceneIndex);
        }
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameManager.Instance.player.gameObject.transform.position = GameManager.Instance.player.startPoint.transform.position;
        Physics.SyncTransforms();

        GameManager.Instance.gameObject.SetActive(true);
        GameManager.Instance.player.gameObject.SetActive(true);
        GameManager.Instance.playerUI.SetActive(true);

        GameManager.Instance.playerHp = GameManager.Instance.originalPlayerHp;
        GameManager.Instance.playerHpText.text = GameManager.Instance.playerHp.ToString();

        GameManager.Instance.deathScreen.SetActive(false);
    }

}
