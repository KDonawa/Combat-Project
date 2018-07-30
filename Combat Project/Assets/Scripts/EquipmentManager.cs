using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    public References references;

    

    private void OnDisable()
    {
        references.playerEqippedItems.UnequipAll(); // only for now
    }
}
