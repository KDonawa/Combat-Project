using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon")]
public class Weapon : Item {

    public GameObject weaponPrefab;
    //public WeaponLocation location;
    //public WeaponType weaponType;
    public WeaponHandType weaponHandType;
    public LH_TransformData offHandData;

    // add bools for which locations have equips, and use those bools to determine what action are performed
    // for example, if we have main and off, an attack will cause both the weapons to move one after the next
    Weapon()
    {
        itemType = ItemType.Weapon;
    }
}

public enum WeaponLocation
{
    mainHand, offHand, bothHands
}

public enum WeaponType
{
    OneHandedMelee, TwoHandedMelee, OneHandedMagic, TwoHandedMagic, OneHandedRanged, TwoHandedRanged, Shield
    
}

public enum WeaponHandType
{
    OneHanded, TwoHanded
}



