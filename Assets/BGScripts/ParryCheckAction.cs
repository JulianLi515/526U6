using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "parryCheck", story: "Do parry check using [HitController] and update [IsParriedDuringUltimate]", category: "Action", id: "b25b0ffb09fb850a7410c1f5638e47a0")]

public partial class ParryCheckAction : Action
{
    [SerializeReference] public BlackboardVariable<HitController> HitController;
    [SerializeReference] public BlackboardVariable<bool> IsParriedDuringUltimate;
    [SerializeReference] public BlackboardVariable<bool> CheckPoint1;
    private HitController hitController;
    protected override Status OnStart()
    {
        hitController = HitController.Value;
        hitController.StartAttackCheck();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (CheckPoint1.Value)
        {
            CheckPoint1.Value = false;
            return Status.Failure;
        }
        int hitResult = hitController.GetHitResult();
        switch (hitResult)
        {
            case 0:
                // dodged the attack, keep effective
                return Status.Running;
                
            case 1:
                // parried the attack, 
                return Status.Running;
                
            case 2:
                // parried the attack during ultimate
                IsParriedDuringUltimate.Value = true;
                
                return Status.Success;

        }
        return Status.Success;
    }

    protected override void OnEnd()
    {
        hitController.StopAttackCheck();
    }
}

