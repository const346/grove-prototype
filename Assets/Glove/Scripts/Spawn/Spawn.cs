using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;

    public Vector3 GetPosition()
    {
        return transform.TransformPoint(positionOffset);
    }

    public Quaternion GetRotation()
    {
        var rotation = Quaternion.Euler(rotationOffset);
        return transform.rotation * rotation;
    }

    private void OnDrawGizmosSelected()
    {
        var p = GetPosition();
        var r = GetRotation();

        Gizmos.color = Color.green;
        Gizmos.DrawCube(p, Vector3.zero * 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(p, p + r * Vector3.up);
    }
}
