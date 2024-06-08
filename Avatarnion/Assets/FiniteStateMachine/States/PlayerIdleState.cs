using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State, IState
{
    public PlayerIdleState(PlayerController player) : base(player)
    {
    }

    public void Enter()
    {
        Debug.Log("Idle State");
    }
    void IState.Update()
    {
        if (player.InputHandler.move.ReadValue<Vector2>() != Vector2.zero)
        {
            player.StateMachine.ChangeState(player.StateMachine.PlayerMoveState);
        }
    }
    public void Exit()
    {

    }

    // Start is called before the first frame update



}
