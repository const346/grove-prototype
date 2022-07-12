using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TorGenerator : MonoBehaviour
{
    [SerializeField] private GameObject segmentTemlate;
    [SerializeField] private float radiusT = 5f;
    [SerializeField] private float radiusM = 20f;
    [SerializeField] private int segmentTCount = 36;
    [SerializeField] private int segmentMCount = 20;
    [SerializeField] private SegmentGen[] generators;
    [SerializeField] private bool generateOnStart = true;

    public UnityEvent OnComplete;

    private void Start()
    {
        if (generateOnStart)
        {
            Generate();
        }
    }

    private void Generate()
    {
        var center = transform.position;
        var deltaM = 360f / segmentTCount;
        var deltaT = 360f / segmentMCount;

        for (var it = 0f; it < 360f; it += deltaT)
        {
            for (var im = 0f; im < 360f; im += deltaM)
            {
                var cp2 = center + Quaternion.AngleAxis(im, Vector3.up) * Vector3.forward * radiusM;

                var rt = Quaternion.AngleAxis(im + 90, Vector3.up);

                var pt2 = cp2 + rt * Quaternion.AngleAxis(it, Vector3.forward) * Vector3.up * radiusT;

                var segment = Instantiate(segmentTemlate, transform);

                segment.transform.rotation = Quaternion.AngleAxis(im + 90, Vector3.up) * Quaternion.AngleAxis(it, Vector3.forward) * Quaternion.AngleAxis(90, Vector3.right);
                segment.transform.position = pt2;

                foreach (var gen in generators)
                {
                    if (TryGenerate(gen, segment.transform, transform))
                    {
                        break;
                    }
                }
            }
        }

        OnComplete?.Invoke();
    }

    private bool TryGenerate(SegmentGen gen, Transform segment, Transform container)
    {
        if (Random.Range(0f, 1f) < gen.Rand)
        {
            var item = Instantiate(gen.Template, container);
            item.transform.position = segment.transform.position + gen.PositionOffset;
            item.transform.rotation = segment.transform.rotation * Quaternion.Euler(gen.RotationOffset);

            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        var center = transform.position;
        var deltaM = 360f / segmentTCount; 
        var deltaT = 360f / segmentMCount;

        for (var it = 0f; it < 360f; it += deltaT)
        {
            for (var im = 0f; im < 360f; im += deltaM)
            {
                var cp1 = center + Quaternion.AngleAxis(im - deltaM, Vector3.up) * Vector3.forward * radiusM;
                var cp2 = center + Quaternion.AngleAxis(im, Vector3.up) * Vector3.forward * radiusM;
                var ncp = (cp2 - cp1).normalized;

                var pt1 = cp2 + Quaternion.AngleAxis(im + 90, Vector3.up) * Quaternion.AngleAxis(it - deltaT, Vector3.forward) * Vector3.up * radiusT;
                var pt2 = cp2 + Quaternion.AngleAxis(im + 90, Vector3.up) * Quaternion.AngleAxis(it, Vector3.forward) * Vector3.up * radiusT;

                Gizmos.color = Color.red;
                Gizmos.DrawLine(cp1, cp2);

                Gizmos.color = Color.blue;
                Gizmos.DrawLine(pt1, pt2);

                Gizmos.color = Color.green;
                Gizmos.DrawLine(cp2, center); 
                
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(cp2, cp2 + ncp);
            }
        }
    }
}
