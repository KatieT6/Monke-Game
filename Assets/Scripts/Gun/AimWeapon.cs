using UnityEngine;

public class AimWeapon : MonoBehaviour
{

    public Transform Weapon;

    Vector2 direction;

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)Weapon.position;
        FaceMouse();
    }

    void FaceMouse()
    {
        Weapon.transform.right = direction;
    }


}
