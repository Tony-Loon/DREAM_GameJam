using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractState : PlayerBaseState
{
    private readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
    private const float AnimationDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;
    public PlayerInteractState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    private Collider curCollider;
    public override void Enter()
    {
        // on state enter
        stateMachine.InputReader.OnInteractionPerformed += LeaveInteractState;
        Collider curCollider = stateMachine.ColliderReader.getCurrentCollider();
        if (curCollider != null) {
            this.curCollider = curCollider;
            curCollider?.gameObject.GetComponent<IInteractable>()?.OnInteractionStart();
            return;
        }
        LeaveInteractState();
    }

    public override void Exit()
    {
        // on state exit
        stateMachine.InputReader.OnInteractionPerformed -= LeaveInteractState;
        curCollider?.gameObject.GetComponent<IInteractable>()?.OnInteractionStop();
        curCollider = null;
    }

    public override void Tick()
    {
        // Add interactive functionality here
        curCollider?.gameObject.GetComponent<IInteractable>().Interaction();
        stateMachine.Animator.SetFloat(MoveSpeedHash, 0f, AnimationDampTime, Time.deltaTime);
    }

    private void LeaveInteractState()
    {
        stateMachine.SwitchState(new PlayerMoveState(stateMachine));
    }
}
