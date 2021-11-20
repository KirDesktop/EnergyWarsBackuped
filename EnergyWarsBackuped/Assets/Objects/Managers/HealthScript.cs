using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int health = 1;

    public virtual void _destroyThis()
    {
        Destroy(this.gameObject);
    }
}
