using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherScript : MonoBehaviour
{
    public GameObject missilePrefab;
    public Transform missileParent;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            LaunchMissile();
        }
    }

    void LaunchMissile()
    {
        GameObject missile = Instantiate(missilePrefab, transform.position + Vector3.up * 10, Quaternion.Euler(-90, 0, 0));

        missile.transform.SetParent(missileParent);
    }
}
