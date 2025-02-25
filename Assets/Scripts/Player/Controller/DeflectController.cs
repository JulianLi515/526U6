using UnityEngine;

public class DeflectController
{
    Player player;
    public GameObject deflectBox;
    public Timer timer;
    public Timer deflectJumpTimer;

    public DeflectController(Player player)
    {
        this.player = player;
        timer = new Timer();
        player.deflectTimer = timer;
        player.TimerCountDownCtrl.register(timer);
    }

    public void Deflect()
    {
        if (player.facingDir == 1)
        {
            deflectBox = Player.Instantiate(player.deflectBoxPrefab, new Vector3(player.transform.position.x + player.deflectHitboxOffsetX, player.transform.position.y + player.deflectHitboxOffsetY, 0), Quaternion.identity, player.transform);
        }
        else
        {
            deflectBox = Player.Instantiate(player.deflectBoxPrefab, new Vector3(player.transform.position.x - player.deflectHitboxOffsetX, player.transform.position.y + player.deflectHitboxOffsetY, 0), Quaternion.identity, player.transform);
        }
        timer.Set(player.deflectDuration);
    }

    public void Bump()
    {
        player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, player.deflectJumpSpeed);
        player.JumpCounter = 1;
    }

    public void DefelectOver()
    {
        Player.Destroy(deflectBox);
    }

}
