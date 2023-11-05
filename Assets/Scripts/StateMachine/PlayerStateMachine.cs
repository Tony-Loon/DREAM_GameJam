using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(ColliderReader))]
//[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : StateMachine
{
    public Vector3 Velocity;
    public float MovementSpeed  = 2.5f;
    public float JumpForce  = 5f;
    public float LookRotationDampFactor  = 10f;
    public Transform MainCamera { get; private set; }
    public InputReader InputReader { get; private set; }
    public ColliderReader ColliderReader { get; private set; }
    public Animator Animator;
    public CharacterController Controller { get; private set; }

    private void Start()
    {
        MainCamera = Camera.main.transform;

        InputReader = GetComponent<InputReader>();
        ColliderReader = GetComponent<ColliderReader>();
        //Animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();

        SwitchState(new PlayerMoveState(this));
    }
}
