using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EditorObject : MonoBehaviour
{
    [SerializeField] protected string objectName;
    [SerializeField] protected int objectID;

    public abstract void ObjectHit(GameObject bullet);

    public int GetObjectID()
    {
        return objectID;
    }

    public string GetObjectName()
    {
        return objectName;
    }

    protected bool CheckCollision(GameObject col, Transform t)
    {
        if (col.CompareTag("Divider")) return false;
        if (col.GetComponent<EditorObject>() != null && col.transform.position.x != t.position.x) return false;
        if (col.GetComponent<PlayerStats>() != null) return false;
        return true;
    }
}
