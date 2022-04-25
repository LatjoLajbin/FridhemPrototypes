using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceExplorer_Player : MonoBehaviour
{
    public bool FoundTarget { get; private set; }
    public bool Dead { get; private set; }

    private void Awake()
    {
        FoundTarget = false;
        Dead = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpaceExplorer_Target"))
        {
            collision.GetComponent<TargetDirectionMarker>().RemoveTarget();
            StartCoroutine(FoundTargetResponse());
        }
    }

    IEnumerator FoundTargetResponse()
    {
        yield return new WaitForSeconds(3.0f);
        FoundTarget = true;
    }
}
