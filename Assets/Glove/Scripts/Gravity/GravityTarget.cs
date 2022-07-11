using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityTarget : MonoBehaviour
{
    private Rigidbody body;
    private Gravity gravity;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        gravity = FindObjectOfType<Gravity>();
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

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, up);
        }
    }

    public Vector3 GetGravity()
    {
        return gravity.GetGravity(transform.position);
    }

    public Vector3 GetUp()
    {
        return gravity.GetUp(transform.position);
    }

    public void ResetVelocity()
    {
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
    }
}