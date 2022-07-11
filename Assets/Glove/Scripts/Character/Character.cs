using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GravityTarget))]
[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField]
    private Transform view;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float offset;

    public Quaternion LookRotation => forwardRotation *
        Quaternion.AngleAxis(lookX, Vector3.right) *
        Quaternion.AngleAxis(lookY, Vector3.up);

    public GravityTarget GravityTarget => gravityTarget;

    private Rigidbody body;
    private GravityTarget gravityTarget;
    private Vector3 localMoveDirection;
    private Quaternion forwardRotation;

    private Quaternion viewRotation;
    private float walk;
    private bool jump;
    private bool attack;
    private float lookX, lookY;

    private void Awake()
    {
        gravityTarget = GetComponent<GravityTarget>();
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        walk = Mathf.Lerp(walk, localMoveDirection != Vector3.zero ? 1f : 0f, 0.2f);
        animator.SetFloat("walk", walk);

        var localLookRotation = Quaternion.AngleAxis(lookY, Vector3.up);

        var up = GravityTarget.GetUp();
        var lookV = forwardRotation * localLookRotation * Vector3.forward;
        var forward = Vector3.ProjectOnPlane(lookV, up).normalized;

        Debug.DrawRay(transform.position, forward);
        

        forwardRotation = Quaternion.LookRotation(forward, up);
        lookY = 0;

        if (localMoveDirection != Vector3.zero)
        {
            var angle = Vector3.SignedAngle(Vector3.forward, localMoveDirection, Vector3.up);
            var targetRotation = forwardRotation * Quaternion.AngleAxis(angle, Vector3.up);

            viewRotation = Quaternion.Lerp(viewRotation, targetRotation, 0.1f);
        }

        view.transform.rotation = viewRotation;
        view.transform.position = transform.position + gravityTarget.GetUp() * offset;
    }

    private void FixedUpdate()
    {
        if (localMoveDirection != Vector3.zero)
        {
            var angle = Vector3.SignedAngle(Vector3.forward, localMoveDirection, Vector3.up);

            body.AddTorque(forwardRotation * Quaternion.AngleAxis(angle, Vector3.up) * Vector3.right * 400);
        }
        else
        {
            body.angularVelocity = Vector3.zero;
        }

        if (jump)
        {
            jump = false;

            var upX = gravityTarget.GetUp();
            body.AddForce(upX.normalized * 300);
            animator.SetTrigger("jump");
        }

        if (attack)
        {
            attack = false;
            animator.SetTrigger("attack");
        }
    }

    public void Move(Vector3 localMove)
    {
           localMoveDirection = localMove;
    }

    public void Look(float deltaX, float deltaY)
    {
        lookX += deltaX;
        lookY += deltaY;
    }

    public void Jump()
    {
        jump = true;
    }

    public void Attack()
    {
        attack = true;
    }
}
