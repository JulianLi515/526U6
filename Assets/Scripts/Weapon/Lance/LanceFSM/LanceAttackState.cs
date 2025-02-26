using JetBrains.Annotations;
using UnityEngine;

public class LanceAttackState : LanceState
{
    public LanceAttackState(LanceStateMachine stateMachine, Lance lance) : base(stateMachine, lance)
    {
    }

    public override void Enter()
    {
        base.Enter();
        lance.Disappear();
        GameObject throwingLance;
        if (stateMachine.attackInfo.isUpPressed())
        {
            throwingLance = Lance.Instantiate(lance.throwableLancePrefabVertical, new Vector3(lance.shootingPostion.position.x, lance.shootingPostion.position.y, 0), Quaternion.identity);
            throwingLance.transform.up = Vector2.down;
            return;
        }
        if (stateMachine.attackInfo.isDownPressed())
        {
            throwingLance = Lance.Instantiate(lance.throwableLancePrefabVertical, new Vector3(lance.shootingPostion.position.x, lance.shootingPostion.position.y, 0), Quaternion.identity);
            return;
        }
        // not pressed, throwing at facing direction;
        throwingLance = Lance.Instantiate(lance.throwableLancePrefabHorizontal, new Vector3(lance.shootingPostion.position.x, lance.shootingPostion.position.y, 0), Quaternion.identity);
        if (lance.player.facingDir == -1)
        {
            throwingLance.transform.right = Vector2.left;
        }
    }

    public override void Exit()
    {
        base.Exit();
        //Set Disappear immediately
    }

    public override void Update()
    {
        base.Update();
        stateMachine.ChangeState(lance.disappearState);
    }
}
