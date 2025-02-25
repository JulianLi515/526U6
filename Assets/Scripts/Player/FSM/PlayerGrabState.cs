using UnityEngine;

public class PlayerGrabState : PlayerState
{
    public PlayerGrabState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        input.isGrabBuffered = false;
        player.GrabCtrl.Grab();
        //TODO; modify, there must be 1 frame of Fragile when deflect finished
        player.iState = Player.IState.Grab;
    }

    public override void Exit()
    {
        base.Exit();
        player.GrabCtrl.GrabOver();
        //TODO; modify, there must be 1 frame of Fragile when deflect finished
        player.iState = Player.IState.Fragile;
    }


    public override bool Update()
    {
        if (base.Update())
        {
            return true;
        }

        player.AirMoveCtrl.Freeze();

        if (player.GrabCtrl.timer.TimeUp())
        {
            player.stateMachine.ChangeState(player.fallState);
            return true;
        }
        return false;
    }
}
