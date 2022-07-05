using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SphereGenerator : MonoBehaviour
{
    [SerializeField] private GameObject segmentTemlate;
    [SerializeField] private int segmentCount = 36;
    [SerializeField] private float radius = 5f;
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
        var mn = 360f / segmentCount;
        for (var y = -90f; y < 91f; y += mn)
        {
            var k = Mathf.Cos(y * Mathf.Deg2Rad);
            var count = (int) Mathf.Lerp(1, segmentCount, k);
            var mk = 360f / count;

            for (var ix = 0; ix < count; ix++)
            {
                var x = mk * ix;

                var segment = Instantiate(segmentTemlate, transform);

                segment.transform.rotation = Quaternion.Euler(y, x, 0);
                segment.transform.position += segment.transform.forward * radius;
                
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
}
