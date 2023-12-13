using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using Enums;

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

    private void OnBladeFinish(int multiplier)
    {
        _sword.gameState = GameStates.LevelEnd;
        _playerScore *= multiplier;
        _uiManager.UpdateScore(_playerScore);
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
        _sword.gameState = GameStates.GameEnd;
        _uiManager.OpenGameOverMenu();
    }

    private void OnBladeCut(int score)
    {
        _playerScore += score;
        _uiManager.UpdateScore(_playerScore);
    }

    private void RegisterSwordEvents()
    {
        _sword.OnBladeCut += OnBladeCut;
        _sword.OnBladeFinish += OnBladeFinish;
        _sword.OnBladeHitGround += OnBladeHitGround;
    }

    private void UnregisterSwordEvents()
    {
        _sword.OnBladeCut -= OnBladeCut;
        _sword.OnBladeFinish -= OnBladeFinish;
        _sword.OnBladeHitGround -= OnBladeHitGround;
    }
}
