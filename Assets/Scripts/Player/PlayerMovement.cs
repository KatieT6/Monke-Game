using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerInput input;
    private Rigidbody2D rb;

    private InputAction moveAction;
    private InputAction jumpAction;

    [SerializeField] AimWeapon aim;

    [Header("Jump Settings: ")]
   // public float jumpHeight = 10f;
    //public float jumpStopSpeed = 3f;
    private bool jumped = false;
    public float jumpForce = 10f;
    //private float jumpTime = 0f;
    //private float jumpVelocity = 0f;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    private float jumpBufferTime = .15f;
    private float jumpBufferCounter;

    private float coyoteTime = 0.15f;
    private float coyoteTimeCounter;


    [Header("Move Settings: ")]
    public float moveSpeed = 10f;
    public float velocityChangeSpeed = 3f;
    public float maxSpeed = 20f;

    private void Awake()
    {
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
            jumpAction = input.actions["Jump"];
        }
        #endregion

        #region GET_JUMP_BUFFER
        if (jumpAction.triggered)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        #endregion

        #region GET_COYOTE_TIME
        if (isGrounded() && rb.linearVelocityY == 0)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        #endregion

        #region JUMP
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            // Jump
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumped = true;
        }

        if (jumped && rb.linearVelocityY <= 0f)
        {
            jumped = false;
        }
        #endregion

        #region MOVE_HORIZONTAL
        float move = moveAction.ReadValue<Vector2>().x * moveSpeed;
        if (rb.linearVelocityX < move){
            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, move, Time.deltaTime * velocityChangeSpeed);
        }
        else
        {
            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, move, Time.deltaTime * velocityChangeSpeed * 0.5f);
        }
        #endregion

        #region CLAMP_SPEED
        rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, -maxSpeed, maxSpeed);
        rb.linearVelocityY = Mathf.Clamp(rb.linearVelocityY, -maxSpeed, maxSpeed);

        #endregion
    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        //Draw ground ray
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    public void Knockback(int force)
    {
        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector2)((worldMousePos - (Vector2)transform.position));
        direction.Normalize();

        Vector2 newVelocity = new Vector2(-direction.x, -direction.y) * force;
        Vector2 currentVelocity = rb.linearVelocity;
        //currentVelocity.x *= Mathf.Min(1.0f - Mathf.Abs(direction.x), 1.0f);
        //currentVelocity.y *= Mathf.Min(1.0f - Mathf.Abs(direction.y), 1.0f);
        currentVelocity.y *= 0.2f;
        currentVelocity.x *= 0.7f;
        rb.linearVelocity = currentVelocity + newVelocity;
    }
}
