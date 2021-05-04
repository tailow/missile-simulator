using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrackingType
{
    Pursuit,
    ProportionalNavigation,
    AlwaysForward
}

public class MissileScript : MonoBehaviour
{
    public TrackingType trackingType;

    public float speed;
    public float turnRate;

    public GameObject target;

    Vector3 targetDirection;

    void FixedUpdate()
    {
        targetDirection = target.transform.position - transform.position;

        switch (trackingType)
        {
            case TrackingType.AlwaysForward:
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                break;
            case TrackingType.Pursuit:
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection,
                 turnRate * 0.0174532925f * Time.deltaTime, 0.0f);

                transform.rotation = Quaternion.LookRotation(newDirection);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                break;
            case TrackingType.ProportionalNavigation:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Launcher"))
        {
            if (other.CompareTag("Target"))
            {
                Destroy(other.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
