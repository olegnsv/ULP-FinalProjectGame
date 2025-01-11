using UnityEngine;

public class ObjectCollision2 : MonoBehaviour
{
    public GameObject npcPrefab1;
    public GameObject npcPrefab2;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player2")) // Check collision with player
        {
            // Get the position where the object will disappear
            Vector3 spawnPosition = transform.position;

            // Destroy the current object
            Destroy(gameObject);

            // Spawn NPCs
            Instantiate(npcPrefab1, spawnPosition + Vector3.right * 2, Quaternion.identity);
            Instantiate(npcPrefab2, spawnPosition + Vector3.left * 2, Quaternion.identity);
        }
    }
}
