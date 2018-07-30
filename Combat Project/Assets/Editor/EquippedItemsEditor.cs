using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EquippedItems))]
public class EquippedItemsEditor : Editor {

    

    EquippedItems equippedItems;

    private void OnEnable()
    {
        equippedItems = (EquippedItems)target;

    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(equippedItems.mainHandWeap != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label("Main Hand");
            if(GUILayout.Button("Unequip MH", GUILayout.MaxWidth(200), GUILayout.MaxHeight(20)))
            {
                equippedItems.UnequipWeaponSlot(WeaponLocation.mainHand);
            }

            GUILayout.EndHorizontal();
        }

        if (equippedItems.offHandWeap != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label("Off Hand");
            if (GUILayout.Button("Unequip OH", GUILayout.MaxWidth(200), GUILayout.MaxHeight(20)))
            {
                equippedItems.UnequipWeaponSlot(WeaponLocation.offHand);
            }

            GUILayout.EndHorizontal();
        }


    }
}
