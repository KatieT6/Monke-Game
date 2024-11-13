using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public enum SHOT_TYPE
{
    Paraller,
    Circle
}

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public Sprite gunSprite;
    public GameObject bullet = null;

    [Min(1)]
    public int bulletsNum = 1;
    public float timeBetweenShots = 1f;

    public Vector3 gunHandlePos = Vector3.zero;
    public Vector3 bulletHolePos = Vector3.zero;

    public bool MultiBullets => bulletsNum > 1;
    [ShowIf("MultiBullets")]
    public SHOT_TYPE shotType = SHOT_TYPE.Paraller;

    private bool CircleShotType => shotType == SHOT_TYPE.Circle;
    [ShowIf("CircleShotType")]
    [MinValue(0f)]
    [MaxValue(90f)]
    public float circleAngle = 60f;

    public List<Vector2> GetBulletsVelocityVectors()
    {
        List<Vector2> shotVectors = new();
        if (!MultiBullets || shotType == SHOT_TYPE.Paraller)
        {
            for (int i = 0; i < bulletsNum; ++i)
            {
                shotVectors.Add(Vector2.right);
            }
            return shotVectors;
        }

        if (shotType == SHOT_TYPE.Circle)
        {
            if (bulletsNum % 2 == 0)
            {
                float angleSteps = circleAngle / (bulletsNum - 1);
                for (int i = 0; i < bulletsNum / 2; ++i)
                {
                    float radians = Mathf.Deg2Rad * (angleSteps / 2f + angleSteps * i);
                    shotVectors.Add(new(Mathf.Cos(radians), Mathf.Sin(radians)));
                    shotVectors.Add(new(Mathf.Cos(radians), -Mathf.Sin(radians)));
                }
            }
            else
            {
                float angleSteps = circleAngle / (bulletsNum - 1);
                shotVectors.Add(Vector2.right);
                for (int i = 0; i < (bulletsNum - 1) / 2; ++i)
                {
                    float radians = Mathf.Deg2Rad * (angleSteps + angleSteps * i);
                    shotVectors.Add(new(Mathf.Cos(radians), Mathf.Sin(radians)));
                    shotVectors.Add(new(Mathf.Cos(radians), -Mathf.Sin(radians)));
                }
            }
        }

        return shotVectors;
    }

    public List<Vector2> GetBulletsStartingPoints() 
    {
        List<Vector2> posVectors = new();
        if (!MultiBullets || shotType == SHOT_TYPE.Paraller)
        {
            if (bulletsNum % 2 == 0)
            {
                for (int i = 0; i < bulletsNum / 2; ++i)
                {
                    posVectors.Add(new Vector2(0f, 0.5f + i));
                    posVectors.Add(new Vector2(0f, -0.5f - i));
                }
            }
            else
            {
                posVectors.Add(Vector2.zero);
                for (int i = 0; i < (bulletsNum - 1) / 2; ++i)
                {
                    posVectors.Add(new Vector2(0f, 1f + i));
                    posVectors.Add(new Vector2(0f, -1f - i));
                }
            }
            return posVectors;
        }

        if (shotType == SHOT_TYPE.Circle)
        {
            for (int i = 0; i < bulletsNum; ++i)
            {
                posVectors.Add(Vector2.zero);
            }
        }

        return posVectors;
    }
}
