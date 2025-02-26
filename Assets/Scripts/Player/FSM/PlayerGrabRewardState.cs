using UnityEngine;

public class PlayerGrabRewardState : PlayerState
{
    public PlayerGrabRewardState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.WeaponCtrl.SetCurrentWP(stateMachine.trigger.getID());
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
        if(base.Update()){
            return true;   
        }
        stateMachine.ChangeState(player.idleState);
        return true;
        //return false;
    }
}
