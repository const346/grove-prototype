using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GravityTarget))]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float height = 1.5f;

    private float distance = 3f;
    private float x;
    private float y;

    private GravityTarget gravityObject;
    private Character controller;
    private Camera cam;

    private void Awake()
    {
        controller = GetComponent<Character>();
        gravityObject = GetComponent<GravityTarget>();

        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            controller.Attack();
        }

        if (Input.GetMouseButton(1))
        {
            var xx = Input.GetAxis("Mouse X") * 80f * Time.deltaTime;
            var yy = Input.GetAxis("Mouse Y") * 80f * Time.deltaTime;

            y += xx;
            x += yy;
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            distance *= Input.mouseScrollDelta.y > 0 ? 0.95f : 1.05f;
        }

        var move = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            move += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += Vector3.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move += Vector3.left;
        }

        if (move != Vector3.zero)
        {
            var up = gravityObject.GetUp();
            var fm = transform.position - cam.transform.position;
            var forward = Vector3.ProjectOnPlane(fm, up).normalized;
            var angle = Vector3.SignedAngle(Vector3.forward, move, Vector3.up);
            forward = Quaternion.AngleAxis(angle, up) * forward;

            Debug.DrawRay(transform.position, forward * 3);
            
            controller.Move(angle + y);
        }
        else
        {
            controller.Stand();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.Jump();
        }
    }

    private void LateUpdate()
    {
        var up = gravityObject.GetUp();

        cam.transform.rotation = gravityObject.GetRotation() * Quaternion.AngleAxis(y, Vector3.up) * Quaternion.AngleAxis(x, Vector3.right);
        cam.transform.position = transform.position + up * height + cam.transform.rotation * Vector3.forward * -1f * distance;
    }

    private void OnDrawGizmos()
    {
        if (gravityObject && cam)
        {
            var up = gravityObject.GetUp();

            var fm = transform.position - cam.transform.position;
            var forward = Vector3.ProjectOnPlane(fm, up).normalized * 2f;

            Gizmos.color = Color.red + Color.blue;
            Gizmos.DrawRay(transform.position, forward);
        }
    }
}
