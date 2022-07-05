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

    private GravityTarget gravityTarget;
    private Rigidbody body;

    private float look;
    private bool move; 
    private bool jump;
    private bool attack;

    private float walk;

    private void Awake()
    {
        gravityTarget = GetComponent<GravityTarget>();
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var up = gravityTarget.GetUp();

        view.transform.rotation = gravityTarget.GetRotation() * Quaternion.AngleAxis(look, Vector3.up);
        view.transform.position = transform.position + up * offset;

        walk = Mathf.Lerp(walk, move ? 1f : 0f, 0.2f);
        animator.SetFloat("walk", walk);

    }

    private void FixedUpdate()
    {
        if (move)
        {
            body.AddTorque(view.transform.right * 400);
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

    public void Move(float angle, Vector3 forward)
    {
        var up = gravityTarget.GetUp();
        var right = Vector3.Cross(up, forward);

        gravityTarget.SetAxis(right);

        look = Mathf.LerpAngle(look, angle, 0.1f);
        move = true;
    }

    public void Move(float angle)
    {
        //gravityTarget.SetAxis(axis);

        look = Mathf.LerpAngle(look, angle, 0.1f);
        move = true;
    }


    public void Stand()
    {
        move = false;
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
