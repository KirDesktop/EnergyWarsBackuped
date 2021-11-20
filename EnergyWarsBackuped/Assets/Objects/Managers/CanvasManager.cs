using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Other")]
    [SerializeField] private Text[] _typeText;
    [SerializeField] private GameObject _shopWarning;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _grabWarning;

    public void setEnergySize(int number, int size)
    {
        _typeText[number].text = size.ToString();
    }

    public void showShopWarning(bool isActive)
    {
        _shopWarning.SetActive(isActive);
    }

    public void openShop(bool isActive)
    {
        _shop.SetActive(isActive);
    }

    public void showGrabWarning(bool isActive)
    {
        _grabWarning.SetActive(isActive);
    }
}
