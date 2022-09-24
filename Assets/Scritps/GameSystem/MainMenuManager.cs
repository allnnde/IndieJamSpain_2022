using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class MainMenuManager : Singleton<MainMenuManager>
{
    
    public UIPanel optionsPanel;
  

    [Header("Play")]
    
    private bool _panelShown = false;

   

    public void ShowOptions()
    {
        ShowPanel(optionsPanel);
    }

    public void HideOptions()
    {
        HidePanel(optionsPanel);
    }


    private void ShowPanel(UIPanel panel)
    {
        if (!_panelShown)
        {
            _panelShown = true;
            panel.ShowPanel();
        }
    }

    private void HidePanel(UIPanel panel)
    {
        if (_panelShown)
        {
            _panelShown = false;
            panel.HidePanel();
        }
    }

    public void QuitGame()
    {
        Game_Manager.Instance.QuitGame();
    }
}
