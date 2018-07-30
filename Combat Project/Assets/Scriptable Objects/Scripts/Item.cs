using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject {

    public string itemName;
    public string itemDescription;
    //public string skillDescription;
    //public Sprite icon;

    public ItemType itemType;
}

public enum ItemType
{
    Weapon, Consumable
}


