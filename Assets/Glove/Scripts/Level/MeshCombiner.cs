using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCombiner : MonoBehaviour
{
    [SerializeField]
    private MeshFilter meshFilter;

    [SerializeField]
    private Transform meshesContainer;

    public void Combine()
    {
        var meshFilters = meshesContainer.GetComponentsInChildren<MeshFilter>();
        var combine = meshFilters.Select(f => new CombineInstance
        {
            mesh = f.mesh,
            transform = f.transform.localToWorldMatrix
        }).ToArray();

        var mesh = new Mesh();
        mesh.CombineMeshes(combine);

        meshFilter.mesh = mesh;
    }
}