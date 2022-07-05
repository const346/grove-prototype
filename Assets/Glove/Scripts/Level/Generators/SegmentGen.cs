using UnityEngine;

[System.Serializable]
public class SegmentGen
{
    public GameObject Template;
    public Vector3 PositionOffset;
    public Vector3 RotationOffset;

    [Range(0f, 1f)]
    public float Rand;
}