using UnityEngine;

public class GrabController
{
    Player player;
    public GameObject GrabBox;
    public Timer timer;

    public GrabController(Player player)
    {
        this.player = player;
        timer = new Timer();
        player.grabTimer = timer;
        player.TimerCountDownCtrl.register(timer);
    }

    public void Grab()
    {
        if (player.facingDir == 1)
        {
            GrabBox = Player.Instantiate(player.grabBoxPrefab, new Vector3(player.transform.position.x + player.grabHitboxOffsetX, player.transform.position.y + player.grabHitboxOffsetX, 0), Quaternion.identity, player.transform);
        }
        else
        {
            GrabBox = Player.Instantiate(player.grabBoxPrefab, new Vector3(player.transform.position.x - player.grabHitboxOffsetX, player.transform.position.y + player.grabHitboxOffsetX, 0), Quaternion.identity, player.transform);
        }
        timer.Set(player.grabDuration);
    }

    public void GrabOver()
    {
        Player.Destroy(GrabBox);
    }

}
