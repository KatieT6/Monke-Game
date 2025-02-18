using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] Hittable hittable;
    [SerializeField] Rigidbody2D rb;
    Vector2 startPos;

    [SerializeField] float dropDelay, respawnTime;

    void Start()
    {
        startPos = transform.position;
    }


    void Update()
    {
        if (hittable.isHit())
            StartCoroutine("Drop");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
            StartCoroutine("Drop");
    }

    IEnumerator Drop()
    {
        
        yield return new WaitForSeconds(dropDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = .1f;
        yield return new WaitForSeconds(respawnTime);
        Respawn();
    }

    void Respawn()
    {
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = startPos;
    }
}
