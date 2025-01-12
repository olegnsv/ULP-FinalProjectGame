using UnityEngine;
using UnityEngine.Timeline;

public class MinionRanged : MinionBaseScript // INHERITANCE
{

    [SerializeField] private float distanceToKeep = 20f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float gunBarrelOffset = 1.5f;
    [SerializeField] private float projectileSpeed = 50f;
    [SerializeField] private float attackOffset = 20.1f;
    //[SerializeField] private int rangedDamage = 5;
    [SerializeField] private float attackCooldown = 2.0f;

    private float realDistanceToEnemy;
    private float nextAttackTime = 0f;

    protected override void DealDamage()
    {
        if (realDistanceToEnemy <= (attackOffset) && Time.time >= nextAttackTime)
        {
            ShootProjectile();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Start()
    {
        isTargetFound = FindTarget();
    }

    void Update()
    {
        if (isTargetFound)
        {
            realDistanceToEnemy = Vector3.Distance(transform.position, targetPlayer.transform.position);
            MoveToTarget(distanceToKeep);
            TurnToTarget();
            DealDamage();
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

}
