using System.Collections.Generic;
using UnityEngine;

public class TutorialBase : MonoBehaviour, ITutorial
{
    public List<GameObject> tutorialPanels;
    private int _currentPanelIndex;
    public static bool isTutorialExit;

    private void Start()
    {
        isTutorialExit = false;
        if (tutorialPanels == null || tutorialPanels.Count == 0)
        {
            return;
        }

        foreach (var tutorialPanel in tutorialPanels)
        {
            tutorialPanel.SetActive(false);
        }

        _currentPanelIndex = 0;
        tutorialPanels[_currentPanelIndex].SetActive(true);
        Time.timeScale = 0;
    }

    public void EnterTutorial()
    {
        if (isTutorialExit) return;
    
        if (tutorialPanels == null || tutorialPanels.Count == 0)
        {
            return;
        }

        _currentPanelIndex = (_currentPanelIndex + 1) % tutorialPanels.Count;

        foreach (var tutorialPanel in tutorialPanels)
        {
            if (tutorialPanel != null)
                tutorialPanel.SetActive(false);
        }

        if (tutorialPanels[_currentPanelIndex] != null)
            tutorialPanels[_currentPanelIndex].SetActive(true);

        Time.timeScale = 0;
    }

    public void ExitTutorial()
    {
        Time.timeScale = 1;
        foreach (var tutorialPanel in tutorialPanels)
        {
            if (tutorialPanel != null)
                tutorialPanel.SetActive(false);
        }

        _currentPanelIndex = 0;
        isTutorialExit = true;
    }
}