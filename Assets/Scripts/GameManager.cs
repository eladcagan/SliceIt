using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private CinemachineVirtualCamera _cmCamera;
    [SerializeField]
    private SwordHandler _swordHandlerPrefab;
    [SerializeField]
    private Transform _levelAnchor;
    [SerializeField]
    private List<Level> _levels;
    
    private SwordHandler _sword;
    private Level _currentLevel;
    private int _playerScore;
    private int _currentLevelIndex;

    private void Awake()
    {
        _currentLevelIndex = 0;
        _uiManager.OnNextLevelClicked += LoadNextLevel;
        _uiManager.OnTryAgainClicked += RestartLevel;
        _uiManager.OnRestartGameClicked += RestartGame;
        RestartGame();
    }

    private void RestartGame()
    {
        _currentLevelIndex = 0;
        RestartLevel();
    }

    private void RestartLevel()
    {
        if(_currentLevel != null)
        {
            _currentLevel.transform.position = new Vector3(1000, 1000, 1000);
            DestroyImmediate(_currentLevel);
        }

        LoadLevel();
    }

    private void LoadNextLevel()
    {
        if (_currentLevel != null)
        {
            _currentLevel.transform.position = new Vector3(1000, 1000, 1000);
            DestroyImmediate(_currentLevel);
        }

        _currentLevelIndex++;
            LoadLevel();
    }

    private void LoadLevel()
    {
        Debug.Log(_currentLevelIndex);
        var level = _levels[_currentLevelIndex];
        _currentLevel = Instantiate(level, _levelAnchor);
        InitializeLevel(_currentLevel.StartAnchor);
    }

    private void InitializeLevel(Transform startAnchor)
    {
        _sword = Instantiate(_swordHandlerPrefab, startAnchor);
        _sword.OnBladeCut += OnBladeCut;
        _sword.OnBladeFinish += OnBladeFinish;
        _sword.OnBladeHitGround += OnBladeHitGround;
        _cmCamera.Follow = _sword.transform;
        _cmCamera.LookAt = _sword.transform;
        _sword.IsGameInProgress = true;
    }

    private void OnBladeFinish(int multiplier)
    {
        _sword.IsGameInProgress = false;
        _playerScore *= multiplier;
        _uiManager.UpdateScore(_playerScore);
        if (_currentLevelIndex + 1 <= _levels.Count)
        {
            LevelFinished();
        }
        else
        {
            AllLevelsFinished();
        }
    }

    private void OnBladeHitGround()
    {
        Lose();
    }

    private void LevelFinished()
    {
        _uiManager.OpenLevelFinishMenu();
    }

    private void AllLevelsFinished()
    {
        _uiManager.OpenComingSoon();
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
