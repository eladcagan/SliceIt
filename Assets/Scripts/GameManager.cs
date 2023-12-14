using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private SoundManager _soundManager;
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
        RegisterUIEvents();
        RestartGame();
    }

    private void RegisterUIEvents()
    {
        _uiManager.OnNextLevelClicked += LoadNextLevel;
        _uiManager.OnTryAgainClicked += RestartLevel;
        _uiManager.OnRestartGameClicked += RestartGame;
    }

    private void RestartGame()
    {
        _currentLevelIndex = 0;
        _playerScore = 0;
        _uiManager.UpdateScore(_playerScore);
        RestartLevel();
    }

    private void RestartLevel()
    {
        ClearLevel();
        LoadLevel();
    }

    private void LoadNextLevel()
    {
        ClearLevel();
        _currentLevelIndex++;
        LoadLevel();
    }

    private void ClearLevel()
    {
        if (_currentLevel != null)
        {
            UnregisterSwordEvents();
            _currentLevel.transform.position = new Vector3(1000, 1000, 1000); // Move the level off screen to be destroyed
            Destroy(_currentLevel.gameObject);
        }
    }

    private void LoadLevel()
    {
        var level = _levels[_currentLevelIndex];
        _currentLevel = Instantiate(level, _levelAnchor);
        InitializeLevel(_currentLevel.StartAnchor);
    }

    private void InitializeLevel(Transform startAnchor)
    {
        _sword = Instantiate(_swordHandlerPrefab, startAnchor);
        RegisterSwordEvents();
        _cmCamera.Follow = _sword.transform;
        _cmCamera.LookAt = _sword.transform;
        _sword.gameState = GameStates.InProgress;
    }

    private void OnSwordMove()
    {
        _soundManager.OnSwordMove();
    }

    private void OnBladeCut(int score)
    {
        _playerScore += score;
        _uiManager.UpdateScore(_playerScore);
        _soundManager.OnCuttableHit();
    }

    private void OnBladeFinish(int multiplier)
    {
        _sword.gameState = GameStates.LevelEnd;
        _playerScore *= multiplier;
        _uiManager.UpdateScore(_playerScore);
        _soundManager.OnFinishLineHit();
        if (_currentLevelIndex + 1 < _levels.Count)
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
        _soundManager.OnGroundHit();
        Lose();
    }

    private void OnPowerupHit(bool isGood)
    {
        if(isGood)
        {
            _soundManager.OnPowerUpHit();
        }
        else
        {
            _soundManager.OnBadPowerUpHit();
        }
    }

    private void OnHiltHit()
    {
        _soundManager.OnHiltHit();
    }

    private void OnBombHit()
    {
        _soundManager.OnBombHit();
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
        _sword.gameState = GameStates.GameEnd;
        _uiManager.OpenGameOverMenu();
    }

    private void RegisterSwordEvents()
    {
        _sword.OnBladeCut += OnBladeCut;
        _sword.OnBladeFinish += OnBladeFinish;
        _sword.OnBladeHitGround += OnBladeHitGround;
        _sword.OnBombHit += OnBombHit;
        _sword.OnPowerupHit += OnPowerupHit;
        _sword.OnSwordMove += OnSwordMove;
        _sword.OnHiltHit += OnHiltHit;
    }

    

    private void UnregisterSwordEvents()
    {
        _sword.OnBladeCut -= OnBladeCut;
        _sword.OnBladeFinish -= OnBladeFinish;
        _sword.OnBladeHitGround -= OnBladeHitGround;
        _sword.OnBombHit += OnBombHit;
        _sword.OnPowerupHit += OnPowerupHit;
        _sword.OnSwordMove -= OnSwordMove;
    }
}
