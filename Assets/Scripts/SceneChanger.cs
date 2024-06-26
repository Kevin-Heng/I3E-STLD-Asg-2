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

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneIndex);
        if(sceneIndex > 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameManager.Instance.gameObject.SetActive(true);
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
