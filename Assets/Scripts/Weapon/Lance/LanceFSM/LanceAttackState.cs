using JetBrains.Annotations;
using UnityEngine;

public class LanceAttackState : LanceState
{
    public LanceAttackState(LanceStateMachine stateMachine, Lance lance) : base(stateMachine, lance)
    {
    }

    public override void Enter()
    {
        base.Enter();
        lance.Disappear();
        GameObject throwingLance;
        if (stateMachine.attackInfo.isUpPressed())
        {
            
            throwingLance = Lance.Instantiate(lance.throwableLancePrefabVertical, new Vector3(lance.shootingPostion.position.x, lance.shootingPostion.position.y, 0), Quaternion.identity);
            throwingLance.transform.up = Vector2.down;
            return;
        }
        if (stateMachine.attackInfo.isDownPressed())
        {
            throwingLance = Lance.Instantiate(lance.throwableLancePrefabVertical, new Vector3(lance.shootingPostion.position.x, lance.shootingPostion.position.y, 0), Quaternion.identity);
            return;
        }
        Vector3 shootPoint = new Vector3(lance.shootingPostion.position.x, lance.shootingPostion.position.y, 0);
        BoxCollider2D lanceCollider = lance.throwableLancePrefabHorizontal.GetComponent<BoxCollider2D>();
        float lanceLength = lanceCollider.size.x / 2 + lanceCollider.offset.x;
        //RaycastHit2D[] hits = Physics2D.RaycastAll(lance.shootingPostion.position, Vector2.right * lance.player.facingDir, lance.player.level);


        //foreach (RaycastHit2D hit in hits)
        //{
        //    if (hit.collider.gameObject.CompareTag("Wall"))
        //    {
        //        Debug.Log(hit.distance);
        //        float lanceLength = lance.throwableLancePrefabHorizontal.GetComponent<BoxCollider2D>().size.x;
        //        if (hit.distance < lanceLength)
        //        {
        //            shootPoint = hit.point - lance.player.facingDir * new Vector2(lanceLength, 0);
        //        }
        //    }
        //}
        RaycastHit2D hit = Physics2D.Raycast(lance.shootingPostion.position, Vector2.right * lance.player.facingDir, lanceLength, lance.player.level);
        if (hit.collider != null)
        {
            shootPoint = hit.point - lance.player.facingDir * new Vector2(lanceLength, 0);
        }
        //float lanceLength = lance.throwableLancePrefabHorizontal.GetComponent<BoxCollider2D>().size.x;
        //Vector3 shootPoint = new Vector3(lance.shootingPostion.position.x, lance.shootingPostion.position.y, 0);
        

        // not pressed, throwing at facing direction;
        throwingLance = Lance.Instantiate(lance.throwableLancePrefabHorizontal, shootPoint, Quaternion.identity);
        if (lance.player.facingDir == -1)
        {
            throwingLance.transform.right = Vector2.left;
        }
    }

    public override void Exit()
    {
        base.Exit();
        //Set Disappear immediately
    }

    public override void Update()
    {
        base.Update();
        stateMachine.ChangeState(lance.disappearState);
    }
}
