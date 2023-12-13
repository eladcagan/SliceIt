using Cinemachine;
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
        LoadLevel();
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
        _sword.OnBladeCut += OnBladeCut;
        _sword.OnBladeFinish += OnBladeFinish;
        _sword.OnBladeHitGround += OnBladeHitGround;
        _cmCamera.Follow = _sword.transform;
        _cmCamera.LookAt = _sword.transform;
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
