using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    // Animation
    Animator animator;
    public float animationSpeed;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;
    private string gripParameter = "Grip";
    private string triggerParameter = "Trigger";

    // Physics movement
    public GameObject followObject;
    private float followSpeed = 30f;
    private float rotateSpeed = 100f;
    private Vector3 positionOffset;
    private Vector3 rotationOffset;
    private Transform followTarget;
    private Rigidbody body;

    void Start()
    {
        // Animation
        animator = GetComponent<Animator>();
        // Ensure the hand is open at the start
        animator.SetFloat(gripParameter, 0f);
        animator.SetFloat(triggerParameter, 0f);
        // Physics movement
        followTarget = followObject.transform;
        body = GetComponent<Rigidbody>();
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.mass = 20f;

        // Teleport hands in the beginning
        body.position = followTarget.position;
        body.rotation = followTarget.rotation;
    }
    
    void Update()
    {
        AnimateHand();
        PhysicsMove();
    }

    internal void SetGrip(float value)
    {
        gripTarget = value;
    }

    internal void SetTrigger(float value)
    {
        triggerTarget = value;
    }

    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(gripParameter, gripCurrent);
        }
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(triggerParameter, triggerCurrent);
        }
    }

    private void PhysicsMove()
    {
        // Position
        var positionWithOffset = followTarget.position + positionOffset;
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        body.linearVelocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);

        // Rotation
        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        body.angularVelocity = axis * angle * Mathf.Deg2Rad * rotateSpeed;
    }
}
