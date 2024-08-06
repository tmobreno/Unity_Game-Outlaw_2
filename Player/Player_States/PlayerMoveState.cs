using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public float WalkSoundTimer { get; private set; }

    private int curState;

    public override void EnterState(PlayerStateManager state)
    {
    }

    public override void UpdateState(PlayerStateManager state)
    {
        if (Input.GetKey(state.ActivePlayerControls.MoveRight))
        {
            Move(state, Vector3.right, state.Stats.GetRules()[PlayerStatNames.MoveSpeed], 0);
            curState = 1;
        }
        else if (Input.GetKey(state.ActivePlayerControls.MoveLeft))
        {
            Move(state, Vector3.right, -state.Stats.GetRules()[PlayerStatNames.MoveSpeed], 0);
            curState = 1;
        }
        else if (Input.GetKey(state.ActivePlayerControls.MoveUp))
        {
            Move(state, Vector3.up, state.Stats.GetRules()[PlayerStatNames.MoveSpeed], 1);
            curState = 0;
        }
        else if (Input.GetKey(state.ActivePlayerControls.MoveDown))
        {
            Move(state, Vector3.up, -state.Stats.GetRules()[PlayerStatNames.MoveSpeed], 2);
            curState = 2;
        }
        else
        {
            state.animator.SetInteger("Direction", 3);
            curState = 1;
        }

        if (Input.GetKey(state.ActivePlayerControls.Fire))
        {
            Fire(state);
        }
    }

    private void Move(PlayerStateManager state, Vector3 direction, float moveSpeed, int animDir)
    {
        if (Time.time > WalkSoundTimer)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Walk", transform.position);
            WalkSoundTimer = Time.time + 0.2f;
        }
        transform.position += direction * moveSpeed * Time.deltaTime;
        state.animator.SetInteger("Direction", animDir);
    }

    private void Fire(PlayerStateManager state)
    {
        if (!state.CanFire) { return; }
        
        FMODUnity.RuntimeManager.PlayOneShot("event:/Fire", transform.position);

        Transform t = state.FiringPoints[curState].transform;
        GameObject g = Instantiate(state.Projectile, t.position, t.rotation);
        g.GetComponent<Bullet>().SetBulletSpeed(state.Stats.GetRules()[PlayerStatNames.BulletSpeed]);
        g.GetComponent<Bullet>().SetPlayerNumber(state.PlayerSide);

        state.SwitchState(state.ReloadState);
    }
}
