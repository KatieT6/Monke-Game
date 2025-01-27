using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] PlayerMovement player;

    private float attackCooldown;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int knockbackForce;
    [SerializeField] private int punchForce;
    [SerializeField] private float punchDuration;

    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask hitLayers;

    void Start()
    {
        
    }

    void Update()
    {

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
        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(worldMousePos.x - transform.position.x, worldMousePos.y - transform.position.y);
        checkHit(direction);
        /*if (!checkHit(direction))
        {
            player.Knockback(punchForce, -direction);
            float counter = 0f;
            while(counter < punchDuration)
            {
                counter += Time.deltaTime;
                if (checkHit(direction))
                {
                    break;   
                }
            }
        }
        */
    }

    bool checkHit(Vector2 direction)
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(attackPos.position, direction, attackRange, hitLayers);
        if (raycastHit2D.collider != null)
        {
            player.Knockback(knockbackForce, direction);
            return true;
        }
        else
        {
            return false;
        }
    }
}
