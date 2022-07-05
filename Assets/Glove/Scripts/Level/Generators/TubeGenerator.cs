using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TubeGenerator : MonoBehaviour
{

    [SerializeField] private GameObject segmentTemlate;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float height = 20f;
    [SerializeField] private int segmentRCount = 36;
    [SerializeField] private int segmentHCount = 20;
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
        var mh = height / segmentHCount;
        for (var h = height / -2f; h < height / 2f; h += mh)
        {
            var mr = 360f / segmentRCount;
            for (var ix = 0f; ix < 360f; ix += mr)
            {
                var segment = Instantiate(segmentTemlate, transform);

                segment.transform.rotation = Quaternion.Euler(ix, 0, 0);
                segment.transform.position = segment.transform.right * h + segment.transform.forward * radius;

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
