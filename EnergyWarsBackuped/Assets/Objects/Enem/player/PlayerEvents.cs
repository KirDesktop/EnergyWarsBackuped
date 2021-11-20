using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private GlassWallController _wallController;

    [SerializeField] private float _IvulnerabilityTime;
    private float _timeToTakeDamage;
    [SerializeField] private int _enemyWalkingDamage = 1;
    [SerializeField] private int _enemyWalkingShooter = 1;

    [SerializeField] public Transform dropSpawnPoint;

    [SerializeField] private GameObject _glassWall;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _wall;
    private GameObject _lastItem;

    private bool _canGrab = false;
    [HideInInspector] public bool isBuild = false;

    private void Update()
    {
        if (_canGrab && Input.GetKeyDown(KeyCode.E))
        {
            _playerInventory.addItem(_lastItem.GetComponent<ThrowObjectsController>().itemId);

            Destroy(_lastItem.transform.parent.gameObject);
            _canGrab = false;

            _canvasManager.showGrabWarning(false);
        }

        if (Input.GetKeyDown(KeyCode.F) && _playerInventory._isInventory && (_playerInventory.getTargetSlotItemId() == 0))
        {
            _cameraManager.changeView(false);

            isBuild = !isBuild;

            _glassWall.SetActive(isBuild);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isBuild && _wallController._canBuild)
        {
            Instantiate(_wall, _spawnPoint.transform.position, _glassWall.transform.rotation);

            isBuild = false;

            _glassWall.SetActive(false);

            _playerInventory.dropFromInventoryTargetSlotItem();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ThrowObjects"))
        {
            _canvasManager.showGrabWarning(true);

            _canGrab = true;
            _lastItem = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ThrowObjects"))
        {
            _canvasManager.showGrabWarning(false);

            _canGrab = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "enemy")
        {
            if (Time.time >= _timeToTakeDamage)
            {
                _timeToTakeDamage = Time.time + _IvulnerabilityTime;

                this.GetComponent<HealthScript>().health -= _enemyWalkingDamage;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "enemy")
        {
            if (Time.time >= _timeToTakeDamage)
            {
                _timeToTakeDamage = Time.time + _IvulnerabilityTime;

                this.GetComponent<HealthScript>().health -= _enemyWalkingDamage;
            }
        }
    }
}