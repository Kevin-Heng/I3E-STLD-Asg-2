/*
 * Author: Kevin Heng
 * Date: 24/06/2024
 * Description: The UIInteractions class is used to handle functions for buttons
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractions : MonoBehaviour
{
    /// <summary>
    /// Function to resume game
    /// </summary>
    public void ResumeGame()
    {
        Time.timeScale = 1.0f; //resume game time
        GameManager.Instance.player.pauseScreen.SetActive(false); //hide pause screen panel
        //hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
