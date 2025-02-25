using Unity.VisualScripting;
using UnityEngine;

public class FlipController
{
    public Player player;

    public FlipController(Player player)
    {
        this.player = player;
    }

    public void Flip()
    {
        player.facingDir = player.facingDir * -1;
        player.transform.Rotate(0, 180, 0);
    }

    //TODO: put it in Entity? Called in PlayerState
    //Flip if input(float _x) vector has opposite horizonal direction with respect to facingDirection
    public void onHorizontalInput()
    {
        if (player.input.Xinput > 0 && player.facingDir == -1)
        {
            Flip();
        }
        else if (player.input.Xinput < 0 && player.facingDir == 1)
        {
            Flip();
        }

    }

}
