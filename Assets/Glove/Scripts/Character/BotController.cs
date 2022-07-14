using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class BotController : MonoBehaviour
{
    private Character character;
    
    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Decision());
    }

    private IEnumerator Decision()
    {
        while (enabled)
        {
            var delay = Random.Range(0, 3);
            yield return new WaitForSeconds(delay);

            character.Move(Vector3.zero);

            if (Random.Range(0, 2) == 0)
            {
                var angle = Random.Range(0, 360);
                yield return Move(angle);
            }
        }
    }

    private IEnumerator Move(float angle)
    {
        var count = Random.Range(0, 3100);

        character.Look(0, angle);

        for (var i = 0; i < count; i++)
        {
            if (character.IsBarrier)
            {
                yield break;
            }

            character.Move(Vector3.forward);
            yield return new WaitForFixedUpdate();
        }
    }
}
