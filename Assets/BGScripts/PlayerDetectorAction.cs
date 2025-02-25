using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PlayerDetector", story: "Update [Player] with [BoxPlayerDetector]", category: "Action", id: "e370436099ffa2d63eed53acab6b317d")]
public partial class PlayerDetectorAction : Action
{
    [SerializeReference] public BlackboardVariable<Player> Player;
    [SerializeReference] public BlackboardVariable<PlayerDetector> BoxPlayerDetector;
    protected override Status OnStart()
    {
        Player player = BoxPlayerDetector.Value.isPlayerInRange();
        if (player != null)
        {
            Player.Value = player;
            return Status.Success;
        }
        else
        {
            Player.Value = null;
            return Status.Failure;
        }

        
    }


}

