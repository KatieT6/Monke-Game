using UnityEngine;

public class DestructiblePlatform : MonoBehaviour
{
    [SerializeField]
    Hittable hittable;

    void Update()
    {
        if (hittable.isHit())
            Destroy(gameObject);
    }
}
