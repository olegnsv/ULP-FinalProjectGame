using UnityEngine;

public class MinionMelee : MinionBaseScript // INHERITANCE
{
    [SerializeField] private float distanceToKeep = 2.5f;
    [SerializeField] private float attackOffset = 2.6f;
    [SerializeField] private int meleeDamage = 5;
    [SerializeField] private float attackCooldown = 2.0f;

    private float realDistanceToEnemy;
    private float nextAttackTime = 0f;

    protected override void DealDamage()
    {
        if (realDistanceToEnemy <= (attackOffset) && Time.time >= nextAttackTime)
        {
            PlayerController playerController = targetPlayer.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(meleeDamage);
                nextAttackTime = Time.time + attackCooldown;
            }
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
            //MoveToTarget();
            MoveToTarget(distanceToKeep);
            TurnToTarget();
            DealDamage();
        }
        //Debug.Log($"Melee Real Distance to Enemy: {realDistanceToEnemy}");
    }
}
