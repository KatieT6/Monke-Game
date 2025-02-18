using UnityEngine;
using System.Collections;

public class DestructiblePlatform : MonoBehaviour
{
    [SerializeField] Hittable hittable;
    [SerializeField] Collider2D pCollider;
    [SerializeField] Animator anim;

    [SerializeField] float animTime, respawnTime;

    void Update()
    {
        if (hittable.isHit())
            StartCoroutine("DestroyPlatform");
    }

    IEnumerator DestroyPlatform()
    {
        anim.Play("PlatformDestroy");
        yield return new WaitForSeconds(animTime);
        pCollider.enabled = false;
        yield return new WaitForSeconds(respawnTime);
        anim.Play("PlatformRespawn");
        pCollider.enabled = true;

    }
}
