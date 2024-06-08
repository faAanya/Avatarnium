using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunState : State, IState
{
    public PlayerRunState(PlayerController player) : base(player)
    {
    }

    public void Enter()
    {
        Debug.Log("Run State");
    }
    public void Update()
    {
        player.InputHandler.maxSpeed = player.InputHandler.runSpeed;
        player.InputHandler.Walk();

        if (player.InputHandler.run.phase == InputActionPhase.Waiting)
        {
            player.StateMachine.ChangeState(player.StateMachine.PlayerMoveState);
        }
        if (player.InputHandler.move.ReadValue<Vector2>() == Vector2.zero)
        {
            player.StateMachine.ChangeState(player.StateMachine.PlayerIdleState);
        }
    }
    public void Exit()
    {

    }


}
