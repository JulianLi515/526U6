using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "checkIfGrabbed", story: "update [isWeaponGrabbed] with [GrabController]", category: "Action", id: "208a9eef90dda4807f5dbeeb0cf026aa")]
public partial class CheckIfGrabbedAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> IsWeaponGrabbed;
    [SerializeReference] public BlackboardVariable<EnemyGrabController> GrabController;
    protected override Status OnStart()
    {
        if (GrabController.Value.gameObject.activeSelf)
        {
            IsWeaponGrabbed.Value = GrabController.Value.GetResult();
            return Status.Success;
        }
        return Status.Failure;
    }


}

