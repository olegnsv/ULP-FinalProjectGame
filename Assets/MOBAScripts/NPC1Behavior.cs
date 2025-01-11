using UnityEngine;

public class NPC1Behavior : MonoBehaviour
{
    public float speed = 5f;

    private GameObject targetPlayer;

    public bool isPlayer2Killable;

    private void Start()
    {
        // Find the object with the tag "Player2"
        targetPlayer = GameObject.FindGameObjectWithTag("Player2");
    }

    private void Update()
    {
        if (targetPlayer != null)
        {
            // Move towards the Player2 object
            Vector3 direction = (targetPlayer.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile2"))
        {
            isPlayer2Killable = true;
            Debug.Log("Player1 is now killable");
        }
    }
}