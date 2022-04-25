using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight_PlayerMovement : MonoBehaviour
{
    CharacterController2D _characterController;

    public float _speed = 40;
    
    float _horizontalMove;
    bool _jump = false;

    bool _enabled;

    public void SetEnabled(bool aEnabled)
    {
        _enabled = aEnabled;
    }

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController2D>();
        SetEnabled(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_enabled)
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal") * _speed;

            if (Input.GetButtonDown("Jump"))
                _jump = true;
        }
        else
        {
            _horizontalMove = 0;
            _jump = false;
        }    
    }

    private void FixedUpdate()
    {
        _characterController.Move(_horizontalMove * Time.fixedDeltaTime, false, _jump);
        _jump = false;
    }
}
