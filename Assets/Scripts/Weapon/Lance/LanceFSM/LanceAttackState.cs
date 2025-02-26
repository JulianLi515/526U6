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
        lance.timer = 1f;
        GameObject throwingLance;
        throwingLance = Lance.Instantiate(lance.throwableLancePrefab, new Vector3(lance.transform.position.x, lance.transform.position.y, 0), Quaternion.identity);
    }

    public override void Exit()
    {
        base.Exit();
        lance.Appear();
    }

    public override void Update()
    {
        base.Update();
        if(lance.timer <= 0f)
        {
            stateMachine.ChangeState(lance.idleState);
            return;
        }
    }
}
