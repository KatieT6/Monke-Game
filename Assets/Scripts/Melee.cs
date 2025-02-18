using UnityEngine;
using Unity.Cinemachine;

public class Melee : MonoBehaviour
{
    [SerializeField] PlayerMovement player;

    [SerializeField] CinemachineImpulseSource impulseSource;
    [SerializeField] private float shakeForce = .5f;

    
    private float attackCooldown;
    [Header("Attack Settings: ")]
    [SerializeField] private float attackSpeed;
    [SerializeField] private int knockbackForce;
    [SerializeField] private int punchForce;
    [SerializeField] private float punchDuration;

    private Vector3 attackPos;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask hitLayers;

    private bool isPunching = false;
    private Vector2 direction;
    private Vector2 worldMousePos;
    private float counter = 0f;

    void Update()
    {
        #region ATTACK_BUFFER
        if (isPunching)
        {
            if (counter < punchDuration)
            {
                counter += Time.deltaTime;
                checkHit();
            }
            else
            {
                isPunching = false;
                counter = 0f;
            }
        }
        #endregion

        #region ATTACK_INPUT
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time > attackCooldown)
            {
                attackCooldown = Time.time + 1 / attackSpeed;
                Attack();
            }
        }
        #endregion
    }

    void Attack()
    {
        worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(worldMousePos.x - transform.position.x, worldMousePos.y - transform.position.y);
        direction.Normalize();
        if (!checkHit())
        {
            isPunching = true;
            //player.Knockback(punchForce, -direction, .5f);
        }
    }

    bool checkHit()
    {
        float angle = Vector2.Angle(new Vector2(1,0), direction);
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1, 0.7f), 0, direction, attackRange - .5f, hitLayers);
        if (hit)
        {
            ShakeCamera(direction);
            isPunching = false;
            Hittable hittable;
            player.Knockback(knockbackForce, direction);
            if (hittable = hittable = hit.collider.GetComponent<Hittable>())
                hittable.GetHit(direction);  
        }
        return hit;

    }

    void ShakeCamera(Vector2 dir)
    {
        CameraShake.instance.ShakeCamera(impulseSource, shakeForce, -dir);
    }
}
