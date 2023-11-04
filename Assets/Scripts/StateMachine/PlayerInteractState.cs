using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractState : PlayerBaseState
{
    public PlayerInteractState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        // on state enter
        stateMachine.InputReader.OnInteractionPerformed += LeaveInteractState;
    }

    public override void Exit()
    {
        // on state exit
        stateMachine.InputReader.OnInteractionPerformed -= LeaveInteractState;
    }

    public override void Tick()
    {
        // Add interactive functionality here
    }

    private void LeaveInteractState()
    {
        stateMachine.SwitchState(new PlayerMoveState(stateMachine));
    }
}
