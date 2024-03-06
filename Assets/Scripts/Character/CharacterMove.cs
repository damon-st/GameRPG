using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
   [SerializeField] private float velocity;

    private CharacterLife _characterLife;
    private Rigidbody2D _rigidBody2D;
    private Vector2 _input;
    private Vector2 _directionMove;

    public Vector2 DirectionMove => _directionMove;
    public bool IsMoved => _directionMove.magnitude > 0f;

    private void Awake()
    {
        _characterLife = GetComponent<CharacterLife>();
        _rigidBody2D = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void Update()
    {
        if (_characterLife.Defeated)
        {
            _directionMove = Vector2.zero;
            return;
        }
       //Get keyword pressed
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
       //Asig direction x
       if(_input.x > 0.1f)
        {
            _directionMove.x = 1f;
        }else if (_input.x < 0f)
        {
            _directionMove.x = -1f;
        }
        else
        {
            _directionMove.x = 0f;
        }

        //Asing direction Y
        if (_input.y > 0.1f)
        {
            _directionMove.y = 1f;
        }
        else if (_input.y < 0f)
        {
            _directionMove.y = -1f;
        }
        else
        {
            _directionMove.y = 0f;
        }

    }

    private void FixedUpdate()
    {
        //MOVE CHARACTER
        _rigidBody2D.MovePosition(_rigidBody2D.position + _directionMove * velocity * Time.fixedDeltaTime);

    }
}
