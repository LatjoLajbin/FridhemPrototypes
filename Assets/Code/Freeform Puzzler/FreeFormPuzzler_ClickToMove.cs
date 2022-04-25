using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFormPuzzler_ClickToMove : MonoBehaviour
{
    public GameObject _pivotObject;
    public Vector2 _onClickMoveAmount;
    public float _onClickRotateDegrees;

    public float _timeToMove;

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.transform == this.transform)
                {
                    StartCoroutine(MoveToNewState());
                }
            }
        }
    }

    IEnumerator MoveToNewState()
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        Vector3 startPosition = _pivotObject.transform.localPosition;
        Quaternion startRotation = _pivotObject.transform.localRotation;

        Vector3 endPosition = startPosition + new Vector3(_onClickMoveAmount.x, _onClickMoveAmount.y, startPosition.z);
        Quaternion endRotation = startRotation * Quaternion.Euler(Vector3.forward * _onClickRotateDegrees);

        while (percent < 1.0f)
        {
            percent = currentTime / _timeToMove;
            currentTime += Time.deltaTime;

            _pivotObject.transform.localPosition = Vector3.Lerp(startPosition, endPosition, percent);
            _pivotObject.transform.localRotation = Quaternion.Lerp(startRotation, endRotation, percent);

            yield return null;
        }

        _pivotObject.transform.localPosition = endPosition;
        _pivotObject.transform.localRotation = endRotation;
    }
}
