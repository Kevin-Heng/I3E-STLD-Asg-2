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

public class Interact : MonoBehaviour
{
    public int sceneIndex;
    public GameObject spawnObject;
    public bool checkPointSet;
    [SerializeField] Player player;
    public virtual void InteractObject()
    {       
        Destroy(gameObject);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
    
    public void SetSpawnPoint()
    {
        player.CheckPoint(spawnObject);
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
