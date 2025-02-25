using UnityEngine;

public class InvincibleController
{
    public Player player;
    public Timer timer;
    public bool invincible => timer.timer > 0;

    public InvincibleController(Player player)
    {
        this.player = player;
        timer = new Timer();
        player.Invincibletimer = timer;
        player.TimerCountDownCtrl.register(timer);
    }

    public void GoInvincivle(float invincibleDuration)
    {
        timer.Set(invincibleDuration);
    }

}
