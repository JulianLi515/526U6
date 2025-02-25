using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        input.isRollBuffered = false;
        player.RollCtrl.Prep();
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocity.y);
        //TODO: Use event to change Istate
        player.iState = Player.IState.Fragile;
    }

    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }

        player.RollCtrl.Dashing();
        // dash => wallSlide
        if ((input.Xinput * player.facingDir > 0) && player.LevelCollisionCtrl.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
            return true;
        }
        // dash => fall
        if (player.RollCtrl.rollDurationTimer.TimeUp())
        {
            stateMachine.ChangeState(player.fallState);
            return true;
        }
        return false;
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

}
