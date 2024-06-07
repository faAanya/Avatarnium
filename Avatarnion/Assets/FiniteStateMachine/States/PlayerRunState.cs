using UnityEngine;


public class PlayerRunState : IState
{
    public PlayerController player;

    public PlayerRunState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Move State");
    }
    public void Update()
    {
        //player.transform.position = player.InputHandler.RawMovementInput;
        // player.RB.velocity = player.InputHandler.InputVector * player.movementSpeed;

        // if (player.InputHandler.InputVector == Vector2.zero)
        // {
        //     player.StateMachine.ChangeState(player.StateMachine.PlayerIdleState);
        // }
    }
    public void Exit()
    {

    }


}
