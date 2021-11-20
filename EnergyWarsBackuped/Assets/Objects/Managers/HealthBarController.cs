using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Slider _bar;
    [SerializeField] private GameObject _pivot;
    [SerializeField] private Transform _center;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _thisLayer;

    private void Awake()
    {
        _bar.maxValue = this.GetComponent<HealthScript>().health;
        _bar.value = _bar.maxValue;
    }

    private void Update()
    {
        _bar.value = this.GetComponent<HealthScript>().health;

        RaycastHit hit;

        

        bool ist = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, _thisLayer);

        //Debug.Log(this.gameObject.name + " " + ist);

        if (ist)
        {
            if(hit.transform == this.gameObject.transform)
            {
                if (Vector3.Distance(Camera.main.transform.position, _center.transform.position) <= _maxDistance)
                {
                    //Debug.Log(this.gameObject.name + " lol why");
                    _bar.gameObject.SetActive(true);

                    _pivot.transform.position = Camera.main.WorldToScreenPoint(_center.transform.position);
                }
                else
                {
                    _bar.gameObject.SetActive(false);
                }
            }
            else
            {
                _bar.gameObject.SetActive(false);
            }
        }
        else
        {
            _bar.gameObject.SetActive(false);
        }
    }
}
