using UnityEngine;

public class Melee : MonoBehaviour
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
    private Vector2 direction;
    private Vector2 worldMousePos;
    private float counter = 0f;

    void Update()
    {
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


        if (Input.GetMouseButtonDown(0))
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
        worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(worldMousePos.x - transform.position.x, worldMousePos.y - transform.position.y);
        if (!checkHit())
        {
            isPunching = true;
            //player.Knockback(punchForce, -direction, .5f);
        }
    }

    bool checkHit()
    {
        if(Physics2D.Raycast(transform.position, direction, attackRange, hitLayers))
        {
            player.Knockback(knockbackForce, direction, 1f);
            return true;
        }
        else
        {
            
            return false;
        }
    }
}
