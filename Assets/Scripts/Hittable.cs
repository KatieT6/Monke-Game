using UnityEngine;

public class Hittable : MonoBehaviour
{
    bool hit = false;
    public Vector2 hitDirection;

    public void GetHit(Vector2 direction)
    {
        hit = true;
        hitDirection = direction;
    }

    public bool isHit()
    {
        if (hit)
        {
            hit = false;
            return true;
        }
        return false;
    }
}
