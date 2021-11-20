using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.GetComponent<HealthScript>() != null)
        {
            //Debug.Log("-hp");
            other.GetComponent<HealthScript>().health -= damage;
        }

        Destroy(this.gameObject);
    }
}
