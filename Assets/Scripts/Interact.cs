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
    public GameObject spawnObject;

    public virtual void InteractObject()
    {       
        Destroy(gameObject);
    }
    
    public virtual void Interaction()
    {
        Debug.Log("Interact");
    }
    public override void ChangeScene()
    {
        base.ChangeScene();
    }
    
    public void SetSpawnPoint()
    {
        GameManager.Instance.player.CheckPoint(spawnObject);
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
