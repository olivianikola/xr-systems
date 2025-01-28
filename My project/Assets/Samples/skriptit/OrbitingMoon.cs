using UnityEngine;

public class OrbitingMoon : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up, 20 * Time.deltaTime);
    }
}
