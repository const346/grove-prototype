using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityTarget : MonoBehaviour
{
    private Rigidbody body;
    private Gravity gravity;
    private Vector3 axis;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        gravity = FindObjectOfType<Gravity>();
        axis = Vector3.right;
    }

    private void FixedUpdate()
    {
        if (!gravity.CheckOutside(this))
        {
            var gForce = gravity.GetGravity(transform.position);
            body.AddForce(gForce);
        }
    }

    private void OnDrawGizmos()
    {
        if (gravity)
        {
            var up = GetUp();
            var forward = GetForward();
            var right = GetRight();

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, up);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, forward);
            
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, right);
        }
    }

    public void SetAxis(Vector3 axis)
    {
        this.axis = axis;
    }

    public Vector3 GetGravity()
    {
        return gravity.GetGravity(transform.position);
    }

    public Vector3 GetUp()
    {
        return gravity.GetUp(transform.position);
    }

    public Vector3 GetForward()
    {
        return gravity.GetForward(transform.position, axis);
    }

    public Vector3 GetRight()
    {
        var up = GetUp();
        var forward = GetForward();

        return Vector3.Cross(up, forward).normalized;
    }

    public Quaternion GetRotation()
    {
        var up = GetUp();
        var forward = GetForward();

        return Quaternion.LookRotation(forward, up);
    }

    public void ResetVelocity()
    {
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
    }
}