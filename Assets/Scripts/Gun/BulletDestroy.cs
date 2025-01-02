using UnityEngine;

public class BulletDestroy : MonoBehaviour
{

    public void OnAnimationFinished()
    {
        Destroy(gameObject);
    }
}
