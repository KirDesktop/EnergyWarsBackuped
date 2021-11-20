using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;
    public bool isGround = false;

    private void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
       // Debug.Log(other.gameObject.name);
        if (!((_ground.value & (1 << other.gameObject.layer)) == 0))
        {
            isGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!((_ground.value & (1 << other.gameObject.layer)) == 0))
        {
            isGround = false;
        }
    }
}
