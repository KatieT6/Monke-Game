using UnityEngine;

public class MeleeAlt : MonoBehaviour
{
    [SerializeField] PlayerMovement player;

    private float attackCooldown;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int knockbackForce;
    [SerializeField] private int punchForce;
    [SerializeField] private float punchDuration;

    private Vector3 attackPos;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask hitLayers;

    private bool isPunching = false;
    private float counter = 0f;

    Vector2 input = new Vector2(0, 0);

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (isPunching)
        {
            if(counter < punchDuration)
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


        if(input != Vector2.zero)
        {
            if (Time.time > attackCooldown)
            {
                attackCooldown = Time.time + 1 / attackSpeed;
                Attack();
            }
        }
    }

    void Attack()
    {
        if (!checkHit())
        {
            isPunching = true;
            //player.Knockback(punchForce, -direction, .5f);
        }
    }

    bool checkHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, input, attackRange, hitLayers);
        if (hit)
        {
            isPunching = false;
            Hittable hittable;
            if(hittable = hit.collider.GetComponent<Hittable>())
            {
                hittable.GetHit(input);
            }
            player.Knockback(knockbackForce, input);
            return true;
        }
        else
        {
            
            return false;
        }
    }
}
