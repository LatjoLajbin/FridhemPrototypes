using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeformPuzzler_Key : MonoBehaviour
{
    public bool Completed { get; private set; }

    Rigidbody2D _rigidbody;

    float _speed = 3;

    public void Reset()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Completed = false;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _rigidbody.velocity + Vector2.right * _speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FreeformPuzzler_Lock"))
        {
            StartCoroutine(AnimateCompleted());
        }
    }

    IEnumerator AnimateCompleted()
    {
        _speed = 0;
        _rigidbody.velocity = Vector2.zero;
        
        yield return new WaitForSeconds(2);

        Completed = true;
    }
}
