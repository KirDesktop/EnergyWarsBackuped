using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObject : HealthScript
{
    private void Update()
    {
        if (health <= 0)
        {
            _destroyThis();
        }
    }

    public override void _destroyThis()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
