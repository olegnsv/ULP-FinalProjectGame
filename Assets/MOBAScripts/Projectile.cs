using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Start()
    {
        // Destroy the projectile after 5 seconds if it hasn't collided
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject); // Destroy the projectile
        }
    }
}
