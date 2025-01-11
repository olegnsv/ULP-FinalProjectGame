using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController2 : MonoBehaviour
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

    private NPC1Behavior npc1script;

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

    void Update()
    {
        HandleMovement();
        HandleRotation();

        // Shooting
        if (Input.GetKeyDown(KeyCode.N))
        {
            ShootProjectile();
        }

        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    void HandleMovement()
    {
        // Handle forward/backward movement
        if (Input.GetKey(KeyCode.I))
        {
            transform.position += transform.forward * speed * Time.deltaTime; // Move forward
        }
        if (Input.GetKey(KeyCode.K))
        {
            transform.position -= transform.forward * speed * Time.deltaTime; // Move backward
        }
    }

    void HandleRotation()
    {
        // Rotate the player left and right
        if (Input.GetKey(KeyCode.J))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime); // Rotate left
        }
        if (Input.GetKey(KeyCode.L))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); // Rotate right
        }

        // Child camera rotates with the player
        if (cameraTransform != null)
        {
            cameraTransform.rotation = transform.rotation;
        }
    }

    void ShootProjectile()
    {
        Transform gunPosition = transform.Find("Glock");
        Vector3 gunPositionV = gunPosition.position;
        GameObject projectile = Instantiate(projectilePrefab, gunPositionV + new Vector3(0.1f, 0, 1f), Quaternion.Euler(90, 0, 0));
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        if (projectileRb != null)
        {
            projectileRb.linearVelocity = transform.forward * projectileSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with a projectile
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(10); // TODO

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("NPC1"))
        {
            GameObject npc1Object = GameObject.FindGameObjectWithTag("NPC1");
            Debug.Log("Found NPC1");
            npc1script = npc1Object.GetComponent<NPC1Behavior>();
            if (npc1script.isPlayer2Killable)
            {
                TakeDamage(50);
            }
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
