using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public float speed = 3f;
    public float turnRate = 1f;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        transform.Rotate(new Vector3(0, turnRate * Time.deltaTime, 0));
    }
}
