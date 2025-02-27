using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "HitDamageCheck", story: "update Stats using [hitController]", category: "Action", id: "b1912629726dd73976157a1d72c08b7b")]
public partial class HitDamageCheckAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyHitController> HitController;
    private float timer;
    private float invincibleTime = 0.3f;

    protected override Status OnStart()
    {
        
        return Status.Running;
    }
    protected override Status OnUpdate() { 
        
        timer -= Time.deltaTime;
        if (HitController.Value.GetResult())
        {
            if (timer <= 0)
            {
                timer = invincibleTime;
                Debug.Log("Hit");
            }
            
        }
        return Status.Running;
    }

    
}

