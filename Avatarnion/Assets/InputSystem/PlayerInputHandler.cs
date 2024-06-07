using System;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls playerInput;
    private InputAction move;
    private Rigidbody playerRB;
    [SerializeField]
    private float movementForce = 1f;
    [SerializeField]
    private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;
    [SerializeField]
    private Camera playerCamera;


    void Awake()
    {
        playerRB = this.GetComponent<Rigidbody>();
        playerInput = new PlayerControls();

    }

    void OnEnable()
    {
        move = playerInput.Player.Move;
        playerInput.Player.Enable();
    }

    void OnDisable()
    {
        playerInput.Player.Disable();
    }

    private void FixedUpdate()
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

    //[HideInInspector] public PlayerController player;


    // public Vector2 RawMovementInput { get; private set; }

    // public int NormInputX { get; private set; }
    // public int NormInputY { get; private set; }


    // public Vector2 inputPosition;

    public Vector2 InputVector;


    public void OnMoveInput(InputAction.CallbackContext context)

    {
        // RawMovementInput = context.ReadValue<Vector2>();
        // if (Math.Abs(RawMovementInput.x) < 0.5f)
        // {
        //     NormInputX = 0;
        // }
        // else
        // {
        //     if (RawMovementInput.x < 0) NormInputX = -1;
        //     else NormInputX = 1;
        // }
        // if (Math.Abs(RawMovementInput.y) < 0.5f)
        // {
        //     NormInputY = 0;
        // }
        // else
        // {
        //     if (RawMovementInput.y < 0) NormInputY = -1;
        //     else NormInputY = 1;
        // }

        // InputVector = new Vector2(NormInputX, NormInputY);
    }

    public void OnTrajectoryInput(InputAction.CallbackContext context)
    {

        //inputPosition = context.ReadValue<Vector2>();


    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        // Debug.Log("Dash");
    }

    public void OnPauseMenuInput(InputAction.CallbackContext context)
    {
        // if (context.started)
        // {
        //     if (Time.timeScale != 0)
        //     {
        //         PauseMenuUI.OnPauseEnable.Invoke();
        //         Time.timeScale = 0;
        //     }
        //     else
        //     {
        //         PauseMenuUI.OnPauseDisable.Invoke();
        //         Time.timeScale = 1;
        //     }
        // }

    }

    public void OnMiniMapInput(InputAction.CallbackContext context)
    {
        // if (context.started)
        // {
        //     if (!MiniMapUI.isOpened)
        //     {
        //         MiniMapUI.OnMinimapEnable.Invoke();
        //     }
        //     else
        //     {
        //         MiniMapUI.OnMinimapDisable.Invoke();
        //     }
        // }
    }

    public void OnTutorialInput(InputAction.CallbackContext context)
    {
        // if (context.started)
        // {
        //     Debug.Log("Pressed");
        //     tutorial.isOpened = !tutorial.isOpened;
        //     tutorial.tutorialGO.SetActive(tutorial.isOpened);
        //     Time.timeScale = !tutorial.isOpened ? 1 : 0;
        // }
    }
}
