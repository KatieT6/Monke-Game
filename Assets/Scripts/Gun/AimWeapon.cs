using UnityEngine;

public class AimWeapon : MonoBehaviour
{

    public Transform Weapon;

    float lookAngle;
    Vector2 lookDirection;

    [HideInInspector] public Vector2 direction;

    void Update()
    {
        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        lookDirection = new Vector2(worldMousePos.x - Weapon.position.x, worldMousePos.y - Weapon.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        FaceMouse();
    }

    void FaceMouse()
    {
        Weapon.rotation = Quaternion.Euler(0, 0, lookAngle);
    }


}
