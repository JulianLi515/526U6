using System.Collections.Generic;
using UnityEngine;

public class EnemyGrabController : EnemyHitBoxBase,Deflectable
{
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public int getID()
    {
        return 0;
    }

    public override void playerDestroy(int _param)
    {
        result = _param;
        base.playerDestroy(_param);
    }
}
