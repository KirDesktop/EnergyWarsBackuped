using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerEvents _playerEvents;
    [SerializeField] private CanvasManager _canvasManager;

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _crystalInventory;

    [SerializeField] private int[] _itemNames;
    [SerializeField] private int[] _itemCount;

    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Image[] _spriteImages;
    [SerializeField] private GameObject[] _objectsToDrop;
    [SerializeField] private int[] _itemPriceEnergyType;
    [SerializeField] private int[] _itemPrice;
    [SerializeField] private GameObject[] _arrows;
    [SerializeField] private Text[] _texts;
    [SerializeField] private GameObject[] _inHandObjects;
    [SerializeField] private int _activeIdInHand = -1;
    //1-walls 2-energoDamager 3-mini gun 4-energo stick 5-destroyer 6-defence(turret)

    [HideInInspector] public bool _isInventory = false;
    private int _targetSlot = 0;


    public float glassHeight = 1f;
    private RaycastHit _lastHit;

    [HideInInspector] public int[] energy = { 0, 0, 0 };

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _crystalInventory.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            _crystalInventory.SetActive(false);
        }

        if (!_playerMovement.canMove)
        {
            _inventory.SetActive(false);
            _isInventory = false;
        }

        if (Input.GetKeyDown(KeyCode.C) && _playerMovement.canMove && !_playerEvents.isBuild)
        {
            _isInventory = !_isInventory;

            _inventory.SetActive(_isInventory);
        }

        if (_isInventory && !_playerEvents.isBuild)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (_targetSlot != 3)
                {
                    _targetSlot++;

                    _updateArrowInventory(_targetSlot);
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (_targetSlot != 0)
                {
                    _targetSlot--;

                    _updateArrowInventory(_targetSlot);
                }
            }

            if (_activeIdInHand != -1)
            {
                _inHandObjects[_activeIdInHand].SetActive(false);
            }

            if (_itemNames[_targetSlot] != -1)
            {
                _inHandObjects[_itemNames[_targetSlot]].SetActive(true);
                _activeIdInHand = _itemNames[_targetSlot];
            }

            //if (_activeIdInHand != 0)
            //{
            //    _inHandObjects[_activeIdInHand].SetActive(false);
            //    _activeIdInHand = _itemNames[_targetSlot];
            //}
        }

        if (_isInventory && Input.GetKeyDown(KeyCode.J) && (_itemNames[_targetSlot] != -1))
        {
            _dropItem(_itemNames[_targetSlot]);

            dropFromInventoryTargetSlotItem();
        }
    }

    public int getTargetSlotItemId()
    {
        return _itemNames[_targetSlot];
    }

    private void _updateArrowInventory(int slot)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i != slot)
            {
                _arrows[i].SetActive(false);
            }
        }

        _arrows[slot].SetActive(true);
    }

    public void purchaseItem(int itemId)
    {
        if (energy[_itemPriceEnergyType[itemId]-1] >= _itemPrice[itemId])
        {
            Debug.Log("Add " + itemId);
            energy[_itemPriceEnergyType[itemId]-1] -= _itemPrice[itemId];
            _canvasManager.setEnergySize(_itemPriceEnergyType[itemId] - 1, energy[_itemPriceEnergyType[itemId] - 1]);
            addItem(itemId);
        }
    }
    public void addItem(int itemName)
    {
        bool isExist = false;

        for(int i = 0; i < _itemNames.Length; i++)
        {
            if (_itemNames[i] == itemName)
            {
                isExist = true;

                _itemCount[i]++;
            }
        }
        if (!isExist)
        {
            bool isFull = true;
            int lastId = 0;

            for (int i = 0; i < _itemNames.Length; i++)
            {
                if (_itemNames[i] == -1)
                {
                    isFull = false;

                    lastId = i;
                    break;
                }
            }
            if (!isFull)
            {
                _spriteImages[lastId].sprite = _sprites[itemName];
                _itemNames[lastId] = itemName;
                _itemCount[lastId]++;
            }
            else
            {
                _dropItem(itemName);
            }
        }
        updateTexts();
    }

    public void dropFromInventoryTargetSlotItem()
    {
        if (_itemCount[_targetSlot] > 1)
        {
            _itemCount[_targetSlot]--;
        }
        else
        {
            _itemNames[_targetSlot] = -1;
            _spriteImages[_targetSlot].sprite = null;
            _itemCount[_targetSlot] = 0;
        }
        updateTexts();
    }

    private void _dropItem(int objId)
    {
        Instantiate(_objectsToDrop[objId], _playerEvents.dropSpawnPoint.position, Quaternion.identity);

        updateTexts();
    }

    public GameObject getInHandObject(int itemId)
    {
        return _inHandObjects[itemId];
    }

    public void updateTexts()
    {
        for(int i = 0; i < 4; i++)
        {
            _texts[i].text = _itemCount[i].ToString();
        }
    }

}
