using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public enum WeaponType
    {
        Sword,
        Axe,
        Spear,
        LegendarySword
    }

   public WeaponType weaponType;

    public Weapon(WeaponType type)
    {
        weaponType = type;
    }

    public virtual int GetDamage()
    {
        switch (weaponType)
        {
            case WeaponType.Sword: return 10;
            case WeaponType.Axe: return 20;
            case WeaponType.Spear: return 40;
            case WeaponType.LegendarySword: return 100;
            default: return 0;
        }
    }
}
