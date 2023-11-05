using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryState : PlayerBaseState
{
    public PlayerCarryState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    private Collider curCollider;

    public override void Enter()
    {
        stateMachine.InputReader.OnInteractionPerformed += LeaveInteractState;
        Collider curCollider = stateMachine.ColliderReader.getCurrentCollider();
        if (curCollider != null)
        {
            this.curCollider = curCollider;
            curCollider?.gameObject.GetComponent<ICarry>().PickUp();
            return;
        }
        LeaveInteractState();
    }

    public override void Exit()
    {
        stateMachine.InputReader.OnInteractionPerformed -= LeaveInteractState;
        curCollider?.gameObject.GetComponent<ICarry>().LayDown();
        curCollider = null;
    }

    public override void Tick()
    {
        curCollider?.gameObject.GetComponent<ICarry>().Carrying();
        CalculateMoveDirection();
        FaceMoveDirection();
        Move();

        //stateMachine.Animator.SetFloat(MoveSpeedHash, stateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
    }

    private void LeaveInteractState()
    {
        stateMachine.SwitchState(new PlayerMoveState(stateMachine));
    }
}