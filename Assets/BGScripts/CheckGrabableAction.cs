using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckGrabable", story: "update [GrabController] info with [WeaponHitCounter]", category: "Action", id: "9e796f955e78cdd11ede4f98bdbd079d")]
public partial class CheckGrabableAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyGrabController> GrabController;
    [SerializeReference] public BlackboardVariable<int> WeaponHitCounter;
    [SerializeReference] public BlackboardVariable<int> maxHit;
    protected override Status OnStart()
    {
        if (WeaponHitCounter.Value >= 1)
        {
            GrabController.Value.gameObject.SetActive(true);
            return Status.Success;
        }
        else
        {
            GrabController.Value.gameObject.SetActive(false);
            return Status.Failure;
        }
        
    }

    
}

