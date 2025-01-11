using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int teamID; // Team ID to be defined in Player script

    public int damageAmount = 15; // Amount of damage of projectile

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
