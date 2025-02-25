using UnityEngine;

public class ApplyForceController
{
    public Player player;

    public ApplyForceController(Player player)
    {
        this.player = player;
    }

    public void ApplyKnockback(Vector2 attackDirection)
    {
        //TODO, parameterize attackForce and make different knockback given different attack force
        float attackForce = 10f;
        // Calculate the knockback force
        Vector2 knockbackForce = attackDirection.normalized * attackForce * player.knockbackForceMultiplier;

        //Set Speed to 0 before applyforce;
        player.AirMoveCtrl.Freeze();
        player.rb.AddForce(knockbackForce, ForceMode2D.Impulse);
    }
}
