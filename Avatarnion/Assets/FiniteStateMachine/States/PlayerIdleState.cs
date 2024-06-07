using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    public PlayerController player;

    public PlayerIdleState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Idle State");
    }
    void IState.Update()
    {
        // if (player.InputHandler.InputVector != Vector2.zero)
        // {
        //     player.StateMachine.ChangeState(player.StateMachine.PlayerMoveState);
        // }
    }
    public void Exit()
    {

    }

    // Start is called before the first frame update



}
