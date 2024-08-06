using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReloadState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager state)
    {
        state.CanFireSwitch(false);
        state.animator.SetInteger("Direction", 5);
        StartCoroutine(WaitForReload(state));
    }

    public override void UpdateState(PlayerStateManager state)
    {
    }

    private IEnumerator WaitForReload(PlayerStateManager state)
    {
        yield return new WaitForSeconds(state.Stats.GetRules()[PlayerStatNames.ReloadTimer]);

        state.SwitchState(state.MoveState);

        yield return new WaitForSeconds(state.Stats.GetRules()[PlayerStatNames.RefreshTimer]);

        state.CanFireSwitch(true);
    }
}
