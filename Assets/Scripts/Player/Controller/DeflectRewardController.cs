using UnityEngine;

public class DeflectRewardController
{
    public Player player;
    public Timer timer;

    public DeflectRewardController(Player player)
    {
        this.player = player;
        timer = new Timer();
        player.TimerCountDownCtrl.register(timer);
    }
}
