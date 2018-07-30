using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor {

    Inventory inventory;

    private void OnEnable()
    {
        inventory = (Inventory)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
      
        for (int k = 0; k < inventory.inventoryItems.Count; k++)
        {
            GUILayout.BeginHorizontal();

            DisplayInventoryItem(k);

            GUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }
    }

    private void DisplayInventoryItem(int k)
    {
        Item item = inventory.GetItem(k);
        if (item != null)
        {
            GUILayout.Label(inventory.GetItemName(k));

            if (item.itemType == ItemType.Weapon)
                ShowEquipWeaponButtons(item, k);
        }
    }

    private void ShowEquipWeaponButtons(Item item, int k)
    {
        Weapon weapon = item as Weapon;
        if (weapon.weaponHandType == WeaponHandType.OneHanded)
        {
            if (GUILayout.Button("Equip in MH", GUILayout.MaxWidth(200), GUILayout.MaxHeight(20)))
            {
                inventory.references.playerEqippedItems.EquipWeaponSlot(weapon, WeaponLocation.mainHand);
                //if equip is successful, we need to remove the weapon from inventory
                inventory.RemoveItem(k);
            }
            if (GUILayout.Button("Equip in OH", GUILayout.MaxWidth(200), GUILayout.MaxHeight(20)))
            {
                inventory.references.playerEqippedItems.EquipWeaponSlot(weapon, WeaponLocation.offHand);
                inventory.RemoveItem(k);
            }
        }
        else
        {
            if (GUILayout.Button("Equip", GUILayout.MaxWidth(100), GUILayout.MaxHeight(20)))
            {
                inventory.references.playerEqippedItems.EquipWeaponSlot(weapon, WeaponLocation.bothHands);
            }
        }
    }
}
