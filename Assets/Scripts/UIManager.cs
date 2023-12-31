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
    [SerializeField]
    private GameObject ComingSoonPanel;

    public Action OnNextLevelClicked;
    public Action OnTryAgainClicked;
    public Action OnRestartGameClicked;

    public void OnNextLevel()
    {
        OnNextLevelClicked?.Invoke();
        WinPanel.SetActive(false);
    }

    public void OnTryAgain()
    {
        OnTryAgainClicked?.Invoke();
        LosePanel.SetActive(false);
    }

    public void OnComingSoon()
    {
        OnRestartGameClicked?.Invoke();
        ComingSoonPanel.SetActive(false);
    }
    public void OpenComingSoon()
    {
        StartCoroutine(DelayPopUp(ComingSoonPanel));
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
        StartCoroutine(DelayPopUp(WinPanel));
    }
    
    private IEnumerator DelayPopUp(GameObject Popup)
    {
        yield return new WaitForSeconds(1f);
        Popup.SetActive(true);

    }
}
