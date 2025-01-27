using UnityEngine;
using Unity.Cinemachine;

public class WeaponScript : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;

    //Force of camera shake when firing weapon
    [SerializeField] private float shakeForce = .5f;

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
        impulseSource = GetComponent<CinemachineImpulseSource>();

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
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = new Vector2(worldMousePos.x - transform.position.x, worldMousePos.y - transform.position.y);
            //Shake camera
            CameraShake.instance.ShakeCamera(impulseSource, shakeForce, direction);

            currentAmmo--;
            GameObject BulletInstance = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
            BulletInstance.GetComponent<Rigidbody2D>().AddForce(BulletInstance.transform.right * bulletSpeed);

            //player.Knockback(knockbackForce);
        }
    }
}
