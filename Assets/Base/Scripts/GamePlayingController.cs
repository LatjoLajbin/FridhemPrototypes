using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;

public class GamePlayingController : MonoBehaviour
{
    public GameObject _UIGameObject;
    public GamePlaying _gamePlayingInstance;
    public TMPro.TextMeshProUGUI _gamePlayingText;

    EGamePreset _preset;

    Action _stateCompleteCallback;

    float _textFadeInTime = 0.5f;
    float _textShowTime = 1.5f;
    float _textSlideOutTime = 0.25f;

    void Awake()
    {
        _gamePlayingInstance.gameObject.SetActive(false);
    }

    public void Initiate(EGamePreset aPreset, Action aStateCompleteCallback)
    {
        _preset = aPreset;
        _stateCompleteCallback = aStateCompleteCallback;
        _gamePlayingText.color = Color.clear;
        _UIGameObject.SetActive(true);

        switch (_preset)
        {
            case EGamePreset.None:
                _gamePlayingText.text = "No game preset chosen.";
                break;
            case EGamePreset.Boss_Fight:
                _gamePlayingText.text = "Boss fight";
                break;
            case EGamePreset.Space_Explorer:
                _gamePlayingText.text = "Space explorer";
                break;
            case EGamePreset.Talk_Em_Up:
                _gamePlayingText.text = "Talk'em up";
                break;
            case EGamePreset.Freeform_Puzzler:
                _gamePlayingText.text = "Freeform puzzler";
                break;
            default:
                _gamePlayingText.text = "Game preset undefined.";
                break;
        }

        StartCoroutine(InitialAnimation());
    }

    void GameCompleteCallback(EGamePlayingCallbackType aCallbackType)
    {
        switch (aCallbackType)
        {
            case EGamePlayingCallbackType.GameReset:
                SceneManager.LoadScene(0);
                break;
            case EGamePlayingCallbackType.GameCompleted:
                _stateCompleteCallback();
                break;
        }
    }

    IEnumerator InitialAnimation()
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1.0f)
        {
            percent = currentTime / _textFadeInTime;
            currentTime += Time.deltaTime;

            _gamePlayingText.color = Color.Lerp(Color.clear, Color.white, percent);

            yield return null;
        }

        _gamePlayingText.color = Color.white;
        yield return new WaitForSeconds(_textShowTime);

        currentTime = 0.0f;
        percent = 0.0f;
        float deltaX = 2000;
        Vector3 endPosition = _gamePlayingText.transform.localPosition + Vector3.left * deltaX;

        while (percent < 1.0f)
        {
            percent = currentTime / _textSlideOutTime;
            currentTime += Time.deltaTime;

            _gamePlayingText.transform.localPosition = Vector3.Lerp(_gamePlayingText.transform.localPosition, endPosition, 0.05f);

            yield return null;
        }

        _gamePlayingInstance.gameObject.SetActive(true);
        _gamePlayingInstance.InitiateFromBaseController(GameCompleteCallback, _preset);
    }

    public void Deactivate()
    {
        _UIGameObject.SetActive(false);
        _gamePlayingInstance.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}

public enum EGamePlayingCallbackType
{
    GameReset,
    GameCompleted
}
