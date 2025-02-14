using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    [SerializeField] Hittable hittable;
    [SerializeField] Transform playerPos;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] float moveSpeed = 30f;
    [SerializeField] float moveDuration = .5f;

    float ms = 0f;
    bool moving = false;

    void Update()
    {
        if (hittable.isHit())
        {
            //moving = true;
            //rb.linearVelocity = new Vector2(hittable.hitDirection.x, hittable.hitDirection.y) * moveSpeed;
            rb.AddForce(hittable.hitDirection * moveSpeed);
        }
        /*
        if (moving)
            Move();
        */
    }
    /*
    void Move()
    {
        //Wait for the moveDuration to end and stop the block
        while (ms <= moveDuration)
        {
            ms += Time.deltaTime;
            return;
        }
        ms = 0f;
        rb.linearVelocity = new Vector2(0, 0);
        moving = false;
    }
    */
}
