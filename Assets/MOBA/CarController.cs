using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject npcMeleePrefab;
    public GameObject npcRangedPrefab;
    [SerializeField] private float speed = 5f;

    [SerializeField] private int teamID;
    private Renderer carRenderer;

    void Start()
    {
        carRenderer = GetComponent<Renderer>();

        if (teamID == 1)
        {
            carRenderer.material.SetColor("_Color", Color.blue);
        }
        else if (teamID == 2)
        {
            carRenderer.material.SetColor("_Color", Color.red);
        }
        else
        {
            Debug.LogError("Unknown TeamID.");
        }
        
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

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
                Instantiate(npcMeleePrefab, spawnPosition + Vector3.right * 2, Quaternion.identity);
                Instantiate(npcRangedPrefab, spawnPosition + Vector3.left * 2, Quaternion.identity);
            }
        }
    }
}
