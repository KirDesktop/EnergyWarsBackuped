using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private CanvasManager _canvacManager;

    [SerializeField] private int _generatorType = 1;

    [SerializeField] private float _generationSpeed = 1f;
    [SerializeField] private int _amountSpawn = 1;

    private bool _isPlayer = false;
    private float _timeToSpawn;

    private void Update()
    {
        if (_isPlayer) 
        {
            if (Time.time >= _timeToSpawn)
            {
                _timeToSpawn = Time.time + _generationSpeed;

                _playerInventory.energy[_generatorType-1] += _amountSpawn;
                _canvacManager.setEnergySize(_generatorType-1, _playerInventory.energy[_generatorType-1]);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayer = false;
        }
    }
}
