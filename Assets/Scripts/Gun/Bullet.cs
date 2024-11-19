using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private BulletData data;
    public BulletData Data
    {
        get => data;
        set
        {
            data = value;
            if (data != null)
            {
                if (spriteRenderer != null)
                    spriteRenderer.sprite = data.bulletSprite;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot()
    {
        rb.linearVelocity = transform.right * data.bulletSpeed;
        Destroy(gameObject, data.bulletLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (data.HasBulletHit(collision.gameObject.layer))
        {
            // use data.bulletDamage
            Destroy(gameObject);
        }
    }
}
