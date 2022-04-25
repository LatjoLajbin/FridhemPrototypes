using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceExplorer_PlayerMovement : MonoBehaviour
{
    Vector2 _movement;
    Rigidbody2D _rigidBody;
    public float _speed = 5;
    public float _turnSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        _rigidBody.AddForce(transform.up * _speed * _movement.y * Time.deltaTime, ForceMode2D.Impulse);
        _rigidBody.MoveRotation(_rigidBody.rotation + _turnSpeed * -_movement.x * Time.deltaTime);
    }
}
