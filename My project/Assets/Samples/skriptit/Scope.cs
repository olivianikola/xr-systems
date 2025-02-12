using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;

public class Scope : MonoBehaviour
{
    public Transform camera;

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.parent.position - camera.position, transform.parent.up);
    }
}
