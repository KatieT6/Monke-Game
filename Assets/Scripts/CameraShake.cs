using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
  
    //[SerializeField] private float globalShakeForce = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public void ShakeCamera(CinemachineImpulseSource impulseSource, float shakeForce, Vector2 dir)
    {
        impulseSource.GenerateImpulseWithVelocity(dir*shakeForce);
    }
}
