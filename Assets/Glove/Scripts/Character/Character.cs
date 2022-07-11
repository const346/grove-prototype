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

    public Quaternion LookRotation => moveRotation * localLookRotation;
    public GravityTarget GravityTarget => gravityTarget;

    private Rigidbody body;
    private GravityTarget gravityTarget;
    private Vector3 localMoveDirection;
    private Quaternion localLookRotation;
    private Quaternion moveRotation;
    private float walk;
    private bool jump;
    private bool attack;

    private void Awake()
    {
        gravityTarget = GetComponent<GravityTarget>();
        body = GetComponent<Rigidbody>();

        localLookRotation = Quaternion.identity;
        moveRotation = Quaternion.identity;
    }

    private void Update()
    {
        view.transform.rotation = moveRotation;
        view.transform.position = transform.position + gravityTarget.GetUp() * offset;
        
        walk = Mathf.Lerp(walk, localMoveDirection != Vector3.zero ? 1f : 0f, 0.2f);
        animator.SetFloat("walk", walk);



        var up = GravityTarget.GetUp();

        var lookV = moveRotation * localLookRotation * Vector3.forward;
        var forward = Vector3.ProjectOnPlane(lookV, up);
        Debug.DrawRay(transform.position, forward);

        moveRotation = Quaternion.LookRotation(forward, up);

        localLookRotation = Quaternion.identity;
    }

    private void FixedUpdate()
    {
        if (localMoveDirection != Vector3.zero)
        {
            body.AddTorque(moveRotation * Vector3.right * 400);
        }
        else
        {
            body.angularVelocity = Vector3.zero;
        }

        if (jump)
        {
            jump = false;

            var up = gravityTarget.GetUp();
            body.AddForce(up.normalized * 300);
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
        localLookRotation *= Quaternion.AngleAxis(deltaX, Vector3.right) * Quaternion.AngleAxis(deltaY, Vector3.up);
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
