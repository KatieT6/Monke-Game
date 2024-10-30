using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public string groundTag = "Ground";

    public GroundChecker leftChecker;
    public GroundChecker middleChecker;
    public GroundChecker rightChecker;

    public bool IsOnGround => leftChecker.IsOnGround || middleChecker.IsOnGround || rightChecker.IsOnGround;
    public bool IsLeftOnGround => leftChecker.IsOnGround;
    public bool IsMiddleOnGround => middleChecker.IsOnGround;
    public bool IsRightOnGround => rightChecker.IsOnGround;

    public void OnValidate()
    {
        if (leftChecker != null) leftChecker.groundTag = groundTag;
        if (middleChecker != null) middleChecker.groundTag = groundTag;
        if (rightChecker != null) rightChecker.groundTag = groundTag;
    }

    public void Awake()
    {
        if (leftChecker != null) leftChecker.groundTag = groundTag;
        if (middleChecker != null) middleChecker.groundTag = groundTag;
        if (rightChecker != null) rightChecker.groundTag = groundTag;
    }
}
