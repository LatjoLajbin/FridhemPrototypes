using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;

public class Base_Controller : MonoBehaviour
{
    public GameStartController _startController;
    public GamePlayingController _playingController;
    public GameDoneController _doneController;

    public EGamePreset _gamePreset;
    EGameState _gameState;

    void Start()
    {
        _gameState = EGameState.GameStart;
        _startController.Initiate();
        _playingController.Deactivate();
        _doneController.Deactivate();
    }

    public void GameStartButton_Pressed()
    {
        StartCoroutine(_startController.CR_StartGame(OnGameStartStateComplete));
    }

    void OnGameStartStateComplete()
    {
        _gameState = EGameState.GamePlaying;
        _startController.Deactivate();
        _playingController.gameObject.SetActive(true);
        _playingController.Initiate(_gamePreset, OnGamePlayingStateComplete);
    }

    void OnGamePlayingStateComplete()
    {
        _gameState = EGameState.GameDone;
        _playingController.Deactivate();
        _doneController.gameObject.SetActive(true);
        _doneController.Initiate(OnGameDoneStateComplete);
    }

    void OnGameDoneStateComplete()
    {
        SceneManager.LoadScene(0);
    }
}

public interface GamePreset
{
    abstract void InitiateGame();
    abstract void UpdateGame();

    abstract bool GameShouldReset();
    abstract bool GameIsComplete();
}
public enum EGamePreset
{
    None,
    Boss_Fight,
    Space_Explorer,
    Talk_Em_Up,
    Freeform_Puzzler
};

public enum EGameState
{
    GameStart,
    GamePlaying,
    GamePaused,
    GameDone
}