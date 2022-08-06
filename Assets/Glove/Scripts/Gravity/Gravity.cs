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
        Tor,
        Default
    }

    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private GravityType gravityType;

    [Header("Specific")]
    [SerializeField] private float torRadius = 8;

    [Header("Detect Ouside")]
    [SerializeField] private bool checkOutside = true;
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
        else if (gravityType == GravityType.Tor)
        {
            var n = Vector3.ProjectOnPlane(k, transform.up);
            var m = transform.position + (n - transform.position).normalized * torRadius;
            
            return Vector3.Distance(position, m);
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
        else if (gravityType == GravityType.Tor)
        {
            var n = Vector3.ProjectOnPlane(k, transform.up);
            var m = transform.position + (n - transform.position).normalized * torRadius;
            var l = position - m;
            return l.normalized * gravity;
        }

        return transform.up * gravity;
    }

    public Vector3 GetUp(Vector3 position)
    {
        return GetGravity(position).normalized * -1f;
    }

    public bool CheckOutside(GravityTarget gravityTarget)
    {
        if (checkOutside)
        {
            var gk = Mathf.Sign(gravity) * -1;
            if (GetDistance(gravityTarget.transform.position) * gk < outside * gk)
            {
                OnOuside?.Invoke(gravityTarget);
                return true;
            }
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
