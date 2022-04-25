using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class GamePlaying : MonoBehaviour
{
    Action<EGamePlayingCallbackType> _baseGameCallback;
    GameObject _presetObject;

    public void InitiateFromBaseController(Action<EGamePlayingCallbackType> aBaseGameCallback, EGamePreset aPreset)
    {
        _baseGameCallback = aBaseGameCallback;

        switch (aPreset)
        {
            case EGamePreset.Boss_Fight:
                _presetObject = this.gameObject.GetComponentInChildren<BossFight>(true).gameObject;
                break;
            case EGamePreset.Space_Explorer:
                _presetObject = this.gameObject.GetComponentInChildren<SpaceExplorer>(true).gameObject;
                break;
            case EGamePreset.Talk_Em_Up:
                _presetObject = this.gameObject.GetComponentInChildren<TalkEmUp>(true).gameObject;
                break;
            case EGamePreset.Freeform_Puzzler:
                _presetObject = this.gameObject.GetComponentInChildren<FreeformPuzzler>(true).gameObject;
                break;
            default:
                throw (new Exception("Undefined preset."));
        }

        StartCoroutine(Game());
    }

    IEnumerator Game()
    {
        _presetObject.SetActive(true);

        GamePreset preset = _presetObject.GetComponent<GamePreset>();

        preset.InitiateGame();

        while (true)
        {
            preset.UpdateGame();

            if (preset.GameShouldReset())
                _baseGameCallback(EGamePlayingCallbackType.GameReset);
            else if (preset.GameIsComplete())
                _baseGameCallback(EGamePlayingCallbackType.GameCompleted);

            yield return null;
        }
    }
}
