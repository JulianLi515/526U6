using UnityEngine;

public class PlayerSkillState : PlayerState
{
    public PlayerSkillState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        input.isSkillBuffered = false;
        player.WeaponCtrl.Skill();
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
        stateMachine.ChangeState(player.idleState);
        return true;
        return false;
    }
}
