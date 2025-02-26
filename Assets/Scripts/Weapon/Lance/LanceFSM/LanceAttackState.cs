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
        GameObject throwingLance;
        if (stateMachine.attackInfo.isUpPressed())
        {
            throwingLance = Lance.Instantiate(lance.throwableLancePrefabVertical, new Vector3(lance.transform.position.x, lance.transform.position.y, 0), Quaternion.identity);
            return;
        }
        if(stateMachine.attackInfo.isDownPressed())
        {
            throwingLance = Lance.Instantiate(lance.throwableLancePrefabVertical, new Vector3(lance.transform.position.x, lance.transform.position.y, 0), Quaternion.identity);
            return;
        }
        // not pressed, throwing at facing direction;
        throwingLance = Lance.Instantiate(lance.throwableLancePrefabHorizontal, new Vector3(lance.transform.position.x, lance.transform.position.y, 0), Quaternion.identity);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        stateMachine.ChangeState(lance.disappearState);
    }
}
