using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TargetDirectionMarker : MonoBehaviour
{
    public Image _directionMarkerUIObject;

    public GameObject _player;

    float _canvasWidth;
    float _canvasHeight;

    Vector3 _startScale;
    float distanceCutoff = 25;
    float _scaleIncrease = 0.1f;

    private void Awake()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        RectTransform rectTransform = canvas.GetComponent<RectTransform>();
        _canvasWidth = rectTransform.rect.width;
        _canvasHeight = rectTransform.rect.height;
        _startScale = _directionMarkerUIObject.rectTransform.localScale;
    }

    public void Initiate()
    {
        _directionMarkerUIObject.gameObject.SetActive(true);
    }

    public void RemoveTarget()
    {
        _directionMarkerUIObject.CrossFadeAlpha(0.0f, 1.0f, false);
    }

    // Update is called once per frame
    void Update()
    {
        // Get direction from player to target, establish angle
        Vector2 targetDirection = (transform.position - _player.transform.position).normalized;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x);

        float zPosition = _directionMarkerUIObject.transform.position.z;

        // Set position
        Vector2 uiPosition = new Vector2(_canvasWidth * 0.5f * Mathf.Cos(targetAngle), _canvasHeight * 0.5f * Mathf.Sin(targetAngle));
        
        // Reduce position somewhat so that sprite will be shown within screen borders
        uiPosition *= .8f;
        _directionMarkerUIObject.rectTransform.localPosition = new Vector3(uiPosition.x, uiPosition.y, zPosition);

        // Increase scale if player is close to target
        float distance = (transform.position - _player.transform.position).sqrMagnitude;
        float percent = 1.0f - (distance / (distanceCutoff * distanceCutoff));
        // Make sure that the increase isn't too small or too big
        percent = ((percent < 0) ? 0 : (percent > 1) ? 1 : percent);
        // Set scale
        _directionMarkerUIObject.rectTransform.localScale = _startScale + Vector3.Lerp(Vector3.zero, Vector3.one * _scaleIncrease, percent);

        // Sprite 90 degree rotation offset
        float angleOffset = -Mathf.PI * 0.5f;

        // Set rotation
        _directionMarkerUIObject.rectTransform.localRotation = Quaternion.AngleAxis((targetAngle + angleOffset) * Mathf.Rad2Deg, Vector3.forward);
    }
}
