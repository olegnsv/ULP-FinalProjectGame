using UnityEngine;

public class MinionRanged : MinionBaseScript // INHERITANCE
{

    [SerializeField] private float distance = 20f;

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
            MoveToTarget(distance);
        }

    }

}
