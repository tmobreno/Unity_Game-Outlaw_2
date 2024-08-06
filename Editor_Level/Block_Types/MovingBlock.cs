using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : EditorObject
{
    [field: SerializeField]
    public float MoveSpeed { get; private set; }

    public override void ObjectHit(GameObject bullet)
    {
        Destroy(bullet.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.CompareTag("Space"))
        {
            transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
        }
        if (this.transform.position.y < -4.3f) {
            Vector2 t = this.transform.position;
            this.transform.position = new Vector2(t.x, -4f);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.CompareTag("Space")) return;

        GameObject col = collision.gameObject;
        Transform t = this.transform;

        if (!CheckCollision(col, t)) return;

        if (col.GetComponent<BoosterBlock>() != null && col.transform.position.x == t.position.x)
        {
            MoveSpeed *= 1.5f;
            return;
        }
        if (col.GetComponent<MudBlock>() != null && col.transform.position.x == t.position.x)
        {
            MoveSpeed /= 1.5f;
            return;
        }

        RoundPosition(t);

        MoveSpeed = -MoveSpeed;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        Transform t = this.transform;
        if (col.GetComponent<BoosterBlock>() != null && col.transform.position.x == t.position.x)
        {
            MoveSpeed /= 1.5f;
        }
        if (col.GetComponent<MudBlock>() != null && col.transform.position.x == t.position.x)
        {
            MoveSpeed *= 1.5f;
            return;
        }
    }

    private void RoundPosition(Transform t)
    {
        float y = Mathf.Round(t.position.y * 4f) / 4f;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
