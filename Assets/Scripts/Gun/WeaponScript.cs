using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] PlayerMovement player;

    [SerializeField] GameObject Bullet;

    [SerializeField] float bulletSpeed;

    [SerializeField] Transform ShootPoint;

    [SerializeField] float fireRate;

    float shotCountdown;

    [SerializeField] bool isAutomatic = true;

    [SerializeField] int knockbackForce;

    [SerializeField] int maxAmmo = 2;
    private int currentAmmo;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (currentAmmo < maxAmmo)
        {
            if (player.isGrounded())
            {
                currentAmmo = maxAmmo;
            }
        }


        if (isAutomatic)
        {
            if (Input.GetMouseButton(0))
            {
                if (Time.time > shotCountdown)
                {
                    shotCountdown = Time.time + 1 / fireRate;
                    Fire();
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time > shotCountdown)
                {
                    shotCountdown = Time.time + 1 / fireRate;
                    Fire();
                }
            }
        }

    }

    void Fire()
    {
        if (currentAmmo >= 1)
        {
            currentAmmo--;
            GameObject BulletInstance = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
            BulletInstance.GetComponent<Rigidbody2D>().AddForce(BulletInstance.transform.right * bulletSpeed);

            player.Knockback(knockbackForce);
        }
    }
}
