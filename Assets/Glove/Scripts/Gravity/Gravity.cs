using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gravity : MonoBehaviour
{
    public enum GravityType 
    {
        Sphere,
        Tube,
        Default
    }

    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private GravityType gravityType;
    [SerializeField] private float outside = 0;

    public UnityEvent<GravityTarget> OnOuside;

    private void Start()
    {
        Physics.gravity = Vector3.zero;
    }

    public float GetDistance(Vector3 position)
    {
        var k = position - transform.position;
        if (gravityType == GravityType.Sphere)
        {
            return k.magnitude;
        }
        else if (gravityType == GravityType.Tube)
        {
            var m = Vector3.ProjectOnPlane(k, transform.right);
            return m.magnitude;
        }

        return 0;
    }

    public Vector3 GetGravity(Vector3 position)
    {
        var k = position - transform.position;
        if (gravityType == GravityType.Sphere)
        {
            return k.normalized * gravity;
        }
        else if (gravityType == GravityType.Tube)
        {
            var m = Vector3.ProjectOnPlane(k, transform.right);
            return m.normalized * gravity;
        }

        return transform.up * gravity;
    }

    public Vector3 GetUp(Vector3 position)
    {
        return GetGravity(position).normalized * -1f;
    }

    public Vector3 GetForward(Vector3 position, Vector3 axis)
    {
        var up = GetUp(position);
        var forward = Quaternion.AngleAxis(90, axis) * up;

        return forward;
    }

    public bool CheckOutside(GravityTarget gravityTarget)
    {
        var gk = Mathf.Sign(gravity) * -1;
        if (GetDistance(gravityTarget.transform.position) * gk < outside * gk)
        {
            OnOuside?.Invoke(gravityTarget);
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (gravityType == GravityType.Sphere)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 1);
        }
        else if (gravityType == GravityType.Tube)
        {
            Gizmos.color = Color.green;
            var k = transform.right * 10000;

            Gizmos.DrawLine(transform.position - k, transform.position + k);
        }
    }
}
