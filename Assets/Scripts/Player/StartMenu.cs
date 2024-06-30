/*
 * Author: Kevin Heng
 * Date: 29/06/2024
 * Description: The StartMenu class is used to handle functions for buttons in the start menu
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : UIInteractions
{
    [Header("Panels")]
    /// <summary>
    /// options panel
    /// </summary>
    public GameObject optionsPanel;
    /// <summary>
    /// how to play panel
    /// </summary>
    public GameObject htpPanel;
    /// <summary>
    /// exit confirmation panel
    /// </summary>
    public GameObject exitPanel;
    /// <summary>
    /// credits panel
    /// </summary>
    public GameObject creditsPanel;

    [Header("Panel booleans")]
    /// <summary>
    /// check if options panel is open
    /// </summary>
    public bool optionsPanelOpen;
    /// <summary>
    /// check if how to play panel is open
    /// </summary>
    public bool htpPanelOpen;
    /// <summary>
    /// check if exit confirmation panel is open
    /// </summary>
    public bool exitPanelOpen;
    /// <summary>
    /// check if credits panel is open
    /// </summary>
    public bool creditsPanelOpen;

    [Header("How to play")]
    /// <summary>
    /// next page button
    /// </summary>
    public GameObject nextButton;
    /// <summary>
    /// previous page button
    /// </summary>
    public GameObject prevButton;
    /// <summary>
    /// List for how to play pages
    /// </summary>
    public List<GameObject> htpPages = new List<GameObject>();
    /// <summary>
    /// list index starts from 0
    /// </summary>
    int listIndex = 0;

    /// <summary>
    /// Function for player to click options button
    /// </summary>
    public void Options()
    {
        //open options panel
        optionsPanelOpen = true;
        optionsPanel.SetActive(optionsPanelOpen);
    }

    /// <summary>
    /// Function for player to click how to play button
    /// </summary>
    public void HTPPanel()
    {
        listIndex = 0; //list index starts from 0
        //open how to play panel
        htpPanelOpen = true;
        htpPanel.SetActive(htpPanelOpen);
        //turn off next and previous buttons
        prevButton.SetActive(false);
        nextButton.SetActive(false);
    }
    /// <summary>
    /// Function for player to click next page on how to play panel
    /// </summary>
    public void NextPage()
    {
        htpPages[listIndex].SetActive(false); //set current page to false
        listIndex++; //increase index by 1
        htpPages[listIndex].SetActive(true); //set next page to true
    }

    /// <summary>
    /// Function for player to click previous page on how to play panel
    /// </summary>
    public void PrevPage()
    {
        htpPages[listIndex].SetActive(false); //set current page to false
        listIndex--; //decrease index by 1
        htpPages[listIndex].SetActive(true); //set previous page to true
    }

    /// <summary>
    /// Function for player to close any of the panels
    /// </summary>
    public void Close()
    {
        if (optionsPanelOpen) //if options panel is open, hide this panel
        {
            optionsPanelOpen = false;
            optionsPanel.SetActive(optionsPanelOpen);
        }
        else if (htpPanelOpen) //if how to panel is open, hide this panel
        {
            htpPanelOpen = false;
            htpPanel.SetActive(htpPanelOpen);
            listIndex = 0; //set list index back to 0
            foreach (GameObject page in htpPages) //for loop to check each element in list
            {
                if (listIndex != 0)
                {
                    page.SetActive(false); //turn off pages that are not the first one
                }
                else if (listIndex == 0)
                {
                    listIndex++; //increase index by 1
                    page.SetActive(true); //turn on start page

                }
            }
        }
        else if (exitPanelOpen) //if exit confirmation panel is open, hide this panel
        {
            exitPanelOpen = false;
            exitPanel.SetActive(exitPanelOpen);
        }
        else //if credits panel is open, hide this panel
        {
            creditsPanelOpen = false;
            creditsPanel.SetActive(creditsPanelOpen);
        }

    }

    /// <summary>
    /// Function for player to click credits button
    /// </summary>
    public void Credits()
    {
        //open credits panel
        creditsPanelOpen = true;
        creditsPanel.SetActive(creditsPanelOpen);
    }

    /// <summary>
    /// Function for player to click exit button to confirm exit
    /// </summary>
    public void ExitConfirmation()
    {
        //open exit confirmation panel
        exitPanelOpen = true;
        exitPanel.SetActive(exitPanelOpen);
    }
    /// <summary>
    /// Function for player to click exit button and quit game
    /// </summary>
    public void ExitGame()
    {
        //Close game
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (listIndex == 0)
        {
            prevButton.SetActive(false);
            nextButton.SetActive(true);
        }
        else if (listIndex == htpPages.Count - 1)
        {
            nextButton.SetActive(false);
            prevButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(true);
            prevButton.SetActive(true);
        }
    }
}
