using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState CurrentState { get; private set; }

    public PlayerIdleState PlayerIdleState;
    public PlayerMoveState PlayerMoveState;

    public StateMachine(PlayerController player)
    {
        this.PlayerIdleState = new PlayerIdleState(player);
        this.PlayerMoveState = new PlayerMoveState(player);
    }

    public void Initialize(IState StartingState)
    {
        CurrentState = StartingState;
        StartingState.Enter();
    }

    public void ChangeState(IState NextState)
    {
        CurrentState.Exit();
        CurrentState = NextState;
        NextState.Enter();
    }
    public void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.Update();
        }
    }
}
