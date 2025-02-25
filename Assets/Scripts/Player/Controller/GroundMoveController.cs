using UnityEngine;

public class GroundMoveController
{
    
    public Player player;

    public GroundMoveController(Player player)
    {
        this.player = player;
    }

    public void onHorizontalInput(float Xinput)
    {
        player.rb.linearVelocity = new Vector2(Xinput*player.HorizontalSpeedGround, player.rb.linearVelocity.y);
    }
    public void Freeze()
    {
        player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocity.y);
    }
    
}
