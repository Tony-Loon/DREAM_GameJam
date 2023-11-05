using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInspectState : PlayerBaseState
{
    private readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
    private const float AnimationDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;
    public PlayerInspectState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    private Collider curCollider;
    public override void Enter()
    {
        // on state enter
        stateMachine.InputReader.OnInteractionPerformed += LeaveInspectState;
        Collider curCollider = stateMachine.ColliderReader.getCurrentCollider();
        if (curCollider != null) {
            this.curCollider = curCollider;
            curCollider?.gameObject.GetComponent<IInspactable>()?.OnInspectStart();
            return;
        }
        LeaveInspectState();
    }

    public override void Exit()
    {
        // on state exit
        stateMachine.InputReader.OnInteractionPerformed -= LeaveInspectState;
        curCollider?.gameObject.GetComponent<IInspactable>()?.OnInspectStop();
        curCollider = null;
    }

    public override void Tick()
    {
        stateMachine.Animator.SetFloat(MoveSpeedHash, 0f, AnimationDampTime, Time.deltaTime);
    }

    private void LeaveInspectState()
    {
        stateMachine.SwitchState(new PlayerMoveState(stateMachine));
    }
}
