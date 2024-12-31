using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    public GameObject Bullet;

    public float bulletSpeed;

    public Transform ShootPoint;

    public float fireRate;

    float shotCountdown;

    public bool isAutomatic = true;

    void Update()
    {
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
        GameObject BulletInstance = Instantiate(Bullet, ShootPoint.position, ShootPoint.rotation);
        BulletInstance.GetComponent<Rigidbody2D>().AddForce(BulletInstance.transform.right * bulletSpeed);
    }
}
