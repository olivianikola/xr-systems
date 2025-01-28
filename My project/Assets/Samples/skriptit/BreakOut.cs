using UnityEngine;
using UnityEngine.InputSystem;

public class BreakOut : MonoBehaviour
{
    public Transform person;
    public InputActionReference action;
    private bool isOutside = false;
    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = person.position;

        action.action.Enable();
        action.action.performed += ChangeLocation;
    }

    void ChangeLocation(InputAction.CallbackContext context)
    {
        if (isOutside)
        {
            person.position = startingPosition;
            isOutside = false;
        }
        else
        {
            person.position = new Vector3(0, 4, -40);
            isOutside = true;
        }
    }
}