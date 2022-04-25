using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceExplorer_CameraFollow : MonoBehaviour
{
    public bool CameraFollowEnabled { get; set; }

    public GameObject _spaceExplorerPlayer;
    Vector3 _offset;

    float _smoothSpeed = 0.05f;

    void Awake()
    {
        CameraFollowEnabled = false;
        _offset = transform.position - _spaceExplorerPlayer.transform.position;
        _offset.z = 0;
    }

    private void FixedUpdate()
    {
        if (CameraFollowEnabled)
        {
            float playerPositionX = _spaceExplorerPlayer.transform.position.x;
            float playerPositionY = _spaceExplorerPlayer.transform.position.y;
            float cameraPositionZ = transform.position.z;
            Vector3 target = new Vector3(playerPositionX, playerPositionY, cameraPositionZ) + _offset;

            // Every update: move 10% of the distance towards the player.
            transform.position = Vector3.Lerp(transform.position, target, _smoothSpeed);
        }
    }
}
