using UnityEngine;


//WASD - move

public class PlayerMovementMinimized : MonoBehaviour
{
    #region SerializeField Private Fields

    [SerializeField] private GroundCheck _groundChesk;
    [SerializeField] private GameObject _playerTop;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _turnSmoothTime = 0.1f;

    [SerializeField] private bool _isGround = false;

    #endregion

    #region Private Fields

    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _moveDirection;

    private float _turnSmoothVelocity;

    private Vector3 _velocity;

    #endregion

    #region Monobehaviour Callbacks

    private void Update()
    {
        _isGround = _groundChesk.isGround;

        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        _moveDirection = new Vector3(_horizontalInput, 0f, _verticalInput).normalized;

        if (_moveDirection.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(_playerTop.transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            _playerTop.transform.rotation = Quaternion.Euler(0f, angle, 0f);

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

        if (Input.GetKey(KeyCode.Space) && _isGround)
        {
            _velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
        }

        _controller.Move(_velocity * Time.deltaTime);
    }

    #endregion
}
