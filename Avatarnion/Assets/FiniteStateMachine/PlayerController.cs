using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public PlayerMovementControls InputHandler { get; private set; }

    public StateMachine StateMachine { get; private set; }


    public float health;


    private void Awake()
    {
        InputHandler = GetComponent<PlayerMovementControls>();
    }

    void Start()
    {

        StateMachine = new StateMachine(this);
        StateMachine.Initialize(StateMachine.PlayerIdleState);
    }

    void Update()
    {
        StateMachine.Update();
    }

    public void IncreaseHealth(float buff)
    {
        health += buff;
    }
    public void DecreaseHealth(float damage)
    {
        health -= damage;
    }


}
