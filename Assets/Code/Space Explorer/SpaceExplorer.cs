using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceExplorer : MonoBehaviour, GamePreset
{
    SpaceExplorer_Player _player;

    void Awake()
    {
        _player = GetComponentInChildren<SpaceExplorer_Player>(true);
    }

    public void InitiateGame()
    {
        Camera.main.GetComponent<SpaceExplorer_CameraFollow>().CameraFollowEnabled = true;
        GetComponentInChildren<StarField>().Create();
        GetComponentInChildren<TargetDirectionMarker>().Initiate();
    }

    public void UpdateGame()
    {
    }

    public bool GameShouldReset()
    {
        return _player.Dead;
    }

    public bool GameIsComplete()
    {
        if (_player == null)
        {
            Debug.Log("No player object found.");
            return false;
        }

        return _player.FoundTarget;
    }
}
