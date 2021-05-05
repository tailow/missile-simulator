using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrackingType
{
    PurePursuit,
    ProportionalNavigation,
    AlwaysForward
}

public class MissileScript : MonoBehaviour
{
    public TrackingType trackingType;

    float speed;
    float startTime;
    float missileSpeed;

    Vector3 missileVelocity;
    Vector3 missilePrevPos;

    Vector3 targetVelocity;
    Vector3 targetPrevPos;

    public float turnRate;
    public float acceleration;
    public float accelerationTime;
    public float drag;

    TrailRenderer trail;

    GameObject target;

    Vector3 targetDirection;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target");

        trail = GetComponent<TrailRenderer>();

        startTime = Time.time;

        Destroy(gameObject, 15f);
    }

    void FixedUpdate()
    {
        if (Time.time - startTime < accelerationTime)
        {
            speed += acceleration * Time.deltaTime;
        }

        else if (trail.emitting)
        {
            trail.emitting = false;
        }

        speed -= drag * Time.deltaTime;

        targetDirection = target.transform.position - transform.position;

        missileVelocity = transform.position - missilePrevPos;

        missileSpeed = missileVelocity.magnitude * 60;

        missilePrevPos = transform.position;

        switch (trackingType)
        {
            case TrackingType.AlwaysForward:
                break;
            case TrackingType.PurePursuit:
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection,
                 turnRate * 0.0174532925f * Time.deltaTime, 0.0f);

                transform.rotation = Quaternion.LookRotation(newDirection);
                break;
            case TrackingType.ProportionalNavigation:
                targetVelocity = target.transform.position - targetPrevPos;
                targetPrevPos = target.transform.position;

                Vector3 targetToMissile = (transform.position - target.transform.position).normalized;
                Vector3 predictionStepPos = target.transform.position + targetToMissile * missileVelocity.magnitude;

                Vector3 predictedDirection = target.transform.position + targetVelocity - predictionStepPos;

                Vector3 predictedCollisionPoint = transform.position + predictedDirection
                 * Vector3.Distance(transform.position, target.transform.position) / missileVelocity.magnitude;

                Debug.DrawLine(transform.position, predictedCollisionPoint, Color.red, Time.deltaTime);

                Vector3 rotationStep = Vector3.RotateTowards(transform.forward, predictedDirection,
                 turnRate * 0.0174532925f * Time.deltaTime, 0.0f);

                transform.rotation = Quaternion.LookRotation(rotationStep);
                break;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Launcher"))
        {
            if (other.CompareTag("Target"))
            {
                Debug.Log("Hit!");
                //Destroy(other.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
