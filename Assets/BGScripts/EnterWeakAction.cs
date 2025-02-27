using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "enterWeak", story: "stun for [n] seconds if [IsWeaponGrabbed]", category: "Action", id: "63d6af5696118bb748d9eb87aa72763b")]
public partial class EnterWeakAction : Action
{
    [SerializeReference] public BlackboardVariable<float> N;
    [SerializeReference] public BlackboardVariable<bool> IsWeaponGrabbed;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    protected override Status OnStart()
    {
        return Status.Success;
        //if (IsParriedDuringUltimate.Value)
        //{
        //    Self.Value.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;   
        //    return Status.Running;
        //}
        //else
        //{
        //    return Status.Failure;
        //}
    }

    //protected override Status OnUpdate()
    //{
    //    Debug.Log("Dropping weapon");
    //    return Status.Running;
    //}

    //protected override void OnEnd()
    //{
    //}
}

