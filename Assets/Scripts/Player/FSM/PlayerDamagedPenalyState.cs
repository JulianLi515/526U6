using UnityEngine;

public class PlayerDamagedPenalyState : PlayerState
{
    public PlayerDamagedPenalyState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.KnockBackCtrl.Prep();
        player.iState = Player.IState.Invincible;
        
    }

    public override void Exit()
    {
        base.Exit();
        player.iState = Player.IState.Fragile;
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
        player.KnockBackCtrl.ApplyKnockback();

        if (player.KnockBackCtrl.timer.TimeUp())
        {
            stateMachine.ChangeState(player.idleState);
        }
        return false;
    }
}
