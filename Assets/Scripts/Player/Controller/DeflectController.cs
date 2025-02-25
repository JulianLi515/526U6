using UnityEngine;

public class DeflectController
{
    Player player;
    public GameObject deflectBox;
    public float deflectDuration;
    public Timer timer;

    public DeflectController(Player player)
    {
        this.player = player;
        this.deflectDuration = player.deflectDuration;
        timer = new Timer();
        player.deflectTimer = timer;
        player.TimerCountDownCtrl.register(timer);
    }

    public void Deflect()
    {
        if (player.facingDir == 1)
        {
            deflectBox = Player.Instantiate(player.deflectBoxPrefab, new Vector3(player.transform.position.x + 6f, player.transform.position.y + 0.25f, 0), Quaternion.identity, player.transform);
        }
        else
        {
            deflectBox = Player.Instantiate(player.deflectBoxPrefab, new Vector3(player.transform.position.x - 6f, player.transform.position.y + 0.25f, 0), Quaternion.identity, player.transform);
        }
        timer.Set(deflectDuration);
    }

    public void DefelectOver()
    {
        Player.Destroy(deflectBox);
    }

}
