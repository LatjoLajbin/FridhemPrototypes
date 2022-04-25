using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class GameStartController : MonoBehaviour
{
    public RawImage _background;
    public TMPro.TextMeshProUGUI _gameStartText;

    public GameObject _UIGameObject;

    float _startGameAnimationTime = 2.0f;

    public IEnumerator CR_StartGame(Action aCallback)
    {
        float currentTime = 0.0f;

        Color bgStartColor = _background.color;
        Color txtStartColor = _gameStartText.color;

        float percent = 0.0f;

        while (percent < 1.0f)
        {
            percent = currentTime / _startGameAnimationTime;
            currentTime += Time.deltaTime;

            _background.color = Color.Lerp(bgStartColor, Color.clear, percent);
            _gameStartText.color = Color.Lerp(txtStartColor, Color.clear, percent);            

            yield return null;
        }

        _background.color = Color.clear;
        _gameStartText.color = Color.clear;

        aCallback();
    }

    public void Initiate()
    {
        _UIGameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _UIGameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
