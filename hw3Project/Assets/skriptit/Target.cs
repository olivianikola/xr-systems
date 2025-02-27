using UnityEngine;

public class Target : MonoBehaviour
{
    public int points;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("ThrowObject"))
        {
            Debug.Log("ThrowObject hit! Adding points: " + points);
            Scoring scoringSystem = FindObjectOfType<Scoring>();
            if (scoringSystem != null)
            {
                scoringSystem.AddScore(points);
            }
        }
    }
}
