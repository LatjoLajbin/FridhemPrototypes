using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class GameDoneController : MonoBehaviour
{
    public GameObject _UIGameObject;
    public RawImage _background;
    public TMPro.TextMeshProUGUI _gameDoneInfoText;

    Action _stateCompleteCallback;

    public void Initiate(Action onGameDoneStateCallback)
    {
        _stateCompleteCallback = onGameDoneStateCallback;
        _UIGameObject.SetActive(true);

        StartCoroutine(AnimateGameDone());
    }

    public void Deactivate()
    {
        _UIGameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    IEnumerator AnimateGameDone()
    {
        float currentTime = 0;
        float timeToShowText = 2.0f;

        float percent = 0.0f;

        Color bgColor = _background.color;

        while (percent < 1.0f)
        {
            percent = currentTime / timeToShowText;
            currentTime += Time.deltaTime;

            _background.color = Color.Lerp(Color.clear, bgColor, percent);
            _gameDoneInfoText.color = Color.Lerp(Color.clear, Color.white, percent);

            yield return null;
        }

        _background.color = bgColor;
        _gameDoneInfoText.color = Color.white;

        StartCoroutine(WaitForGameEnd());
    }

    IEnumerator WaitForGameEnd()
    {
        yield return new WaitForSeconds(3.0f);

        _stateCompleteCallback();
    }
}
