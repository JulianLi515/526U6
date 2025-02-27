using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckGrabable", story: "update [HitController] info with [WeaponHitCounter]", category: "Action", id: "9e796f955e78cdd11ede4f98bdbd079d")]
public partial class CheckGrabableAction : Action
{
    [SerializeReference] public BlackboardVariable<HitController> HitController;
    [SerializeReference] public BlackboardVariable<int> WeaponHitCounter;
    [SerializeReference] public BlackboardVariable<int> maxHit;
    protected override Status OnStart()
    {
        if (WeaponHitCounter.Value >= 1)
        {
            HitController.Value.BecomeGrabbable();
            return Status.Success;
        }
        else
        {
            HitController.Value.BecomeInGrabbale();
            return Status.Failure;
        }
        
    }

    
}

