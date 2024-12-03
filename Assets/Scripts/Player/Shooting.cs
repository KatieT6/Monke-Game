using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private PlayerInput input;
    private InputAction attackAction;

    public GunData currentGun;
    public BulletData currentBullets;

    public GameObject bulletPrefab;
    public Vector2 gunLocalPosition;

    private float delayTime = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region INIT_INPUT
        if (input == null)
        {
            input = GetComponent<PlayerInput>();
            attackAction = input.actions["Attack"];
        }
        #endregion

        #region ATTACK
        delayTime -= Time.deltaTime;
        if (attackAction.triggered && delayTime <= 0.0f)
        {
            // Attack
            List<GameObject> bullets = new();
            List<Vector2> startingPoints = currentGun.GetBulletsStartingPoints();
            List<Vector2> velocityVectors = currentGun.GetBulletsVelocityVectors();
            List<float> zAngles = currentGun.GetBulletsEulerZRotations();
            Vector3 position = transform.position + (Vector3)gunLocalPosition;
            for (int i = 0; i < currentGun.bulletsNum; i++)
            {
                bullets.Add(Instantiate(bulletPrefab, position + (Vector3)startingPoints[i], Quaternion.identity));
                bullets[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, zAngles[i]));
                Bullet b = bullets[i].GetComponent<Bullet>();
                b.Data = currentBullets;
                b.Shoot();
            }
            delayTime = currentGun.timeBetweenShots;
        }
        #endregion
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + new Vector3(gunLocalPosition.x, gunLocalPosition.y, 0.0f), 0.5f);
    }
}
