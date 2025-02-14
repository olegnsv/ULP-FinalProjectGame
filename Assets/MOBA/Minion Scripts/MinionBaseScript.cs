using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public abstract class MinionBaseScript : MonoBehaviour // INHERITANCE
{

    protected GameObject targetPlayer;

    protected int health = 100; // Minion health

    protected int speed = 10;

    protected bool isTargetFound;

    public int teamID;

    protected abstract void DealDamage();

    private void OnCollisionEnter(Collision collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();

        if (projectile != null)
        {
            health -= projectile.damageAmount;
        }
    }

    protected bool FindTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players == null)
        {
            Debug.Log("No players found");
            return false;
        }

        foreach (GameObject player in players)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null && playerController.teamID != teamID)
            {
                targetPlayer = player;
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// Moves minion to target.
    /// </summary>
    protected virtual void MoveToTarget() // POLYMORPHISM
    {
        Vector3 direction = (targetPlayer.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    /// <summary>
    /// Moves minion to target while keeping distance.
    /// </summary>
    /// <param name="distanceToKeep">Distance to keep from the target.</param>
    protected virtual void MoveToTarget(float distanceToKeep) // POLYMORPHISM
    {

        //float realDistanceToTarget = Vector3.Distance(transform.position, targetPlayer.transform.position);
        //Debug.Log($"Distance to Target: {realDistanceToTarget}");
        
        Vector3 direction = (targetPlayer.transform.position - transform.position).normalized;
        Vector3 targetPosition = targetPlayer.transform.position - direction * distanceToKeep;

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);  // Distance to target POSITION (enemy position - offset)

        if (distanceToTarget > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

    }

    protected void TurnToTarget()
    {
        Vector3 targetDirection = targetPlayer.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 1 * Time.deltaTime, 0);
        //Debug.DrawRay(transform.position, newDirection, Color.red);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
