using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] PlayerInput input;
    [SerializeField] public Rigidbody2D rb;

    private InputAction moveAction;

    [SerializeField] CinemachineCamera targetCamera;

    [Header("Ground Check Settings: ")]
    [SerializeField] Vector2 boxSize;
    [SerializeField] float castDistance;
    [SerializeField] LayerMask groundLayer;

    [Header("Move Settings: ")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float velocityChangeSpeed = 3f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float gravityScale = 4.5f;
    [SerializeField] float fallMultiplier = 1.5f;

    void Awake()
    {
        moveAction = input.actions["Move"];
    }

    void Update()
    {
        #region DYNAMIC_FOV
        //Zoom out camera when player velocity is high
        Vector2 velocity = rb.linearVelocity;
        float speed = Mathf.Max(Mathf.Abs(velocity.x), Mathf.Abs(velocity.y));
        float targetZoom = 10f + ((speed / maxSpeed) * 2);
        targetCamera.Lens.OrthographicSize = Mathf.Lerp(targetCamera.Lens.OrthographicSize, targetZoom, .05f);
        #endregion

        #region MOVE_HORIZONTAL
        float move = moveAction.ReadValue<Vector2>().x * moveSpeed;
        //Velocity change is slower in air
        if (isGrounded()){
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

        #region JUMP_GRAVITY
        //Increase gravity when falling
        if(rb.linearVelocityY < 0)
        {
            rb.gravityScale = gravityScale * fallMultiplier;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
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

    public void Knockback(int force, Vector2 knockbackDirection)
    {
        knockbackDirection.Normalize();
        Vector2 newVelocity = new Vector2(-knockbackDirection.x, -knockbackDirection.y) * force;

        //If knockback is in the same direction as player velocity, add the velocity
        if(Mathf.Sign(rb.linearVelocityX) != Mathf.Sign(newVelocity.x))
            rb.linearVelocityX = newVelocity.x;
        else
            rb.linearVelocityX += newVelocity.x;
        if (Mathf.Sign(rb.linearVelocityY) != Mathf.Sign(newVelocity.y))
            rb.linearVelocityY = newVelocity.y;
        else
            rb.linearVelocityY += newVelocity.y;

        rb.linearVelocity *= new Vector2(1.1f, 1);

        //If player is grounded give him slightly higher vertical velocity
        if (isGrounded())
            rb.linearVelocity *= new Vector2(1, 1.1f);
    }
}
