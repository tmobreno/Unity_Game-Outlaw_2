using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GlideBlock : EditorObject
{
    private bool canMove = true;

    [field: SerializeField]
    public float MoveStrength { get; private set; }

    private bool activeCollisions = false;

    public override void ObjectHit(GameObject bullet)
    {
        activeCollisions = true;

        Destroy(bullet.gameObject);
        Transform t = this.transform;

        if (canMove)
        {
            canMove = false;
            t.DOLocalMoveY(t.position.y + MoveStrength, 0.75f).OnComplete(() => {
                RoundPosition(t);
            });
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!activeCollisions) return;

        GameObject col = collision.gameObject;
        Transform t = this.transform;

        if (!CheckCollision(col, t)) return;

        if (col.GetComponent<BoosterBlock>() != null && col.transform.position.x == t.position.x)
        {
            MoveStrength *= 1.5f;
            return;
        }

        if (col.GetComponent<DestructibleBlock>() != null && col.transform.position.x == t.position.x)
        {
            Destroy(col);
        }

        MoveStrength = -MoveStrength;
        canMove = false;
        t.DOLocalMoveY(t.position.y + MoveStrength, 0.75f).OnComplete(() => {
            RoundPosition(t);
        });
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        Transform t = this.transform;
        if (col.GetComponent<BoosterBlock>() != null && col.transform.position.x == t.position.x)
        {
            MoveStrength /= 1.5f;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!activeCollisions) return;

        Vector2 col = collision.gameObject.transform.position;
        Transform t = this.transform;

        if (canMove && col.x == t.position.x && col.y == t.position.y)
        {
            int r = Random.Range(0, 3);
            if (r == 0) { return; }
            MoveStrength = (r == 1) ? MoveStrength : -MoveStrength;
            canMove = false;
            t.DOLocalMoveY(t.position.y + MoveStrength, 0.75f).OnComplete(() =>
            {
                RoundPosition(t);
            });
        }
    }

    private void RoundPosition(Transform t)
    {
        float y = Mathf.Round(t.position.y * 4f) / 4f;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        canMove = true;
    }
}
