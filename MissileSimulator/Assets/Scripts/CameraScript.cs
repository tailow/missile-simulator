using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject target;

    Vector3 lookTarget;

    void Update()
    {
        lookTarget = Vector3.Lerp(Vector3.zero, target.transform.position, 0.2f);

        transform.LookAt(lookTarget);
    }
}
