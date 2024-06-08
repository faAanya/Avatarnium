using System;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementControls : MonoBehaviour
{
    #region Components
    private PlayerControls playerInput;
    [SerializeField]
    private Camera playerCamera;
    #endregion

    #region Actions
    private InputAction move;
    private InputAction run;
    #endregion
    private Rigidbody playerRB;


    #region Number Variables

    [SerializeField] private float movementForce;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float maxSpeed;

    private Vector3 forceDirection = Vector3.zero;
    #endregion




    void Awake()
    {
        playerRB = this.GetComponent<Rigidbody>();
        playerInput = new PlayerControls();

    }

    void OnEnable()
    {
        move = playerInput.Player.Move;
        run = playerInput.Player.Run;


        playerInput.Player.Enable();
    }

    void OnDisable()
    {
        playerInput.Player.Disable();
    }

    private void FixedUpdate()
    {
        if (run.phase == InputActionPhase.Performed)
        {
            maxSpeed = runSpeed;
        }
        else
        {
            maxSpeed = walkSpeed;
        }

        Walk();

        LookAt();
    }

    private void LookAt()
    {
        Vector3 direction = playerRB.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > .1f && direction.sqrMagnitude > .1f)
        {
            this.playerRB.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            playerRB.angularVelocity = Vector3.zero;
        }

    }
    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    public void Walk()
    {

        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        playerRB.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        Vector3 horizontalVelocity = playerRB.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            playerRB.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * playerRB.velocity.y;
        }
    }

}
