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

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.player.pauseScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("resume");
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
