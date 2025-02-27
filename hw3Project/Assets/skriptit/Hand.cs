using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    Animator animator;
    public float speed;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;
    private string gripParameter = "Grip";
    private string triggerParameter = "Trigger";

    void Start()
    {
        animator = GetComponent<Animator>();
        // Ensure the hand is open at the start
        animator.SetFloat(gripParameter, 0f);
        animator.SetFloat(triggerParameter, 0f);
        Debug.Log("Hand initialized with open state.");
    }
    
    void Update()
    {
        AnimateHand();
    }

    internal void SetGrip(float value)
    {
        gripTarget = value;
        Debug.Log("SetGrip called with value: " + value);
    }

    internal void SetTrigger(float value)
    {
        triggerTarget = value;
        Debug.Log("SetTrigger called with value: " + value);
    }

    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat(gripParameter, gripCurrent);
            Debug.Log("Animating grip to: " + gripCurrent);
        }
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat(triggerParameter, triggerCurrent);
            Debug.Log("Animating trigger to: " + triggerCurrent);
        }
    }
}
