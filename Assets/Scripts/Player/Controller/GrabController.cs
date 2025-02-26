using UnityEngine;

public class GrabController
{
    Player player;
    public GameObject grabBox;
    public Timer timer;
    public Vector3 grabPosition;
    public GameObject grabItem;

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
            grabBox = Player.Instantiate(player.grabBoxPrefab, new Vector3(player.transform.position.x + player.grabHitboxOffsetX, player.transform.position.y + player.grabHitboxOffsetX, 0), Quaternion.identity, player.transform);
        }
        else
        {
            grabBox = Player.Instantiate(player.grabBoxPrefab, new Vector3(player.transform.position.x - player.grabHitboxOffsetX, player.transform.position.y + player.grabHitboxOffsetX, 0), Quaternion.identity, player.transform);
        }
        timer.Set(player.grabDuration);
    }

    public void GrabOver()
    {
        Player.Destroy(grabBox);
    }

    public void getGrabbingPosition(Deflectable df)
    {
        grabPosition = df.getPosition();
    }

}
