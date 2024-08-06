using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndestructibleBlock : EditorObject
{ 
    public override void ObjectHit(GameObject bullet)
    {
        Destroy(bullet.gameObject);
    }
}
