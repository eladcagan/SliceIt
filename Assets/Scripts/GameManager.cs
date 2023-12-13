using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SwordHandler _sword;
    [SerializeField]
    private UIManager _uiManager;
    private int _playerScore;

    private void Awake()
    {
        _sword.OnBladeCut += OnBladeCut;
        _sword.OnBladeFinish += OnBladeFinish;
        _sword.OnBladeHitGround += OnBladeHitGround;
    }

    private void OnBladeFinish(int multiplier)
    {
        _playerScore *= multiplier;
        _uiManager.UpdateScore(_playerScore);
        LevelFinished();
    }

    private void OnBladeHitGround()
    {
        Lose();
    }

    private void LevelFinished()
    {
        _uiManager.OpenLevelFinishMenu();
    }

    private void Lose()
    {
        _uiManager.OpenGameOverMenu();
    }

    private void OnBladeCut(int score)
    {
        _playerScore += score;
        _uiManager.UpdateScore(_playerScore);
    }
}
