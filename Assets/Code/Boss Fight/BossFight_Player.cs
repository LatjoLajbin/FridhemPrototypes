using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight_Player : MonoBehaviour
{
    // The particle effect is saved as a prefab in Assets/Particles/BossFight, and is created/removed during the game.
    public ParticleSystem _takeDamageEffect;

    public int Health { get; private set; }

    BossFight_PlayerMovement _playerMovement;

    int _startHealth = 2;
    float _timeToTakeDamage = 0.2f;
    float _timeToDie = 1.5f;

    bool _takingDamage = false;

    private void Awake()
    {
        Health = _startHealth;
        _playerMovement = GetComponent<BossFight_PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BossFight_Boss"))
        {
            if (_takingDamage == false)
                StartCoroutine(TakeDamage());
        }
    }

    IEnumerator TakeDamage()
    {
        _takingDamage = true;
        float currentTime = 0.0f;

        float percent = 0.0f;

        // If damage won't kill, trigger particle animation and wait until player can get hit again.
        if (Health > 1)
        {
            GameObject particle = Instantiate(_takeDamageEffect.gameObject, this.transform);
            Destroy(particle, _takeDamageEffect.main.duration);

            yield return new WaitForSeconds(_timeToTakeDamage);
        }
        else // If damage will kill, behavior should be different.
        {
            _playerMovement.SetEnabled(false);

            // Rotation at time of death
            Quaternion startRotation = transform.rotation;
            // Rotation to move towards: (should be the player sprite fallen over).
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
