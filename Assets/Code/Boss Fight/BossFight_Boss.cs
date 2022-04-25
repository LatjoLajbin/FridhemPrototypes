using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight_Boss : MonoBehaviour
{
    public int Health { get; private set; }

    int _startHealth = 10;
    float _timeToTakeDamage = 0.5f;
    float _timeToDie = 3.0f;

    bool _takingDamage = false;

    private void Awake()
    {
        Health = _startHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    }

    IEnumerator TakeDamage()
    {
        _takingDamage = true;
        float currentTime = 0.0f;

        float percent = 0.0f;

        // If damage won't kill, trigger some behavior
        if (Health > 1)
        {
            yield return new WaitForSeconds(_timeToTakeDamage);
        }
        else // If damage will kill, behavior should be different.
        {
            // Rotation at time of death
            Quaternion startRotation = transform.rotation;
            // Rotation to move towards: (boss sprite fallen over)
            Quaternion endRotation = transform.rotation * Quaternion.Euler(0, 0, 90);

            while (percent <= 1.0f)
            {
                percent = currentTime / _timeToDie;
                currentTime += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, percent);

                yield return null;
            }
        }

        Health -= 1;
        _takingDamage = false;
    }
}
