using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//J - throw away item
//Left, right arrow - move active slot in inventary
//c - on/off inventary
//hold TAB - show energy count
//WASD - move
//F - build walls while it in active slot in inventary
//Z - change view to shooting

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GroundCheck _groundChesk;
    [SerializeField] private GameObject _playerTop;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private GameObject pivot1;
    [SerializeField] private GameObject pivot2;

    [Header("Player Settings")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _turnSmoothTime = 0.1f;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _isGround = false;

    [HideInInspector] public bool canMove = true;
    

    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _moveDirection;

    private float _turnSmoothVelocity;

    [SerializeField] private Vector3 _velocity;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        //pivot2.transform.position = this.transform.position;
        //pivot1.transform.position = this.transform.position;
    }

    private void Update()
    {
        //Debug.Log(_groundChesk.isGround);
        _isGround = _groundChesk.isGround;

        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        _moveDirection = new Vector3(_horizontalInput, 0f, _verticalInput).normalized;

        if ((_moveDirection.magnitude >= 0.1) && canMove)
        {
            //+ _cameraTransform.eulerAngles.y
            float targetAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;

            if (_playerShooting.isRotated)
            {
                float angle = Mathf.SmoothDampAngle(_playerTop.transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
                _playerTop.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir * _moveSpeed * Time.deltaTime);
        }

        if (_isGround)
        {
            _velocity.y = -2f;
        }
        else
        {
            _velocity.y += _gravity * Time.deltaTime;
        }

        if (canMove && Input.GetKey(KeyCode.Space) && _isGround)
        {
            _velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
        }

        _controller.Move(_velocity * Time.deltaTime);
    }
}
