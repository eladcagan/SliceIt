using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private GameObject WinPanel;
    [SerializeField]
    private GameObject LosePanel;

    public Action OnNextLevelClicked;
    public Action OnTryAgainClicked;

    public void OnNextLevel()
    {
        OnNextLevelClicked?.Invoke();
    }

    public void OnTryAgain()
    {
        OnTryAgainClicked?.Invoke();
    }

    internal void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    internal void OpenGameOverMenu()
    {
        LosePanel.SetActive(true);
    }

    internal void OpenLevelFinishMenu()
    {
        WinPanel.SetActive(true);
    }
}
