using System;
using UnityEngine;
using UnityEngine.Windows;

public class AirMoveController
{
    public Player player;
    public float moveSpeed;

    public AirMoveController(Player player)
    {
        this.player = player;
        this.moveSpeed = player.HorizontalSpeedFalling;
    }
    
    public void OnHorizontalInput(float Xinput)
    {
        player.rb.linearVelocity = new Vector2(Xinput*moveSpeed, player.rb.linearVelocity.y);
    }

    public void Freeze()
    {
        player.rb.linearVelocity = new Vector2(0,0);
    }
}
