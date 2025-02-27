using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "OnGrabUpdate", story: "update [isWeaponGrabbed] with [WeaponHitController]", category: "Action", id: "438d4a28b1952871c61c5bccdc7e6573")]
public partial class OnGrabUpdateAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> IsWeaponGrabbed;
    [SerializeReference] public BlackboardVariable<HitController> WeaponHitController;

    protected override Status OnStart()
    {
        if (WeaponHitController.Value.IsGrabbed())
        {
            IsWeaponGrabbed.Value = true;
            return Status.Success;
        }
        else
        {
            IsWeaponGrabbed.Value = false;
            return Status.Failure;
        }
        
    }

    
}

