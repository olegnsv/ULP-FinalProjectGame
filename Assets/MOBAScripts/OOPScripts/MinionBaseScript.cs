using Unity.VisualScripting;
using UnityEngine;

public abstract class MinionBaseScript : MonoBehaviour
{

    protected GameObject targetPlayer;

    protected int health = 100; // Minion health

    protected int speed = 10;

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
            else
            {
                Debug.Log("Enemy player not found");
                return false;
            }

        }
        return false;
    }

    protected virtual void MoveToTarget()
    {
        Vector3 direction = (targetPlayer.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
