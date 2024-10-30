using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private GroundCheck groundCheck;
    public bool IsOnGround => groundCheck.IsOnGround;
    private PlayerInput input;
    private Rigidbody2D rb;

    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction jumpAction;

    [Header("Jump Settings: ")]
    public float jumpHeight = 10f;
    public float jumpTime = 1f;

    [Header("Move Settings: ")]
    public float moveSpeed = 10f;
    public float velocityChangeSpeed = 10f;

    private void Awake()
    {
        groundCheck = GetComponent<GroundCheck>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input == null)
        {
            input = GetComponent<PlayerInput>();
            moveAction = input.actions["Move"];
            attackAction = input.actions["Attack"];
            jumpAction = input.actions["Jump"];
        }

        if (attackAction.triggered)
        {
            // Attack
        }

        Vector2 toMove = moveAction.ReadValue<Vector2>();
        toMove.x *= moveSpeed;

        if (jumpAction.triggered && IsOnGround)
        {
            // Jump
            float jumpVelocity = (jumpHeight + 0.5f * Physics2D.gravity.y * jumpTime * jumpTime) / jumpTime;
            toMove += Vector2.up * jumpVelocity;
        }

        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, toMove.x, Time.deltaTime * velocityChangeSpeed);
        rb.linearVelocityY += toMove.y;
    }
}
