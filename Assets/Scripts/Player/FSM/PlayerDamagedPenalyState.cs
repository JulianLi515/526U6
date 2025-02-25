using UnityEngine;

public class PlayerDamagedPenalyState : PlayerState
{
    public PlayerDamagedPenalyState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Vector2 direction = (player.transform.position - stateMachine.trigger.dfTransform.position).normalized;
        player.ApplyForceCtrl.ApplyKnockback(direction);
        player.InvincibleCtrl.GoInvincivle(player.KnockBackinvincibilityDuration);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }
        //Call only one frame, Exit after force Applied
        stateMachine.ChangeState(player.idleState);
        return false;
    }
}
