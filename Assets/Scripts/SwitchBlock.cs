using UnityEngine;

public class SwitchBlock : MonoBehaviour
{
    [SerializeField] Hittable hittable;
    [SerializeField] Collider2D bCollider;
    static bool globalSwitch = false;
    [SerializeField] bool switched = false;


    [SerializeField] float animTime, respawnTime;

    void Update()
    {
        if (hittable.isHit())
            SwitchPlatform();
    }

    void SwitchPlatform()
    {
        if (globalSwitch)
            bCollider.enabled = false;
        else
            bCollider.enabled = true;
        globalSwitch = !globalSwitch;
    }
}
