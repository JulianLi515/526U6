using Unity.VisualScripting;
using UnityEngine;

public class WeaponController
{
    Player player;


    public WeaponController(Player player)
    {
        this.player = player;
    }

    public void UseGrabSkill()
    {

    }
    public void Attack(AttackInfo ai)
    {
        player.currentWeapon.attack(ai);
    }
    public void Skill(AttackInfo ai)
    {
        
    }
    public void switchWP1()
    {
        if(player.weapon1 == null)
        {
            return;
        }
        PlayerWeapon temp = player.currentWeapon;
        player.currentWeapon.DeactivateWeapon();
        player.currentWeapon = player.weapon1;
        player.weapon1 = temp;
        player.currentWeapon.ActivateWeapon();
    }
    public void switchWP2()
    {
        if (player.weapon2 == null)
        {
            return;
        }
        PlayerWeapon temp = player.currentWeapon;
        player.currentWeapon.DeactivateWeapon();
        player.currentWeapon = player.weapon2;
        player.weapon2 = temp;
        player.currentWeapon.ActivateWeapon();
    }

    public void SetCurrentWP(int id)
    {
        player.currentWeapon = player.weaponDictionary.SearchWeapon(id);
        player.currentWeapon.ActivateWeapon();
    }
}
