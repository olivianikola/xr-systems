using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    public InputActionReference gripAction;
    public InputActionReference triggerAction;
    public Hand hand;

    void Start()
    {
        if (gripAction != null)
        {
            gripAction.action.Enable();
        }
        else
        {
            Debug.LogError("Grip Action is not assigned in the Inspector!");
        }

        if (triggerAction != null)
        {
            triggerAction.action.Enable();
        }
        else
        {
            Debug.LogError("Trigger Action is not assigned in the Inspector!");
        }
    }

    void Update()
    {
        if (hand == null)
        {
            Debug.LogError("Hand is missing!");
            return;
        }

        if (gripAction != null && gripAction.action != null)
        {
            try
            {
                hand.SetGrip(gripAction.action.ReadValue<float>());
                Debug.Log("Grip value read successfully.");
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error reading grip value: " + ex.Message);
            }
        }

        if (triggerAction != null && triggerAction.action != null)
        {
            try
            {
                hand.SetTrigger(triggerAction.action.ReadValue<float>());
                Debug.Log("Trigger value read successfully.");
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error reading trigger value: " + ex.Message);
            }
        }
    }
}
