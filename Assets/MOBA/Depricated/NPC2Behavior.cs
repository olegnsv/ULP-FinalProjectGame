using UnityEngine;

public class NPC2Behavior : MonoBehaviour
{
    public float speed = 5f;

    private GameObject targetPlayer;

    public bool isPlayer1Killable;

    private void Start()
    {
        // Find the object with the tag "Player"
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (targetPlayer != null)
        {
            // Move towards the Player
            Vector3 direction = (targetPlayer.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            isPlayer1Killable = true;
            Debug.Log("Player1 is now killable");
        }
    }
}