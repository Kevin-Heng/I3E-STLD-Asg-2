/*
 * Author: Kevin Heng
 * Date: 19/06/2024
 * Description: The Interact class is used to handle player interaction with interactable
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : SceneChanger
{
    /// <summary>
    /// GameObject to set spawn point
    /// </summary>
    public GameObject spawnObject;

    /// <summary>
    /// Function for player to interact with objects
    /// </summary>
    public virtual void InteractObject()
    {       
        Destroy(gameObject);
        AudioManager.Instance.pickUp.Play();
    }

    /// <summary>
    /// Function change scenes 
    /// </summary>
    public override void ChangeScene()
    {
        base.ChangeScene();
    }
    
    /// <summary>
    /// Function to set spawn point for player
    /// </summary>
    public void SetSpawnPoint()
    {
        GameManager.Instance.warningTextBox.SetActive(true);
        GameManager.Instance.warningText.text = "Check point set";
        StartCoroutine(HideText());
        GameManager.Instance.player.CheckPoint(spawnObject);
    }

    /// <summary>
    /// function to hide text after interactions
    /// </summary>
    /// <returns></returns>
    public IEnumerator HideText()
    {
        yield return new WaitForSeconds(5);
        GameManager.Instance.warningTextBox.SetActive(false);
        GameManager.Instance.warningText.text = null;
    }

    /// <summary>
    /// Function to place down item
    /// </summary>
    public virtual void PlaceItem()
    {
        AudioManager.Instance.placeObject.Play();
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
