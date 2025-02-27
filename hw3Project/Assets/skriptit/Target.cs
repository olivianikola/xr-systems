using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    public int points;
    private bool isCooldown = false;
    public float cooldownTime = 1f; // Cooldown time in seconds

    private void OnTriggerEnter(Collider other)
    {
        if (isCooldown) return;

        Debug.Log("Trigger detected with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("ThrowObject"))
        {
            Debug.Log("ThrowObject hit! Adding points: " + points);
            Scoring scoringSystem = FindObjectOfType<Scoring>();
            if (scoringSystem != null)
            {
                scoringSystem.AddScore(points);
            }

            // Start cooldown
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
}
