using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : UIInteractions
{
    public GameObject optionsPanel;
    public GameObject htpPanel;
    public GameObject exitPanel;
    public GameObject creditsPanel;

    public GameObject nextButton;
    public GameObject prevButton;

    public bool optionsPanelOpen;
    public bool htpPanelOpen;
    public bool exitPanelOpen;
    public bool creditsPanelOpen;

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
        if (optionsPanelOpen)
        {
            optionsPanelOpen = false;
            optionsPanel.SetActive(false);
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
                else if (listIndex == 0)
                {
                    listIndex++;
                    page.SetActive(true);

                }
            }
        }
        else if (exitPanelOpen)
        {
            exitPanelOpen = false;
            exitPanel.SetActive(false);
        }
        else
        {
            creditsPanelOpen = false;
            creditsPanel.SetActive(false);
        }

    }

    public void Credits()
    {
        creditsPanel.SetActive(true);
        creditsPanelOpen = true;
    }

    public void ExitConfirmation()
    {
        exitPanelOpen = true;
        exitPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("exit game");
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
