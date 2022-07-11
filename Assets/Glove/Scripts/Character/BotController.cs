using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    //private GravityTarget gravityObject;
    //private Character controller;
    //private bool actionBreak;

    //private void Awake()
    //{
    //    controller = GetComponent<Character>();
    //    gravityObject = GetComponent<GravityTarget>();
    //}

    //private void OnEnable()
    //{
    //    StopAllCoroutines();
    //    StartCoroutine(Decision());
    //}

    //private IEnumerator Decision()
    //{
    //    while (enabled)
    //    {
    //        var delay = Random.Range(0, 3);
    //        yield return new WaitForSeconds(delay);

    //        controller.Stand();

    //        if (Random.Range(0, 2) == 0)
    //        {
    //            var angle = Random.Range(0, 360);
    //            yield return Move(angle);
    //        }
    //    }
    //}

    //private IEnumerator Move(float angle)
    //{
    //    var count = Random.Range(0, 3100);
    //    for (var i = 0; i < count; i++)
    //    {
    //        if (actionBreak)
    //        {
    //            actionBreak = false;
    //            yield break;
    //        }

    //        controller.Move(angle);
    //        yield return new WaitForFixedUpdate();
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (Vector3.Angle(collision.contacts[0].normal, gravityObject.GetUp()) > 45)
    //    {
    //        actionBreak = true;
    //    }
    //}
}
