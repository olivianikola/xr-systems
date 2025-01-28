using UnityEngine;
using UnityEngine.InputSystem;

public class PointLight : MonoBehaviour
{
    public Light lightComponent;
    public InputActionReference action;
    private bool isChanged = false;

    void Start()
    {
        lightComponent = GetComponent<Light>();
        action.action.Enable();
        action.action.performed += ChangeColor;
    }
    void ChangeColor(InputAction.CallbackContext context) 
    {
        if (isChanged) {
            lightComponent.color = Color.white;
            isChanged = false;
        }
        
        else {
            lightComponent.color = Color.red;
            isChanged = true;
        }
    }
}
