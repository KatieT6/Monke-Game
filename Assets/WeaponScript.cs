using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] PlayerMovement player;

    public GameObject Bullet;

    public float bulletSpeed;

    public Transform ShootPoint;

    public float fireRate;

    float shotCountdown;

    public bool isAutomatic = true;

    public int maxAmmo = 2;
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
        }
    }
}
