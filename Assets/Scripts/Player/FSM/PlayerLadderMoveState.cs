using UnityEngine;

public class PlayerLadderMoveState : PlayerState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerLadderMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
       
        base.Enter();
        player.rb.gravityScale = 0f;
        player.JumpCtrl.ResetCounter(2);
    }

    public override void Exit()
    {
        player.rb.gravityScale = player.gravityScale;
        base.Exit();
    }

    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }
        if (input.Jump || input.isJumpBuffered)
        {
            stateMachine.ChangeState(player.jumpState);
            return true;
        }
        player.FlipCtrl.onHorizontalInput();
        player.rb.linearVelocityX = input.Xinput * player.ladderHorizontalSpeed;
        player.rb.linearVelocityY = input.Yinput * player.ladderVerticalSpeed;
        return false;
        
    }
}
