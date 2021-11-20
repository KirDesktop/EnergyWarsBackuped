using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private GameObject _cameraSimple;
    [SerializeField] private GameObject _cameraShooting;
    [SerializeField] private PlayerEvents _playerEvents;
    [SerializeField] private GameObject _followTarget;
    [SerializeField] private GameObject _followTarget2;

    [SerializeField] private float _maxRotate;
    [SerializeField] private float _minRotate;

    [SerializeField] private float _rotationPower;

    [SerializeField] private Vector3 _look;

    [SerializeField] private GameObject _aimCross;

    [HideInInspector] public bool isShootingView = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !_playerEvents.isBuild && _playerMovement.canMove)
        {
            isShootingView = !isShootingView;

            changeView(isShootingView);
        }
        _look.x = Input.GetAxis("Mouse X");
        _look.y = -Input.GetAxis("Mouse Y");

        _followTarget.transform.rotation *= Quaternion.AngleAxis(_look.x * _rotationPower, Vector3.up);


        _followTarget.transform.rotation *= Quaternion.AngleAxis(_look.y * _rotationPower, Vector3.right);

        var angles = _followTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = _followTarget.transform.localEulerAngles.x;

        if (angle > 180 && angle < _maxRotate)
        {
            angles.x = _maxRotate;
        }
        else if (angle < 180 && angle > _minRotate)
        {
            angles.x = _minRotate;
        }

        _followTarget.transform.localEulerAngles = angles;
        _followTarget2.transform.rotation = _followTarget.transform.rotation;
    }

    public void lockCamera(bool isActive)
    {
        if (isActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (isShootingView)
        {
            _cameraShooting.SetActive(!isActive);
        }
        else
        {
            _cameraSimple.SetActive(!isActive);
        }
    }

    public void changeView(bool isShoot)
    {
        isShootingView = isShoot;

        _aimCross.SetActive(isShoot);

        _cameraSimple.SetActive(!isShoot);
        _cameraShooting.SetActive(isShoot);
    }
}
