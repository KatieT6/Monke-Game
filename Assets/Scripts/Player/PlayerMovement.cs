using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerInput input;
    private Rigidbody2D rb;

    private InputAction moveAction;

    [SerializeField] CinemachineCamera targetCamera;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    [Header("Move Settings: ")]
    public float moveSpeed = 10f;
    public float velocityChangeSpeed = 3f;
    public float maxSpeed = 20f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        #region DYNAMIC_FOV
        Vector2 velocity = rb.linearVelocity;
        float speed = Mathf.Max(Mathf.Abs(velocity.x), Mathf.Abs(velocity.y));
        float targetZoom = 10f + ((speed / maxSpeed) * 2);
        targetCamera.Lens.OrthographicSize = Mathf.Lerp(targetCamera.Lens.OrthographicSize, targetZoom, .05f);
        #endregion

        #region INIT_INPUT
        if (input == null)
        {
            input = GetComponent<PlayerInput>();
            moveAction = input.actions["Move"];
        }
        #endregion

        #region MOVE_HORIZONTAL
        float move = moveAction.ReadValue<Vector2>().x * moveSpeed;
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
        rb.linearVelocity = newVelocity;
    }
}
