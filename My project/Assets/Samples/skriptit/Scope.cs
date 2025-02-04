using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Scope : MonoBehaviour
{
    public Animator animator;
    public InputActionReference action;
    public GameObject scopeOverlay;
    public Camera mainCamera;
    public float magnificationFOV = 10f;
    private float normalFOV;
    // public GameObject magnifierCamera;
    private bool isMagnified = false;
    
    void Start()
    {
        action.action.Enable();
        action.action.performed += MagnifyingGlass;
    }
    void MagnifyingGlass(InputAction.CallbackContext context) 
    {
        if (isMagnified == false) {
            StartCoroutine(Magnifine());
        }
        else {
            UnMagnifine();
        }
    }
    IEnumerator Magnifine()
    {
        yield return new WaitForSeconds(.20f);
        isMagnified = true;
        scopeOverlay.SetActive(true);
        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = magnificationFOV;
    }
    void UnMagnifine()
    {
        isMagnified = false;
        scopeOverlay.SetActive(false);
        mainCamera.fieldOfView = normalFOV;
    }
}
