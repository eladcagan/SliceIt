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

    internal void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    internal void OpenGameOverMenu()
    {
        throw new NotImplementedException();
    }

    internal void OpenLevelFinishMenu()
    {
        throw new NotImplementedException();
    }
}
