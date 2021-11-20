using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassWallController : MonoBehaviour
{
    [SerializeField] private LayerMask _canBuildLayers;
    [SerializeField] private LayerMask _wallLayer;

    [SerializeField] private GameObject _redWall;
    [SerializeField] private GameObject _blueWall;

    public bool _canBuild = false;

    private void _setRedWall(bool isRed)
    {
        _redWall.SetActive(isRed);
        _blueWall.SetActive(!isRed);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!((_canBuildLayers.value & (1 << other.gameObject.layer)) == 0))
        {
            _canBuild = true;
        }
        if (!((_wallLayer.value & (1 << other.gameObject.layer)) == 0))
        {
            _canBuild = false;
        }
        _setRedWall(!_canBuild);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!((_canBuildLayers.value & (1 << other.gameObject.layer)) == 0))
        {
            _canBuild = false;
        }
        _setRedWall(!_canBuild);
    }
}