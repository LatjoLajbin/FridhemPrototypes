using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarField : MonoBehaviour
{
    int _maxStars = 10000;
    int _universeSize = 100;

    ParticleSystem.Particle[] _points;
    ParticleSystem _particleSystem;

    public void Create()
    {
        _points = new ParticleSystem.Particle[_maxStars];

        for (int i = 0; i < _maxStars; ++i)
        {
            Vector3 randomPosition = Random.insideUnitSphere * _universeSize;
            randomPosition.z = transform.position.z;
            _points[i].position = randomPosition;
            _points[i].startSize = Random.Range(0.045f, 0.055f);
            _points[i].startColor = new Color(1, 1, 1, 1);
        }

        _particleSystem = gameObject.GetComponent<ParticleSystem>();
        _particleSystem.SetParticles(_points, _points.Length);
    }
}
