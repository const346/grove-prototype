using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float height = 1.5f;

    private Camera currentCamera;
    private Character character;
    private float distance = 5f;

    private void Awake()
    {
        currentCamera = Camera.main;
        character = GetComponent<Character>();
    }

    private void Update()
    {
        InputProcessing();
    }

    private void LateUpdate()
    {
        currentCamera.transform.rotation = character.LookRotation;

        currentCamera.transform.position = GetCameraPivot() + 
            character.LookRotation * Vector3.forward * -1f * distance;
    }

    private void InputProcessing()
    {
        if (Input.GetMouseButtonDown(0))
        {
            character.Attack();
        }

        if (Input.GetMouseButton(1))
        {
            var deltaY = Input.GetAxis("Mouse X") * 80f * Time.deltaTime;
            var deltaX = Input.GetAxis("Mouse Y") * 80f * Time.deltaTime;

            character.Look(deltaX, deltaY);
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

        character.Move(move);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.Jump();
        }
    }

    private Vector3 GetCameraPivot()
    {
        var up = character.GravityTarget.GetUp();
        return transform.position + up * height;
    }
}
