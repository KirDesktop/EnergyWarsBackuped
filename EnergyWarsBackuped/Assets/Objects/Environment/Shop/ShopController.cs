using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerEvents _playerEvents;


    private bool _isPlayer = false;
    [HideInInspector] public bool _isShop = false;

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Y)) && (_isPlayer) && !_playerEvents.isBuild)
        {
            _isShop = !_isShop;

            _canvasManager.openShop(_isShop);

            _playerMovement.canMove = !_isShop;
            _cameraManager.lockCamera(_isShop);
        }
        if(!_isPlayer)
        {
            _isShop = false;

            _canvasManager.openShop(_isShop);

            _playerMovement.canMove = !_isShop;
            _cameraManager.lockCamera(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canvasManager.showShopWarning(true);

            _isPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canvasManager.showShopWarning(false);

            _isPlayer = false;
        }
    }
}
