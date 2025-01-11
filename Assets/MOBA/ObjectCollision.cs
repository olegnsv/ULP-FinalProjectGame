using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    public GameObject npcPrefab1;
    public GameObject npcPrefab2;

    [SerializeField] private int teamID;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check collision with player
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null && playerController.teamID == teamID)
            {
                Vector3 spawnPosition = transform.position; // Get the position where the object will disappear
                
                Destroy(gameObject); // Destroy the current object

                // Spawn NPCs
                Instantiate(npcPrefab1, spawnPosition + Vector3.right * 2, Quaternion.identity);
                Instantiate(npcPrefab2, spawnPosition + Vector3.left * 2, Quaternion.identity);
            }
        }
    }
}
