using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEditor;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DamageCheck", story: "do DamageCheck using [HitBoxController]", category: "Action", id: "f21ca5f1143a724bb46b9d3fe379b84a")]
public partial class DamageCheckAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyHitController> HitBoxController;
    [SerializeReference] public BlackboardVariable<float> ITimeOnHit;
    [SerializeReference] public BlackboardVariable<float> Health;
    [SerializeReference] public BlackboardVariable<EnemyDestroyer> Destroyer;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Drop;
    private float timer;

    protected override Status OnStart()
    {
        Health.Value = 100;
        timer = 0;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            int damage = HitBoxController.Value.GetResult();
            if (damage > 0)
            {
                //Debug.Log("Enemy is Hit");
                //Debug.Log(Time.time);
                Health.Value -= damage;
                timer = ITimeOnHit.Value;
            }
        }
        if (Health.Value < 0)
        {
            /// Drop weapon Here
            //Vector2 pos = Self.Value.transform.position;
            //GameObject gb = GameObject.Instantiate(Drop.Value, pos, Quaternion.identity);
            ///

            Destroyer.Value.DestroyMe();
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

