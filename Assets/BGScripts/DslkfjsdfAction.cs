using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "dslkfjsdf", story: "Use [Groundetector] to do groundCheck", category: "Action", id: "f53284b69bfdddb0e103354e3fb8f275")]
public partial class DslkfjsdfAction : Action
{
    [SerializeReference] public BlackboardVariable<GroundDetector> Groundetector;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

