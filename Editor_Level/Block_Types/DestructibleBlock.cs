using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBlock : EditorObject
{
    public override void ObjectHit(GameObject bullet)
    {
        Destroy(bullet.gameObject);
        Destroy(this.gameObject);
    }
}
