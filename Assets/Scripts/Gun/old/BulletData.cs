using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Scriptable Objects/BulletData")]
public class BulletData : ScriptableObject
{
    public Sprite bulletSprite;
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletLifeTime;
    public LayerMask bulletHitMask;

    public bool HasBulletHit(int layerIndex)
    {
        return (bulletHitMask.value & layerIndex) != 0;
    }

    public bool HasBulletHit(string layerName)
    {
        return HasBulletHit(LayerMask.NameToLayer(layerName));
    }
}
