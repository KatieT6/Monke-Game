using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    [SerializeField] Hittable hittable;

    bool moving = false;

    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] float moveSpeed = 10f;

    private Vector3 nextPosition;

    void Start()
    {
        nextPosition = pointB.position;
    }

    void Update()
    {
        if (hittable.isHit())
            moving = true;

        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

            if (transform.position == nextPosition)
            {
                if (nextPosition == pointA.position)
                    nextPosition = pointB.position;
                else
                    nextPosition = pointA.position;
                moving = false;
            }
        }
    }
}
