using UnityEngine;

public class MinionMelee : MinionBaseScript // INHERITANCE
{

    private bool isTargetFound;

    protected override void DealDamage()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        isTargetFound = FindTarget();
    }

    void Update()
    {
        if (isTargetFound)
        {
            MoveToTarget();
        }
        
    }
}
