using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] private GameObject _playerTop;
    [SerializeField] GameObject _player;

    [SerializeField] private float[] _speed;
    [SerializeField] private float[] _duration;
    [SerializeField] private float[] _spread;
    [SerializeField] private float[] _liveTime;
    [SerializeField] private Transform _damagePivot;
    [SerializeField] private GameObject damageArea;
    [SerializeField] private float _turnSmoothTime;
    [SerializeField] private float[] _timeToUnlockRotation;

    [SerializeField] private LayerMask _ground;

    private float _timeToShoot = 0;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _cross;
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private PlayerInventory _playerInventory;

    //public bool isShooting = false;

    private Transform _shootingPos;
    private int ind = 0;
    public bool isRotated = true;

    [SerializeField] private float toRotate;

    private float timeToUnlock;
    
    private void Update()
    {
        if (!isRotated)
        {
            float angle = Mathf.SmoothDampAngle(_playerTop.transform.eulerAngles.y, toRotate, ref _turnSmoothVelocity, _turnSmoothTime);
            _playerTop.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            if(Time.time >= timeToUnlock)
            {
                isRotated = true;
            }
        }

        if (((_cameraManager.isShootingView && ((_playerInventory.getTargetSlotItemId() == 1) || (_playerInventory.getTargetSlotItemId() == 2))) || 
            (((_playerInventory.getTargetSlotItemId() == 3) || (_playerInventory.getTargetSlotItemId() == 4))))&& (_playerInventory.getTargetSlotItemId() != 5))
        {
            ind = _playerInventory.getTargetSlotItemId() - 1;
            if((_playerInventory.getTargetSlotItemId() == 3) || (_playerInventory.getTargetSlotItemId() == 4))
            {
                _shootingPos = _damagePivot;
            }
            else
            {
                _shootingPos = _playerInventory.getInHandObject(_playerInventory.getTargetSlotItemId()).transform;
            }

            if ((Input.GetKeyDown(KeyCode.Mouse0) && ((_playerInventory.getTargetSlotItemId() == 3) || (_playerInventory.getTargetSlotItemId() == 4))) ||
                (Input.GetKey(KeyCode.Mouse0) && ((_playerInventory.getTargetSlotItemId() == 1) || (_playerInventory.getTargetSlotItemId() == 2))))
            {
                if(Time.time >= _timeToShoot)
                {
                    if ((_playerInventory.getTargetSlotItemId() == 3) || (_playerInventory.getTargetSlotItemId() == 4))
                    {
                        _shoot(damageArea);
                    }
                    else
                    {
                        _shoot(_bullet);
                    }

                    _timeToShoot = Time.time + _duration[ind];
                }
            }
        }
        if(_cameraManager.isShootingView && (_playerInventory.getTargetSlotItemId() == 3 || _playerInventory.getTargetSlotItemId() == 4))
        {
            ind = _playerInventory.getTargetSlotItemId() - 1;
        }
    }
    private float _turnSmoothVelocity;
    private void _shoot(GameObject bulletT)
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, _ground))
        {
            Vector3 spawn = _shootingPos.transform.GetChild(0).transform.position;
            GameObject shooted = Instantiate(bulletT, spawn, _shootingPos.transform.GetChild(0).transform.rotation);
            shooted.transform.LookAt(hit.point);
            shooted.GetComponent<Rigidbody>().AddForce(_speed[ind] * shooted.transform.forward + new Vector3(Random.Range(-_spread[ind], +_spread[ind]), Random.Range(-_spread[ind], +_spread[ind]), Random.Range(-_spread[ind], +_spread[ind])), ForceMode.VelocityChange);
            Destroy(shooted, _liveTime[ind]);

            Vector3 shootDir = (hit.point - _playerTop.transform.position);
            float targetAngle = Mathf.Atan2(shootDir.x, shootDir.z) * Mathf.Rad2Deg;
            toRotate = targetAngle;

            isRotated = false;
            timeToUnlock = Time.time + _timeToUnlockRotation[ind];
        }
    }

}
