using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterBlock : EditorObject
{
    public override void ObjectHit(GameObject bullet)
    {
        Bullet b = bullet.GetComponent<Bullet>();
        if (b.BulletSpeed < 8) { b.SetBulletSpeed(b.BulletSpeed * 3f); }
    }
}
