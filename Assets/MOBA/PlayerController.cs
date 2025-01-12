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

    private Animator playerAnimator;

    private KeyCode forwardKey;
    private KeyCode backwardKey;
    private KeyCode leftKey;
    private KeyCode rightKey;
    private KeyCode shootKey;
    private KeyCode reloadKey;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        cameraTransform = transform.Find("Camera"); // Child camera is named "Camera"

        if (cameraTransform == null)
        {
            Debug.LogError("Child camera not found.");
        }

        SetTeamControls();
        UpdateHealthText();
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleShooting();
        HandleReloading();
        HandleDeath();
    }

    void SetTeamControls()
    {
        if (teamID == 1)
        {
            forwardKey = KeyCode.W;
            backwardKey = KeyCode.S;
            leftKey = KeyCode.A;
            rightKey = KeyCode.D;
            shootKey = KeyCode.Space;
            reloadKey = KeyCode.R;
        }
        else if (teamID == 2)
        {
            forwardKey = KeyCode.I;
            backwardKey = KeyCode.K;
            leftKey = KeyCode.J;
            rightKey = KeyCode.L;
            shootKey = KeyCode.N;
            reloadKey = KeyCode.P;
        }
    }

    void HandleMovement()
    {
        if (Input.GetKey(forwardKey))
        {
            transform.position += transform.forward * speed * Time.deltaTime; // Move forward
        }
        if (Input.GetKey(backwardKey))
        {
            transform.position -= transform.forward * speed * Time.deltaTime; // Move backward
        }
    }

    void HandleRotation()
    {
        if (Input.GetKey(leftKey))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime); // Rotate left
        }
        if (Input.GetKey(rightKey))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); // Rotate right
        }

        // Child camera rotates with the player
        if (cameraTransform != null)
        {
            cameraTransform.rotation = transform.rotation;
        }
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(shootKey))
        {
            ShootProjectile();
        }
    }

    void HandleReloading()
    {
        if (Input.GetKeyDown(reloadKey))
        {
            playerAnimator.SetTrigger("tReload");
        }
    }

    void HandleDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.isGameRunning = false;
        }
    }

    void ShootProjectile()
    {
        // Instantiate the projectile and set its velocity
        Transform gunPosition = transform.Find("Hands/Glock"); // Shoot from
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

    public void TakeDamage(int damage)
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