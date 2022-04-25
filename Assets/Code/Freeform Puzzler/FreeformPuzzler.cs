using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeformPuzzler : MonoBehaviour, GamePreset
{
    public GameObject _freeformPuzzlerUIObject;

    List<Vector3> _startingPositions;
    List<Quaternion> _startingRotations;
    List<Vector3> _startingScales;

    FreeformPuzzler_Key _key;

    public void Reset()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>(true);

        for (int i = 0; i < transforms.Length; ++i)
        {
            transforms[i].localPosition = _startingPositions[i];
            transforms[i].localRotation = _startingRotations[i];
            transforms[i].localScale = _startingScales[i];
        }

        _key.Reset();
    }

    void Awake()
    {
        _startingPositions = new List<Vector3>();
        _startingRotations = new List<Quaternion>();
        _startingScales = new List<Vector3>();

        Transform[] transforms = GetComponentsInChildren<Transform>(true);

        for (int i = 0; i < transforms.Length; ++i)
        {
            _startingPositions.Add(transforms[i].localPosition);
            _startingRotations.Add(transforms[i].localRotation);
            _startingScales.Add(transforms[i].localScale);
        }

        _key = GetComponentInChildren<FreeformPuzzler_Key>();
    }

    public void InitiateGame()
    {
        _freeformPuzzlerUIObject.SetActive(true);
    }

    public void UpdateGame()
    {
    }

    public bool GameShouldReset()
    {
        return false;
    }

    public bool GameIsComplete()
    {
        return _key.Completed;
    }
}
