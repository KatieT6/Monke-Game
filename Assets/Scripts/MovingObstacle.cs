using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] float moveSpeed = 2f;

    private Vector3 nextPosition;

    void Start()
    {
        nextPosition = pointB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

        if(transform.position == nextPosition)
        {
            if(nextPosition == pointA.position)
                nextPosition = pointB.position;
            else
                nextPosition = pointA.position;
        }
    }
}
