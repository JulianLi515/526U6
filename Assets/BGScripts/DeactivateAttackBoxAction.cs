using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DeactivateAttackBox", story: "Deactivae my [AttackController]", category: "Action", id: "825edb79bb7d957646685972ad50ff48")]
public partial class DeactivateAttackBoxAction : Action
{
    [SerializeReference] public BlackboardVariable<LancerWeaponController> AttackController;
    protected override Status OnStart()
    {
        if (AttackController.Value.gameObject.activeSelf) { 
        
            AttackController.Value.gameObject.SetActive(false);
        }
        return Status.Success;
    }

    
}

