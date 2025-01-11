using UnityEngine;

public class MinionRanged : MinionBaseScript
{

    [SerializeField] private float distance = 20f;

    protected override void DealDamage()
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        if (FindTarget())
        {
            MoveToTarget(distance);
        }

    }

}
