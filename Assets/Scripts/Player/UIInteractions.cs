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
    public GameObject optionsPanel;
    public GameObject htpPanel;

    public GameObject nextButton;
    public GameObject prevButton;

    public bool optionsPanelOpen;
    public bool htpPanelOpen;
    public List<GameObject> htpPages = new List<GameObject>();
    int listIndex = 0;

    public void Options()
    {
        optionsPanelOpen = true;
        optionsPanel.SetActive(true);
    }

    public void HTPPanel()
    {
        listIndex = 0;
        htpPanelOpen = true;
        htpPanel.SetActive(true);
        prevButton.SetActive(false);
        nextButton.SetActive(false);
    }

    public void NextPage()
    {
        htpPages[listIndex].SetActive(false);
        listIndex++;
        htpPages[listIndex].SetActive(true);
        Debug.Log("next page");
    }

    public void PrevPage()
    {
        htpPages[listIndex].SetActive(false);
        listIndex--;
        htpPages[listIndex].SetActive(true);
        Debug.Log("prev page");
    }

    public void Close()
    {
        Debug.Log("close button clicked");
        if(optionsPanelOpen)
        {
            optionsPanelOpen = false;
            optionsPanel.SetActive(false);
            Debug.Log("Close options");
        }
        else if (htpPanelOpen)
        {
            htpPanelOpen = false;
            htpPanel.SetActive(false);
            Debug.Log("Close HTP");
            listIndex = 0;
            foreach (GameObject page in htpPages)
            {
                if (listIndex != 0)
                {
                    page.SetActive(false);
                }
                else if(listIndex == 0)
                {
                    listIndex++;
                    page.SetActive(true);

                }
            }
        }

    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("exit game");
    }

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
        if(listIndex == 0)
        {
            prevButton.SetActive(false);
            nextButton.SetActive(true);
        }
        else if(listIndex == htpPages.Count -1)
        {
            nextButton.SetActive(false);
            prevButton.SetActive(true);
        }
    }
}
