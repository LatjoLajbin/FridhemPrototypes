using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour, GamePreset
{
    BossFight_Player _player;
    BossFight_Boss _boss;

    void Awake()
    {
        _player = GetComponentInChildren<BossFight_Player>(true);
        _boss = GetComponentInChildren<BossFight_Boss>(true);
    }

    public void InitiateGame()
    {
    }

    public void UpdateGame()
    {
    }

    public bool GameShouldReset()
    {
        if (_player == null)
        {
            Debug.Log("No player object found.");
            return false;
        }

        return _player.Health <= 0;
    }

    public bool GameIsComplete()
    {
        if (_boss == null)
        {
            Debug.Log("No boss object found.");
            return false;
        }

        return _boss.Health <= 0;
    }
}
