using UnityEngine;

public class MinionMelee : MinionBaseScript
{
    protected override void DealDamage()
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        if (FindTarget())
        {
            MoveToTarget();
        }
        
    }
}
