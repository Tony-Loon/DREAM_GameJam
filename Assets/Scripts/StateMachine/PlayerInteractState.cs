using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractState : PlayerBaseState
{
    public PlayerInteractState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    private Collider curCollider;
    public override void Enter()
    {
        // on state enter
        stateMachine.InputReader.OnInteractionPerformed += LeaveInteractState;
        Collider curCollider = stateMachine.ColliderReader.getCurrentCollider();
        if (curCollider != null) {
            this.curCollider = curCollider;
            if (curCollider.gameObject.GetComponent<ICarry>() != null)
            {
                Exit();
                stateMachine.SwitchState(new PlayerCarryState(stateMachine));
                Debug.Log("Has ICarry!");
                return;
            }
            curCollider?.gameObject.GetComponent<IInteractable>().OnInteractionStart();
            return;
        }
        LeaveInteractState();
    }

    public override void Exit()
    {
        // on state exit
        stateMachine.InputReader.OnInteractionPerformed -= LeaveInteractState;
        curCollider?.gameObject.GetComponent<IInteractable>().OnInteractionStop();
        curCollider = null;
    }

    public override void Tick()
    {
        // Add interactive functionality here
        curCollider?.gameObject.GetComponent<IInteractable>().Interaction();

    }

    private void LeaveInteractState()
    {
        stateMachine.SwitchState(new PlayerMoveState(stateMachine));
    }
}
