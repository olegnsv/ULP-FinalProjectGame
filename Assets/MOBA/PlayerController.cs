using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 9.0f;
    public float rotationSpeed = 90.0f;
    private Rigidbody playerRb;

    public int teamID;

    public GameObject projectilePrefab;
    public float projectileSpeed = 50f;

    public int health = 100;
    public TextMeshProUGUI healthText;

    private Transform cameraTransform; // Reference to the child camera

    private NPC2Behavior npc2script;

    public float gunBarrelOffset = 1.5f;


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        cameraTransform = transform.Find("Camera"); // Child camera is named "Camera"

        if (cameraTransform == null)
        {
            Debug.LogError("Child camera not found.");
        }

        UpdateHealthText();
    }

    // Update is called once per frame
    void Update()
    {

        HandleMovement(); // ABSTRACTION
        HandleRotation();

        // Shooting
        if (teamID == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShootProjectile();
            }
        }
        else if (teamID == 2)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                ShootProjectile();
            }
        }
        

        // Handle death
        if (health == 0)
        {
            Destroy(gameObject);
            GameManager.Instance.isGameRunning = false;
        }
    }

    void HandleMovement()
    {
        // Handle forward/backward movement
        if (teamID == 1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward * speed * Time.deltaTime; // Move forward
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= transform.forward * speed * Time.deltaTime; // Move backward
            }
        }
        else if (teamID == 2)
        {
            if (Input.GetKey(KeyCode.I))
            {
                transform.position += transform.forward * speed * Time.deltaTime; // Move forward
            }
            if (Input.GetKey(KeyCode.K))
            {
                transform.position -= transform.forward * speed * Time.deltaTime; // Move backward
            }
        }
        
    }

    void HandleRotation()
    {
        // Handle left/right rotation
        if (teamID == 1)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime); // Rotate left
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); // Rotate right
            }
        }
        else if (teamID == 2)
        {
            if (Input.GetKey(KeyCode.J))
            {
                transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime); // Rotate left
            }
            if (Input.GetKey(KeyCode.L))
            {
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); // Rotate right
            }
        }
        
        // Child camera rotates with the player
        if (cameraTransform != null)
        {
            cameraTransform.rotation = transform.rotation;
        }
    }

    void ShootProjectile()
    {
        // Instantiate the projectile and set its velocity
        Transform gunPosition = transform.Find("Glock"); // Shoot from
        Vector3 gunPositionV = gunPosition.position;
        GameObject projectile = Instantiate(projectilePrefab, gunPositionV + gunPosition.right * gunBarrelOffset, Quaternion.Euler(90, transform.eulerAngles.y, 0));
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        if (projectileRb != null)
        {
            projectileRb.linearVelocity = transform.forward * projectileSpeed; // Send projectile forward
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with a projectile
        if (collision.gameObject.CompareTag("Projectile")) // Ensure the projectile prefab has the tag "Projectile"
        {
            TakeDamage(10); // TODO as variable / OOP

            // Destroy the projectile on collision
            Destroy(collision.gameObject);
        }

    }

    void TakeDamage(int damage)
    {
        health -= damage;

        UpdateHealthText();

        if (health <= 0)
        {
            Debug.Log("Player died.");
        }
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Жизни: " + health;
        }
    }
}
