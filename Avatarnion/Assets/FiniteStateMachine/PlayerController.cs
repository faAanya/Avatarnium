using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D RB;
    public PlayerInputHandler InputHandler { get; private set; }

    public StateMachine StateMachine { get; private set; }


    #region 
    public float health;
    public float movementSpeed;
    public int score;

    #endregion
    private void Awake()
    {
        Time.timeScale = 1;
        InputHandler = GetComponent<PlayerInputHandler>();
    }

    void Start()
    {

        StateMachine = new StateMachine(this);
        RB = GetComponent<Rigidbody2D>();
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
    public void IncreaseScore()
    {
        score++;
    }
    public void ResetScore()
    {
        score = 0;
    }

}
