using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SegmentView : MonoBehaviour
{
    [SerializeField]
    private Material collisionMaterial;

    private Material defaultMaterial;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = meshRenderer.material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        meshRenderer.material = collisionMaterial;
    }

    private void OnCollisionExit(Collision collision)
    {
        meshRenderer.material = defaultMaterial;
    }
}
