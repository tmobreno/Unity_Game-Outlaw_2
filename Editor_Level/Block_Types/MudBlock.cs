using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudBlock : EditorObject
{
    public override void ObjectHit(GameObject bullet)
    {
        Bullet b = bullet.GetComponent<Bullet>();
        if (b.BulletSpeed > 4) { b.SetBulletSpeed(b.BulletSpeed / 1.6f); }
    }
}
