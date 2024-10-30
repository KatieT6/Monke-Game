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
    public float jumpStopSpeed = 3f;
    private bool jumped = false;
    private float jumpTime = 0f;
    private float jumpVelocity = 0f;

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
        #region INIT_INPUT
        if (input == null)
        {
            input = GetComponent<PlayerInput>();
            moveAction = input.actions["Move"];
            attackAction = input.actions["Attack"];
            jumpAction = input.actions["Jump"];
        }
        #endregion

        #region ATTACK
        if (attackAction.triggered)
        {
            // Attack
        }
        #endregion

        #region JUMP
        if (jumpAction.triggered && IsOnGround)
        {
            // Jump
            jumpTime = Mathf.Sqrt((-2f * jumpHeight) / Physics2D.gravity.y);
            jumpVelocity = -Physics2D.gravity.y * jumpTime;
            rb.linearVelocityY = jumpVelocity;
            jumped = true;
        }
        else if (!jumpAction.IsPressed() && !IsOnGround && rb.linearVelocityY > 0f && jumped)
        {
            rb.linearVelocityY = Mathf.Lerp(rb.linearVelocityY, 0f, Time.deltaTime * jumpStopSpeed);
        }

        if (jumped && rb.linearVelocityY <= 0f)
        {
            jumped = false;
        }
        #endregion

        #region MOVE_HORIZONTAL
        float move = moveAction.ReadValue<Vector2>().x * moveSpeed;
        rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, move, Time.deltaTime * velocityChangeSpeed);
        #endregion
    }
}
