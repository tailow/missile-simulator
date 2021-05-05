using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    LauncherTV,
    General
}

public class CameraScript : MonoBehaviour
{
    public GameObject target;
    public Transform missileParent;

    Camera thisCamera;

    public CameraType cameraType;

    Vector3 lookTarget;

    void Start()
    {
        thisCamera = GetComponent<Camera>();

        switch (cameraType)
        {
            case CameraType.LauncherTV:
                transform.position = new Vector3(0, 10, 10);
                break;
        }
    }

    void Update()
    {
        if (missileParent.childCount > 0)
        {
            lookTarget = Vector3.Lerp(missileParent.GetChild(0).position, target.transform.position, 0.1f);
        }

        else if (target)
        {
            lookTarget = Vector3.Lerp(Vector3.zero, target.transform.position, 0.1f);
        }

        else
        {
            lookTarget = Vector3.zero;
        }

        switch (cameraType)
        {
            case CameraType.LauncherTV:
                thisCamera.fieldOfView = Mathf.Clamp(90 - Vector3.Distance(transform.position, lookTarget) / 30, 5, 90);
                break;
            default:
                transform.position = new Vector3(lookTarget.x, lookTarget.y, lookTarget.z + 500);
                break;
        }

        transform.LookAt(lookTarget);
    }
}
