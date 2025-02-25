using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }

        player.FlipCtrl.onHorizontalInput();

        // idle => Roll
        if ((input.Roll || input.isRollBuffered) && player.RollCtrl.rollCoolDownTimer.TimeUp())
        {

            stateMachine.ChangeState(player.rollState);
            return true;
        }
        // idle => Jump
        if (input.Jump || input.isJumpBuffered)
        {
            stateMachine.ChangeState(player.jumpState);
            return true;
        }
        // Ground => Air
        if (!player.LevelCollisionCtrl.IsGroundDetected() && player.rb.linearVelocityY < 0)
        {
            stateMachine.ChangeState(player.fallState);
            return true;
        }
        // idle => move
        if (input.Xinput != 0)
        {
            stateMachine.ChangeState(player.moveState);
            return true;
        }
        return false;


    }

}