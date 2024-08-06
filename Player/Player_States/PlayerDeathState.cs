using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager state)
    {
        state.CanFireSwitch(false);
        state.animator.SetInteger("Direction", 4);
        StartCoroutine(WaitForRevive(state));
    }

    public override void UpdateState(PlayerStateManager state)
    {
    }

    private IEnumerator WaitForRevive(PlayerStateManager state)
    {
        yield return new WaitForSeconds(state.Stats.GetRules()[PlayerStatNames.ReviveTimer]);

        state.SwitchState(state.MoveState);

        yield return new WaitForSeconds(state.Stats.GetRules()[PlayerStatNames.RefreshTimer]);

        state.CanFireSwitch(true);
    }
}
