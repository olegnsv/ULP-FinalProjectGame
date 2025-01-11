using System.Collections;
using System.Collections.Generic;
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

    private Vector3 gunBarrelOffset = new Vector3(0.1f, 0, 1f);


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

        HandleMovement();
        HandleRotation();

        // Shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
        }

        // Handle death
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    void HandleMovement()
    {
        // Handle forward/backward movement
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime; // Move forward
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed * Time.deltaTime; // Move backward
        }
    }

    void HandleRotation()
    {
        // Handle left/right rotation
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime); // Rotate left
        }
        if (Input.GetKey(KeyCode.D))
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
        // Instantiate the projectile and set its velocity
        Transform gunPosition = transform.Find("Glock"); // Shoot from
        Vector3 gunPositionV = gunPosition.position;
        GameObject projectile = Instantiate(projectilePrefab, gunPositionV + gunBarrelOffset, Quaternion.Euler(90, transform.eulerAngles.y, 0));
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        if (projectileRb != null)
        {
            projectileRb.linearVelocity = transform.forward * projectileSpeed; // Send projectile forward
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with a projectile
        if (collision.gameObject.CompareTag("Projectile2")) // Ensure the projectile prefab has the tag "Projectile2"
        {
            TakeDamage(10); // TODO as variable / OOP

            // Destroy the projectile on collision
            Destroy(collision.gameObject);
        }

        //if (collision.gameObject.CompareTag("NPC2"))
        //{
        //    GameObject npc2Object = GameObject.FindGameObjectWithTag("NPC2");
        //    Debug.Log("Found NPC2");
        //    npc2script = npc2Object.GetComponent<NPC2Behavior>();
        //    if (npc2script.isPlayer1Killable)
        //    {
        //        TakeDamage(50);
        //    }
        //} // Old system for death on collision with NPC2
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
