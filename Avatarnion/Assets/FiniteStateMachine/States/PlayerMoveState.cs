using UnityEngine;

using UnityEngine.InputSystem;
public class PlayerMoveState : State, IState
{
    public PlayerMoveState(PlayerController player) : base(player)
    {
    }

    public void Enter()
    {
        Debug.Log("Move State");
    }
    public void Update()
    {
        player.InputHandler.maxSpeed = player.InputHandler.walkSpeed;
        player.InputHandler.Walk();

        if (player.InputHandler.move.ReadValue<Vector2>() == Vector2.zero)
        {
            player.StateMachine.ChangeState(player.StateMachine.PlayerIdleState);
        }
        if (player.InputHandler.run.phase == InputActionPhase.Performed)
        {
            player.StateMachine.ChangeState(player.StateMachine.PlayerRunState);
        }

    }
    public void Exit()
    {

    }


}
